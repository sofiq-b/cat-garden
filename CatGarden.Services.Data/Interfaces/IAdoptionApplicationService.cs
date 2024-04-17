using CatGarden.Data.Models;
using CatGarden.Web.ViewModels.AdoptionApplication;

namespace CatGarden.Services.Data.Interfaces
{
    public interface IAdoptionApplicationService
    {
        Task SendApplication(Guid userId, int catId);
        Task<bool> DeleteAdoptionApplicationAsync(Guid applicationId);
        Task<List<AdoptionApplicationOfUserViewModel>> GetUserAdoptionApplications(Guid userId);

        Task<AdoptionApplication> GetAdoptionApplicationByIdAsync(Guid applicationId);
        Task<bool> HasUserAlreadySentApplicationForCat(Guid userId, int catId);

    }
}
