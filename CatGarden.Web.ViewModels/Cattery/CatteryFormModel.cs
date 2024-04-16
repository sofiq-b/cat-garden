using System.ComponentModel.DataAnnotations;
using CatGarden.Common;
using static CatGarden.Common.Enums;
using static CatGarden.Common.EntityValidationConstants.Cattery;
using CatGarden.Web.ViewModels.ImageGallery;


namespace CatGarden.Web.ViewModels.Cattery
{
    public class CatteryFormModel
    {
        [Required]
        [StringLength(NameMaxLength, 
            MinimumLength = NameMinLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(AddressMaxLength,
            MinimumLength = AddressMinLength)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public City City { get; set; }

        [Display(Name = "Establishment Date")]
        [DataType(DataType.Date)]
        public DateTime EstablishmentDate { get; set; } = DateTime.Today; 
        public List<ImageModel> Images { get; set; } = new List<ImageModel>();
    }
}
