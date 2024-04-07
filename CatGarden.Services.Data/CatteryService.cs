using CatGarden.Data;
using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.ViewModels.Cattery;
using Microsoft.EntityFrameworkCore;

namespace CatGarden.Services.Data
{
    public class CatteryService : ICatteryService
    {
        private readonly CatGardenDbContext dbContext;

        public CatteryService(CatGardenDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<IEnumerable<CatteryViewForCatFormModel>> AllCatteriesAsync(string userId)
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
    }
}
