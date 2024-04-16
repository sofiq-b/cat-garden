using CatGarden.Web.ViewModels.AdoptionApplication;

namespace CatGarden.Web.ViewModels.Cat
{
    public class CatWithAdoptionApplicationViewModel : CatDisplayViewModel
    {
        public ICollection<AdoptionApplicationViewModel> AdoptionApplications { get; set; } = new List<AdoptionApplicationViewModel>();
    }
}
