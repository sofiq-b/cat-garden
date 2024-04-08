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

        public async Task<CatDetailsViewModel> GetDetailsByIdAsync(int catId)
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
                ImageUrls = cat.Images.Select(image => image.URL).ToList()
            };

            return viewModel;
        }
    }
}
