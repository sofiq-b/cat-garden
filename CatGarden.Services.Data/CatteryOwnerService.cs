using CatGarden.Data;
using CatGarden.Data.Models;
using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.ViewModels.CatteryOwner;
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

        public async Task<bool> CatteryOwnerExistsByPhoneNumberAsync(string phoneNumber)
        {
            bool result = await this.dbContext
                 .CatteryOwners
                 .AnyAsync(c => c.PhoneNumber == phoneNumber);

            return result;
        }

        public async Task<bool> CatteryOwnerExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .CatteryOwners
                .AnyAsync(c => c.UserId.ToString() == userId);

            return result;
        }

        public async Task Create(string userId, BecomeCatteryOwnerFormModel model)
        {
            CatteryOwner newCatteryOwner = new CatteryOwner()
            {
                PhoneNumber = model.PhoneNumber,
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.CatteryOwners.AddAsync(newCatteryOwner);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
