using CatGarden.Data;
using CatGarden.Services.Data.Interfaces;
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

        public async Task<IEnumerable<IndexViewModel>> LastThreeCatsAsync()
        {
            IEnumerable<IndexViewModel> lastThreeCats = await this.dbContext
                .Cats
                .OrderByDescending(c => c.DateAdded)
                .Take(3)
                .Select(c => new IndexViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImageUrl = c.ImageUrl,
                })
                .ToArrayAsync();
            return lastThreeCats;
        }
    }
}
