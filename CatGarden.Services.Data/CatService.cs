using CatGarden.Data;
using CatGarden.Data.Models;
using CatGarden.Services.Data.Interfaces;
using CatGarden.ViewModels.Cat;
using CatGarden.Web.ViewModels.Cat;
using CatGarden.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
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
                   CoverImageUrl = c.CoverImageUrl,
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
                DateAdded = DateTime.UtcNow,
                CoverImageUrl = model.CoverImageUrl,
                CatteryId = model.SelectedCatteryId
            };

            foreach (var file in model.Images)
            {
                newCat.Images.Add(new Image()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }

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
                Description = cat.Description,
                DateAdded = cat.DateAdded,
                CoverImageUrl = cat.CoverImageUrl,
                ImageUrls = cat.Images.Select(image => image.URL).ToList(),
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
            Cat cat = await dbContext.Cats
                .Include(c => c.Cattery)
                .FirstAsync(c => c.Id == catId);

            return new CatDisplayViewModel
            {
                Id = catId,
                CoverImageUrl = cat.CoverImageUrl,
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
                    CoverImageUrl = cat.CoverImageUrl,
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


        public async Task<bool> IsFavoritedByUserWithIdAsync(int catId, string userId)
        {
            return await dbContext.UsersFavCats
            .AnyAsync(ufc => ufc.CatId == catId && ufc.UserId.ToString() == userId);
        }
    
    }
}
