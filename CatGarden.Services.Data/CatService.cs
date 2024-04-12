using CatGarden.Data;
using CatGarden.Data.Models;
using CatGarden.Services.Data.Interfaces;
using CatGarden.ViewModels.Cat;
using CatGarden.Web.ViewModels.Cat;
using CatGarden.Web.ViewModels.Home;
using CatGarden.Web.ViewModels.ImageGallery;
using Microsoft.EntityFrameworkCore;
using static CatGarden.Common.EntityValidationConstants;
using static CatGarden.Common.Enums;
namespace CatGarden.Services.Data
{
    public class CatService : ICatService
    {
        private readonly CatGardenDbContext dbContext;

        public CatService(CatGardenDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

       public async Task<IEnumerable<IndexViewModel>>LastThreeCatsAsync()
       {
           IEnumerable<IndexViewModel> lastThreeCats = await this.dbContext
               .Cats
               .OrderByDescending(c => c.DateAdded)
               .Take(3)
               .Select(c => new IndexViewModel
               {
                   Id = c.Id,
                   Name = c.Name,
                   CoverImageUrl = c.Images.FirstOrDefault(i => i.isCover)!.URL,
               })
               .ToArrayAsync();
           return lastThreeCats;
       }

        public async Task<int> CreateAndReturnIdAsync(CatFormModel model)
        {
            var newCat = new CatGarden.Data.Models.Cat()
            {
                Name = model.Name,
                Age = model.Age,
                Gender = model.Gender,
                Breed = model.Breed,
                Color = model.Color,
                CoatLength = model.CoatLength,
                Description = model.Description,
                DateAdded = DateTime.UtcNow,
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
            CatGarden.Data.Models.Cat cat = await dbContext.Cats
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
                CoverImageUrl = cat.Images.FirstOrDefault(i => i.isCover)!.URL,
                ImageUrls = cat.Images.Where(i=>i.isCover==false).Select(image => image.URL).ToList(),
                isFavorite = await IsFavoritedByUserWithIdAsync(catId, userId)
            };

            return viewModel;
        }

        public async Task AddCatToFavoritesAsync(int catId, string userId)
        {
            // Create a new entry in the UserFavCat table linking the user and the cat
            dbContext.UsersFavCats.Add(new UserFavCat
            {
                CatId = catId,
                UserId = Guid.Parse(userId)
            });

            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveFavoriteAsync(int catId, string userId)
        {
            // Find the existing favorite entry
            var favorite = await dbContext.UsersFavCats
                .FirstOrDefaultAsync(f => f.CatId == catId && f.UserId.ToString() == userId);

            // Remove the favorite entry
            dbContext.UsersFavCats.Remove(favorite!);
            await dbContext.SaveChangesAsync();
            
        }

        public async Task<CatDisplayViewModel> GetCatDisplayViewModelAsync(int catId)
        {
            CatGarden.Data.Models.Cat cat = await dbContext.Cats
                .Include(c => c.Cattery)
                .FirstAsync(c => c.Id == catId);

            return new CatDisplayViewModel
            {
                Id = catId,
                CoverImageUrl = cat.Images.FirstOrDefault(i => i.isCover)!.URL,
                Name = cat.Name,
                Breed = cat.Breed.ToString(),
                Gender = cat.Gender.ToString(),
                Age = cat.Age,
                Location = cat.Cattery.City.ToString()
            };
        }

        public async Task<IEnumerable<CatDisplayViewModel>> GetFavoriteCatsAsync(string userId)
        {
            // Get the IDs of cats favorited by the user
            var favoriteCatIds = await dbContext.UsersFavCats
                .Where(ufc => ufc.UserId.ToString() == userId)
                .Select(ufc => ufc.CatId)
                .ToListAsync();

            // Retrieve the details of cats in the user's favorites
            var favoriteCats = await dbContext.Cats
                .Include(c => c.Cattery)
                .Where(c => favoriteCatIds.Contains(c.Id)) // Filter by favorite cat IDs
                .Select(cat => new CatDisplayViewModel
                {
                    Id = cat.Id,
                    CoverImageUrl = cat.Images.FirstOrDefault(i => i.isCover)!.URL,
                    Name = cat.Name,
                    Breed = cat.Breed.ToString(),
                    Gender = cat.Gender.ToString(),
                    Age = cat.Age,
                    IsFavorite = true,
                    Location = cat.Cattery.City.ToString()
                })
                .ToListAsync();

            return favoriteCats;
        }

        public async Task RemoveAllCatsFromFavoritesAsync(string userId)
        {
            var userFavorites = dbContext.UsersFavCats
                .Where(ufc => ufc.UserId.ToString() == userId)
                .ToList();

            dbContext.UsersFavCats.RemoveRange(userFavorites);

            await dbContext.SaveChangesAsync();
        }


        public async Task<bool> IsCatPartOfOwnedCattery(int catId, string userId)
        {
            // Assuming dbContext is your DbContext instance
            var cat = await dbContext.Cats
                .Include(c => c.Cattery)
                    .ThenInclude(c => c.Owner)
                .FirstOrDefaultAsync(c => c.Id == catId);


            // Check if the cat's cattery exists and is owned by the specified owner
            return cat.Cattery.Owner.UserId.ToString() == userId;
        }

        public async Task<CatFormModel> GetCatForEdit(int catId, string userId)
        {
            // Get the cat details
            var catDetails = await GetDetailsByIdAsync(catId, userId);

            var images = new List<ImageModel>();
            var imageData = dbContext.Images.Where(i => i.CatId == catId);
            foreach (var image in imageData)
            {
                // Retrieve image data based on the URL (Assuming you have a method to fetch image data from URL)
                

                // Create ImageModel instance
                var imageModel = new ImageModel
                {
                    Name = image.Name,
                    URL = image.URL // Assuming the URL is still valid and accessible
                };

                // Add to the list of images
                images.Add(imageModel);
            }

            // Map the CatDetailsViewModel to CatFormModel
            var catFormModel = new CatFormModel
            {
                Name = catDetails.Name,
                Age = catDetails.Age,
                Gender = Enum.Parse<Gender>(catDetails.Gender),
                Breed = Enum.Parse<Breed>(catDetails.Breed),
                Color = Enum.Parse<Color>(catDetails.Color),
                CoatLength = Enum.Parse<CoatLength>(catDetails.CoatLength),
                Description = catDetails.Description,
                CoverImageUrl = catDetails.CoverImageUrl,
                SelectedCatteryId = catDetails.CatteryId,
                Images = images

            };

            return catFormModel;
        }

        public async Task<IEnumerable<CatDisplayViewModel>> GetAllCatsAsync(string userId)
        {
            // Get all cats with their details
            var allCats = await dbContext.Cats
                .Include(c => c.Cattery)
                .Include(c => c.Images)
                .Select(cat => new CatDisplayViewModel
                {
                    Id = cat.Id,
                    CoverImageUrl = cat.Images.FirstOrDefault(i => i.isCover)!.URL,
                    Name = cat.Name,
                    Breed = cat.Breed.ToString(),
                    Gender = cat.Gender.ToString(),
                    Age = cat.Age,
                    IsFavorite = dbContext.UsersFavCats.Any(ufc => ufc.UserId.ToString() == userId && ufc.CatId == cat.Id),
                    Location = cat.Cattery.City.ToString()
                })
                .ToListAsync();

            return allCats;
        }


        public async Task<bool> IsFavoritedByUserWithIdAsync(int catId, string userId)
        {
            return await dbContext.UsersFavCats
            .AnyAsync(ufc => ufc.CatId == catId && ufc.UserId.ToString() == userId);
        }
    
    }
}
