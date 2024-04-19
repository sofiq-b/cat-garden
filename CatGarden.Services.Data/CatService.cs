using CatGarden.Data;
using CatGarden.Data.Models;
using CatGarden.Services.Data.Interfaces;
using CatGarden.Services.Data.Models.Cat;
using CatGarden.ViewModels.Cat;
using CatGarden.Web.ViewModels.Cat;
using CatGarden.Web.ViewModels.Cat.Enums;
using CatGarden.Web.ViewModels.Home;
using CatGarden.Web.ViewModels.ImageGallery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using static CatGarden.Common.EntityValidationConstants;
using static CatGarden.Common.Enums;
using Cat = CatGarden.Data.Models.Cat;
using Image = CatGarden.Data.Models.Image;
namespace CatGarden.Services.Data
{
    public class CatService : ICatService
    {
        private readonly CatGardenDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IImageService imageService;
        private readonly ICatteryService catteryService;
        public CatService(CatGardenDbContext dbContext, IWebHostEnvironment webHostEnvironment, IImageService imageService, ICatteryService catteryService)
        {
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.imageService = imageService;
            this.catteryService = catteryService;
        }

        public async Task<IEnumerable<IndexViewModel>> LastThreeCatsAsync()
        {
            var lastThreeCats = await this.dbContext
                .Cats
                .OrderByDescending(c => c.DateAdded)
                .Take(3)
                .Select(c => new IndexViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    CoverImageUrl = c.Images.FirstOrDefault(i => i.IsCover) != null ? c.Images.FirstOrDefault(i => i.IsCover)!.URL : "alternative_text_here"
                })
                .ToArrayAsync();

            return lastThreeCats;
        }
        public async Task<int> CreateAndReturnIdAsync(CatFormModel model)
        {
            var newCat = new Cat()
            {
                Name = model.Name,
                Age = model.Age,
                Gender = model.Gender,
                Breed = model.Breed,
                Color = model.Color,
                CoatLength = model.CoatLength,
                Description = model.Description,
                DateAdded = DateTime.Now,
                CatteryId = model.SelectedCatteryId
            };
            await dbContext.Cats.AddAsync(newCat);
            await dbContext.SaveChangesAsync();

            return newCat.Id;
        }
        public async Task<bool> ExistsByIdAsync(int catId)
        {
            bool result = await dbContext
                .Cats
                .AnyAsync(c => c.Id == catId);

            return result;
        }
        public async Task<CatDetailsViewModel> GetDetailsByIdAsync(int catId, string userId)
        {
            Cat cat = await dbContext.Cats
                .Include(c => c.Cattery)
                .Include(c => c.Images)
                .FirstAsync(c => c.Id == catId);


            var viewModel = new CatDetailsViewModel
            {
                Id = cat.Id,
                Name = cat.Name,
                Age = cat.Age,
                Gender = cat.Gender.ToString(),
                Breed = cat.Breed.ToString(),
                CoatLength = cat.CoatLength.ToString(),
                Color = cat.Color.ToString(),
                CatteryName = cat.Cattery.Name,
                CatteryId = cat.Cattery.Id,
                Description = cat.Description,
                DateAdded = cat.DateAdded,
                IsFavorite = await IsFavoritedByUserWithIdAsync(catId, userId),
                LikesCount = cat.LikesCount
            };
            
            viewModel.CoverImageUrl = cat.Images.FirstOrDefault(i => i.IsCover)?.URL ?? "alternative_text_here";
           
            viewModel.ImageUrls = cat.Images.Where(i => !i.IsCover).Select(image =>
                Path.Combine("\\cats", GenerateCatDirectory(cat), image.URL)).ToList();

            return viewModel;
        }

        public async Task<int> InsertImagesAndReturnCatIdAsync(CatFormModel formModel)
        {
            int catId = await CreateAndReturnIdAsync(formModel);
            var cat = await GetByIdAsync(catId);
            foreach (ImageModel imageModel in formModel.Images)
            {
                string uniqueFileName = $"{Guid.NewGuid()}_{imageModel.Name}";
                string destinationFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "cats", GenerateCatDirectory(cat)).Replace('\\', '/');

                Directory.CreateDirectory(destinationFolderPath);

                string destinationFilePath = Path.Combine(destinationFolderPath, uniqueFileName).Replace('\\', '/');

                System.IO.File.Move(imageModel.URL, destinationFilePath);

                imageModel.URL = Path.Combine("/cats", GenerateCatDirectory(cat), uniqueFileName).Replace('\\', '/');

                var image = new Image { Name = imageModel.Name, URL = imageModel.URL, CatId = catId, IsCover = imageModel.IsCover };
                cat.Images.Add(image);
                dbContext.Images.Add(image);
            }
            await dbContext.SaveChangesAsync();
            string tempFolderPath = Path.Combine(webHostEnvironment.WebRootPath,"images", "temp");
            if (Directory.Exists(tempFolderPath))
            {
                Directory.Delete(tempFolderPath, true);
            }
            return catId;
        }

        

        public async Task AddCatToFavoritesAsync(int catId, string userId)
        {
            dbContext.UsersFavCats.Add(new UserFavCat
            {
                CatId = catId,
                UserId = Guid.Parse(userId)
            });
            var cat = await GetByIdAsync(catId);
            cat.LikesCount++;
            await UpdateCatLikesAsync(cat);
            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveFavoriteAsync(int catId, string userId)
        {
            var favorite = await dbContext.UsersFavCats
                .FirstOrDefaultAsync(f => f.CatId == catId && f.UserId.ToString() == userId);

            dbContext.UsersFavCats.Remove(favorite!);
            var cat = await GetByIdAsync(catId);
            cat.LikesCount--;
            await UpdateCatLikesAsync(cat);
            await dbContext.SaveChangesAsync();
            
        }

        public async Task<CatDisplayViewModel> GetCatDisplayViewModelAsync(int catId)
        {
            Cat cat = await dbContext.Cats
                .Include(c => c.Cattery)
                .FirstAsync(c => c.Id == catId);

            return new CatDisplayViewModel
            {
                Id = catId,
                CoverImageUrl = cat.Images.FirstOrDefault(i => i.IsCover)?.URL ?? "alternative_text_here",
                Name = cat.Name,
                Breed = cat.Breed.ToString(),
                Gender = cat.Gender.ToString(),
                Age = cat.Age,
                Location = cat.Cattery.City.ToString()
            };
        }

        public async Task<IEnumerable<CatDisplayViewModel>> GetFavoriteCatsAsync(string userId)
        {
            var favoriteCatIds = await dbContext.UsersFavCats
                .Where(ufc => ufc.UserId.ToString() == userId)
                .Select(ufc => ufc.CatId)
                .ToListAsync();

            var favoriteCats = await dbContext.Cats
                .Include(c => c.Cattery)
                .Where(c => favoriteCatIds.Contains(c.Id)) // Filter by favorite cat IDs
                .Select(cat => new CatDisplayViewModel
                {
                    Id = cat.Id,
                    Name = cat.Name,
                    Breed = cat.Breed.ToString(),
                    Gender = cat.Gender.ToString(),
                    Age = cat.Age,
                    IsFavorite = true,
                    Location = cat.Cattery.City.ToString(),
                    LikesCount = cat.LikesCount,
                    CoverImageUrl = cat.Images.FirstOrDefault(i => i.IsCover) != null ? cat.Images.FirstOrDefault(i => i.IsCover)!.URL : "alternative_text_here"
                })
                .ToListAsync();
            return favoriteCats;
        }

        public async Task RemoveAllCatsFromFavoritesAsync(string userId)
        {
            var userFavorites = dbContext.UsersFavCats
                .Where(ufc => ufc.UserId.ToString() == userId)
                .ToList();

            foreach (var favorite in userFavorites)
            {
                var cat = await GetByIdAsync(favorite.CatId);
                if (cat != null && cat.LikesCount > 0)
                {
                    cat.LikesCount--;
                    await UpdateCatLikesAsync(cat);
                }
            }
            dbContext.UsersFavCats.RemoveRange(userFavorites);

            await dbContext.SaveChangesAsync();
        }
        public async Task UpdateCatLikesAsync(Cat cat)
        {
            var existingCat = await GetByIdAsync(cat.Id);

            if (existingCat != null)
            {
                existingCat.LikesCount = cat.LikesCount;

                await dbContext.SaveChangesAsync();
            }
        }
        public async Task<Cat> GetByIdAsync(int catId)
        {
            return await dbContext.Cats.FirstOrDefaultAsync(c => c.Id == catId);
        }

        public async Task<bool> IsCatPartOfOwnedCattery(int catId, string userId)
        {
            var cat = await dbContext.Cats
                .Include(c => c.Cattery)
                    .ThenInclude(c => c.Owner)
                .FirstOrDefaultAsync(c => c.Id == catId);

            return cat.Cattery.Owner.UserId.ToString() == userId;
        }

        public async Task<CatFormModel> GetCatForEdit(int catId, string userId)
        {
            var catDetails = await GetDetailsByIdAsync(catId, userId);

            var images = new List<ImageModel>();
            var imageData = dbContext.Images.Where(i => i.CatId == catId);
            foreach (var image in imageData)
            {
                var imageModel = new ImageModel
                {
                    Name = image.Name,
                    URL = image.URL 
                };
                images.Add(imageModel);
            }

            var catFormModel = new CatFormModel
            {
                Name = catDetails.Name,
                Age = catDetails.Age,
                Gender = Enum.Parse<Gender>(catDetails.Gender),
                Breed = Enum.Parse<Breed>(catDetails.Breed),
                Color = Enum.Parse<Color>(catDetails.Color),
                CoatLength = Enum.Parse<CoatLength>(catDetails.CoatLength),
                Description = catDetails.Description,
                SelectedCatteryId = catDetails.CatteryId,
                Images = images

            };

            return catFormModel;
        }

        public async Task<CatFormEditViewModel> LoadEditCatAsync(int catId, string userId)
        {
            var cat = await GetByIdAsync(catId);

            var model = new CatFormEditViewModel()
            {
                Id = cat.Id,
                Name = cat.Name,
                Age = cat.Age,
                Gender = cat.Gender,
                Breed = cat.Breed,
                Color = cat.Color,
                CoatLength = cat.CoatLength,
                Description = cat.Description,
                SelectedCatteryId = cat.CatteryId,
            };

            model.FolderPathUrl = Path.Combine(webHostEnvironment.WebRootPath, "cats", GenerateCatDirectory(cat)).Replace('\\', '/');
            model.Catteries = await catteryService.OwnedCatteriesAsync(userId);
            model.Images = await imageService.GetCatImagesAsync(cat);
            return model;
        }
        public async Task<IEnumerable<CatDisplayViewModel>> GetAllCatsAsync(string userId)
        {
            var allCats = await dbContext.Cats
                .Include(c => c.Cattery)
                .Include(c => c.Images)
                .ToListAsync(); 

            var catViewModels = allCats.Select(cat => new CatDisplayViewModel
            {
                Id = cat.Id,
                Name = cat.Name,
                Breed = cat.Breed.ToString(),
                Gender = cat.Gender.ToString(),
                Age = cat.Age,
                IsFavorite = dbContext.UsersFavCats.Any(ufc => ufc.UserId.ToString() == userId && ufc.CatId == cat.Id),
                Location = cat.Cattery.City.ToString(),
                LikesCount = cat.LikesCount,
                CoverImageUrl = cat.Images.FirstOrDefault(i => i.IsCover)?.URL ?? "alternative_text_here"
        }).ToList();

            return catViewModels;
        }

        public async Task<bool> DeleteCatAsync(int catId)
        {
            Cat cat = await GetByIdAsync(catId);

            var catFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "cats", GenerateCatDirectory(cat));

            if (Directory.Exists(catFolderPath))
            {
                Directory.Delete(catFolderPath, true);
            }
            var favCats = dbContext.UsersFavCats.Where(x => x.CatId == catId).ToList();
            dbContext.UsersFavCats.RemoveRange(favCats);

            var imagesToDelete = dbContext.Images.Where(image => image.CatId == catId);
            dbContext.RemoveRange(imagesToDelete);

            var applicationsToDelete = dbContext.AdoptionApplications.Where(ap => ap.CatId == catId);
            dbContext.RemoveRange(applicationsToDelete);

            dbContext.Cats.Remove(cat);

            await dbContext.SaveChangesAsync();
            return true; 
        }
        

        public async Task UpdateCatAsync(CatFormEditViewModel model)
        {
            var existingCat = await GetByIdAsync(model.Id);

            existingCat.Name = model.Name;
            existingCat.Age = model.Age;
            existingCat.Gender = model.Gender;
            existingCat.Breed = model.Breed;
            existingCat.Color = model.Color;
            existingCat.CoatLength = model.CoatLength;
            existingCat.Description = model.Description;
            existingCat.CatteryId = model.SelectedCatteryId;

            await dbContext.SaveChangesAsync();
        }
        public async Task<bool> IsAdoptedAsync(int catId)
        {
            return await dbContext.Cats
                 .Where(c => c.Id == catId)
                 .SelectMany(c => c.AdoptionApplications)
                 .AnyAsync(app => app.ApplicationStatus == ApplicationStatus.Accepted);
        }
        public string GenerateCatDirectory(Cat cat)
        {
            return $"{cat.Name.ToLower().Replace(" ", "-")}_{cat.Id}"; 
        }

        public async Task<bool> IsFavoritedByUserWithIdAsync(int catId, string userId)
        {
            return await dbContext.UsersFavCats
            .AnyAsync(ufc => ufc.CatId == catId && ufc.UserId.ToString() == userId);
        }

        public async Task<AllCatsFilteredAndPagedServiceModel> AllAsync(AllCatsQueryModel queryModel, string userId)
        {
            IQueryable<Cat> catsQuery = this.dbContext.Cats
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Cattery))
            {
                catsQuery = catsQuery
                    .Where(c => c.Cattery.Name == queryModel.Cattery);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";
                catsQuery = catsQuery
                    .Where(c => EF.Functions.Like(c.Name, wildCard) ||
                     EF.Functions.Like(c.Description, wildCard));
            }

            if (queryModel.Breed != null)
            {
                catsQuery = catsQuery.Where(c => c.Breed == queryModel.Breed);
            }

            if (queryModel.Gender != null)
            {
                catsQuery = catsQuery.Where(c => c.Gender == queryModel.Gender);
            }

            if (queryModel.Color != null)
            {
                catsQuery = catsQuery.Where(c => c.Color == queryModel.Color);
            }

            if (queryModel.CoatLength != null)
            {
                catsQuery = catsQuery.Where(c => c.CoatLength == queryModel.CoatLength);
            }

            if (queryModel.City != null)
            {
                catsQuery = catsQuery.Where(c => c.Cattery.City == queryModel.City);
            }


            catsQuery = queryModel.CatSorting switch
            {
                CatSorting.Newest => catsQuery
                .OrderBy(c=>c.DateAdded),

                CatSorting.Oldest => catsQuery
                 .OrderByDescending(c => c.DateAdded),

                CatSorting.AgeAscending => catsQuery
                .OrderBy(c => c.Age),

                CatSorting.AgeDescending => catsQuery
                .OrderByDescending(c => c.Age),

                CatSorting.LikesAscending => catsQuery
                .OrderBy(c => c.LikesCount),

                CatSorting.LikesDescending => catsQuery
                .OrderByDescending(c => c.LikesCount),

                CatSorting.IsAvailable => catsQuery
                .OrderByDescending(c => c.AvailabilityStatus!= AvailabilityStatus.Adopted)
            };
            IEnumerable<CatDisplayViewModel> allCats = await catsQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.CatsPerPage)
                .Take(queryModel.CatsPerPage)
                .Select(cat => new CatDisplayViewModel
                {
                    Id = cat.Id,
                    Name = cat.Name,
                    Breed = cat.Breed.ToString(),
                    Gender = cat.Gender.ToString(),
                    Age = cat.Age,
                    IsFavorite = dbContext.UsersFavCats.Any(ufc => ufc.UserId.ToString() == userId && ufc.CatId == cat.Id),
                    Location = cat.Cattery.City.ToString(),
                    LikesCount = cat.LikesCount,
                    CoverImageUrl = cat.Images.FirstOrDefault(i => i.IsCover) != null ? cat.Images.FirstOrDefault(i => i.IsCover)!.URL : "alternative_text_here"
                })
                .ToListAsync();
            int totalCats = catsQuery.Count();

            return new AllCatsFilteredAndPagedServiceModel()
            {
                TotalCatsCount = totalCats,
                Cats = allCats
            };
        }
    }
}
