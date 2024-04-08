
using CatGarden.ViewModels.Cat;
using CatGarden.Web.ViewModels.Cat;
using CatGarden.Web.ViewModels.Home;

namespace CatGarden.Services.Data.Interfaces
{
    public interface ICatService
    {
        Task<IEnumerable<IndexViewModel>> LastThreeCatsAsync();

        Task<int> CreateAndReturnIdAsync(CatFormModel formModel);

        Task<bool> ExistsByIdAsync(int catId);

        Task<CatDetailsViewModel> GetDetailsByIdAsync(int catId);

        Task<bool> IsFavoritedByUserWithIdAsync(int catId, string userId);

    }
}
