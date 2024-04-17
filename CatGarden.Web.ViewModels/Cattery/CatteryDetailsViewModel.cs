using CatGarden.Web.ViewModels.Cat;
using CatGarden.Web.ViewModels.ImageGallery;
using CatGarden.Web.ViewModels.Review;

namespace CatGarden.Web.ViewModels.Cattery
{
    public class CatteryDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        public DateTime EstablishmentDate { get; set; }
        public List<ImageModel> Images { get; set; } = new List<ImageModel>();
        public List<CatWithAdoptionApplicationViewModel> Cats { get; set; } = new List<CatWithAdoptionApplicationViewModel>();

        public List<ReviewDisplayViewModel> Reviews { get; set; } = new List<ReviewDisplayViewModel>();
    }
}
