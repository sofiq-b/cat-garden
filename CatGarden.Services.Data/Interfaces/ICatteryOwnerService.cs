using CatGarden.Web.ViewModels.CatteryOwner;

namespace CatGarden.Services.Data.Interfaces
{
    public interface ICatteryOwnerService
    {
        Task<bool> CatteryOwnerExistsByUserIdAsync(string userId);

        Task<bool> CatteryOwnerExistsByPhoneNumberAsync(string phoneNumber);

        Task Create(string userId, BecomeCatteryOwnerFormModel model);
    }
}
