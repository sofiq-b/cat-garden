using CatGarden.Web.ViewModels.AdoptionApplication;
using CatGarden.Web.ViewModels.ImageGallery;

namespace CatGarden.Web.ViewModels.Cat
{
    public class CatWithAdoptionApplicationViewModel : CatDisplayViewModel
    {
        public ICollection<AdoptionApplicationOfUserViewModel> AdoptionApplications { get; set; } = new List<AdoptionApplicationOfUserViewModel>();
        public List<ImageModel> Images { get; set; } = new List<ImageModel>();
    }
}
