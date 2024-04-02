using CatGarden.Web.ViewModels.Home;

namespace CatGarden.Services.Data.Interfaces
{
    public interface ICatService
    {
        Task<IEnumerable<IndexViewModel>> LastThreeCatsAsync();
    }
}
