using CatGarden.Data.Models;
using CatGarden.Web.ViewModels.AdoptionApplication;

namespace CatGarden.Services.Data.Interfaces
{
    public interface IAdoptionApplicationService
    {
        Task SendApplication(Guid userId, int catId);
        Task<bool> DeleteAdoptionApplicationAsync(Guid applicationId);
        Task<List<AdoptionApplicationOfUserViewModel>> GetUserAdoptionApplications(Guid userId);
        Task<bool> AcceptAdoptionApplicationAsync(Guid id);
        Task<bool> RejectAdoptionApplicationAsync(Guid id);
        Task<AdoptionApplication> GetAdoptionApplicationByIdAsync(Guid applicationId);
        Task<bool> HasUserAlreadySentApplicationForCat(Guid userId, int catId);

    }
}
