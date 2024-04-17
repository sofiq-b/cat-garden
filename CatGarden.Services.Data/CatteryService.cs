using CatGarden.Data;
using CatGarden.Data.Models;
using CatGarden.Services.Data.Interfaces;
using CatGarden.ViewModels.Cat;
using CatGarden.Web.ViewModels.AdoptionApplication;
using CatGarden.Web.ViewModels.Cat;
using CatGarden.Web.ViewModels.Cattery;
using CatGarden.Web.ViewModels.ImageGallery;
using CatGarden.Web.ViewModels.Review;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static CatGarden.Common.EntityValidationConstants;
using static System.Net.Mime.MediaTypeNames;
using Cattery = CatGarden.Data.Models.Cattery;
using CatteryOwner = CatGarden.Data.Models.CatteryOwner;
using Image = CatGarden.Data.Models.Image;

namespace CatGarden.Services.Data
{
    public class CatteryService : ICatteryService
    {
        private readonly CatGardenDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CatteryService(CatGardenDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
        }


        public async Task<IEnumerable<CatteryViewForCatFormModel>> OwnedCatteriesAsync(string userId)
        {
            IEnumerable<CatteryViewForCatFormModel> ownedCatteries = await this.dbContext
                .Catteries
                .Where(c => c.Owner.UserId.ToString() == userId)
                .AsNoTracking()
                .Select(c => new CatteryViewForCatFormModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

            return ownedCatteries;
        }

        public async Task<IEnumerable<CatteryDisplayViewModel>> AllCatteriesAsync()
        {
            var catteries = await dbContext.Catteries
                .Include(c => c.Images)
                .AsNoTracking()
                .ToListAsync();
            var catteryDisplayModels = catteries.Select(c => new CatteryDisplayViewModel()
            {
                Id = c.Id,
                CoverImageUrl = c.Images.FirstOrDefault(i => i.IsCover)?.URL ?? "alternative_text_here",
                Name = c.Name,
                City = c.City.ToString(),
                Address = c.Address,
                EstablishmentDate = c.EstablishmentDate,
            });
            return catteryDisplayModels;
        }

        public async Task<int> CreateAndReturnIdAsync(CatteryFormModel model, string userId)
        {
            CatteryOwner owner = dbContext.CatteryOwners.SingleOrDefault(x => x.UserId.ToString() == userId);
            var newCattery = new Cattery()
            {
                Name = model.Name,
                Address = model.Address,
                City = model.City,
                EstablishmentDate = model.EstablishmentDate,
                OwnerId = owner.Id,
                Owner = owner,
            };
            await dbContext.Catteries.AddAsync(newCattery);
            await dbContext.SaveChangesAsync();

            return newCattery.Id;
        }

        public async Task<int> InsertImagesAndReturnCatteryIdAsync(CatteryFormModel formModel, string userId)
        {
            int catteryId = await CreateAndReturnIdAsync(formModel, userId);
            var cattery = await GetByIdAsync(catteryId);
            // Associate uploaded images with the cat entity
            foreach (ImageModel imageModel in formModel.Images)
            {
                string uniqueFileName = $"{Guid.NewGuid()}_{imageModel.Name}";
                // Construct the destination folder path (e.g., based on cat ID)
                string destinationFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "catteries", GenerateCatteryDirectory(cattery)).Replace('\\', '/');

                // Ensure that the destination folder exists; if not, create it
                Directory.CreateDirectory(destinationFolderPath);

                // Construct the destination file path
                string destinationFilePath = Path.Combine(destinationFolderPath, uniqueFileName).Replace('\\', '/');

                // Move the file from the temporary location to the permanent location
                System.IO.File.Move(imageModel.URL, destinationFilePath);

                // Update the URL property of the ImageModel to contain the relative path within wwwroot
                imageModel.URL = Path.Combine("/catteries", GenerateCatteryDirectory(cattery), uniqueFileName).Replace('\\', '/');

                // Create Image entity and associate it with the cat
                var image = new Image { Name = imageModel.Name, URL = imageModel.URL, CatteryId = catteryId, IsCover = imageModel.IsCover };
                cattery.Images.Add(image);
                dbContext.Images.Add(image);
            }
            // Save changes to the database
            await dbContext.SaveChangesAsync();
            string tempFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "images", "temp");
            if (Directory.Exists(tempFolderPath))
            {
                Directory.Delete(tempFolderPath, true);
            }
            return catteryId;
        }

        public async Task<CatteryDetailsViewModel> GetDetailsByIdAsync(int catteryId)
        {
            Cattery cattery = await dbContext.Catteries
                .Include(c => c.Cats)
                    .ThenInclude(cat => cat.Images)
                .Include(c => c.Cats) // Include adoption applications for each cat
                    .ThenInclude(cat => cat.AdoptionApplications)
                    .ThenInclude(app => app.User)
                .Include(c => c.Reviews)
                    .ThenInclude(r => r.User) // Include the user information for each review
                .Include(c => c.Images)
                .FirstAsync(c => c.Id == catteryId);



            var viewModel = new CatteryDetailsViewModel
            {
                Id = catteryId,
                Name = cattery.Name,
                City = cattery.City.ToString(),
                Address = cattery.Address,
                EstablishmentDate = cattery.EstablishmentDate,
            };

            viewModel.Images = cattery.Images.Select(image => new ImageModel
            {
                Name = image.Name,
                URL = image.URL,
                IsCover = image.IsCover,
                CatteryId = catteryId,
            }).ToList();
            

            viewModel.Cats = cattery.Cats.Select(cat => new CatWithAdoptionApplicationViewModel
            {
                Id = cat.Id,
                CoverImageUrl = cat.Images.Select(i => new ImageModel
                {
                    Name = i.Name,
                    URL = i.URL,
                    IsCover = i.IsCover,
                    CatId = cat.Id,
                }).FirstOrDefault(i => i.IsCover)?.URL ?? "alternative_text_here",
                Name = cat.Name,
                Breed = cat.Breed.ToString(),
                Gender = cat.Gender.ToString(),
                Age = cat.Age,
                Location = cat.Cattery.City.ToString(),
                AdoptionApplications = cat.AdoptionApplications
                      .Select(app => new AdoptionApplicationOfUserViewModel   
                      {
                          Username = app.User.UserName,
                          CatName = cat.Name,
                          ApplicationDate = app.ApplicationDate,
                          ApplicationStatus = app.ApplicationStatus.ToString()
                      })
                      .ToList()
            }).ToList();

           
            viewModel.Reviews = cattery.Reviews.Select(review => new ReviewDisplayViewModel
            {
                Id = review.Id,
                UserId = review.User.Id.ToString(),
                Username = review.User.UserName,
                Rating = review.Rating,
                Comment = review.Comment,
                DatePosted = review.DatePosted
            })
            .ToList();


            return viewModel;
        }

        public async Task<bool> ExistsByIdAsync(int catteryId)
        {
            bool result = await dbContext
                .Catteries
                .AnyAsync(c => c.Id == catteryId);
            return result;
        }

        public async Task<Cattery> GetByIdAsync(int catteryId)
        {
            return await dbContext.Catteries.FirstOrDefaultAsync(c => c.Id == catteryId);
        }

        public string GenerateCatteryDirectory(Cattery cattery)
        {
            return $"{cattery.Name.ToLower().Replace(" ", "-")}_{cattery.Id}";
        }

        public async Task<bool> IsCatteryOwnedByUserAsync(string userId, int catteryId)
        {
            var catteryOwner = await dbContext.CatteryOwners.FirstOrDefaultAsync(c => c.UserId.ToString() == userId);
            // Retrieve the cattery from the database
            var cattery = await dbContext.Catteries.FirstOrDefaultAsync(c => c.Id == catteryId);

            if (catteryOwner == null)
            {
                return false;
            }

            if (cattery != null && cattery.OwnerId == catteryOwner.Id)
            {
                return true; // The cattery is owned by the user
            }

            return false; // The cattery is not owned by the user or does not exist
        }
    }
}
