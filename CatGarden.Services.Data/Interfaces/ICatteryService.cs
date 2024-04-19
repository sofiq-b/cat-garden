using CatGarden.Data.Models;
using CatGarden.Web.ViewModels.Cattery;

namespace CatGarden.Services.Data.Interfaces
{
    public interface ICatteryService
    {
        Task<IEnumerable<CatteryDisplayViewModel>> AllCatteriesAsync();
        Task<IEnumerable<CatteryViewForCatFormModel>> OwnedCatteriesAsync(string userId);
        Task<int> CreateAndReturnIdAsync(CatteryFormModel model, string userId);
        Task<int> InsertImagesAndReturnCatteryIdAsync(CatteryFormModel formModel, string userId);
        Task<CatteryDetailsViewModel> GetDetailsByIdAsync(int catteryId);
        Task<CatteryFormModel> GetCatteryForEdit(int catteryId, string userId);
        Task<CatteryFormEditViewModel> LoadEditCatteryAsync(int catteryId);
        Task UpdateCatteryAsync(CatteryFormEditViewModel model);

        Task<bool> DeleteCatteryAsync(int catteryId);

        Task<bool> ExistsByIdAsync(int catteryId);
        string GenerateCatteryDirectory(Cattery cattery);
        Task<Cattery> GetByIdAsync(int catteryId);
        Task<bool> IsCatteryOwnedByUserAsync(string userId, int catteryId);
        Task<IEnumerable<string>> AllCatteryNamesAsync();
    }
}
