using CatGarden.Data;
using CatGarden.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatGarden.Services.Data
{
    public class CatteryOwnerService : ICatteryOwnerService
    {
        private readonly CatGardenDbContext dbContext;

        public CatteryOwnerService(CatGardenDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CatteryOwnerExistsByUserId(string userId)
        {
            bool result = await this.dbContext
                .CatteryOwners
                .AnyAsync(c => c.UserId.ToString() == userId);

            return result;
        }
    }
}
