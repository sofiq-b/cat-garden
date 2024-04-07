using CatGarden.Common;
using System.ComponentModel.DataAnnotations;
using static CatGarden.Common.Enums;
using static CatGarden.Common.EntityValidationConstants.Cat;
using static CatGarden.Common.GeneralApplicationConstants;
using CatGarden.Web.ViewModels.Cattery;

namespace CatGarden.Web.ViewModels.Cat
{
    public class CatFormModel
    {
        [Required(ErrorMessage = CatAttributeRequired)]
        [StringLength(NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = CatAttributeRequired)]
        [Range(AgeMinLength, AgeMaxLength, ErrorMessage = "The age must be between 0 and 30.")]
        public int Age { get; set; }

        [Required(ErrorMessage = CatAttributeRequired)]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = CatAttributeRequired)]
        public Breed Breed { get; set; }

        [Required(ErrorMessage = CatAttributeRequired)]
        public Color Color { get; set; }

        [Required(ErrorMessage = CatAttributeRequired)]
        public CoatLength CoatLength { get; set; }

        [Required(ErrorMessage = CatAttributeRequired)]
        [StringLength(DescriptionMaxLength,
            MinimumLength = DescriptionMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Required(ErrorMessage = CatAttributeRequired)]
        [Url(ErrorMessage = "Invalid URL format.")]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a cattery.")]
        public int SelectedCatteryId { get; set; }

        public List<CatteryViewForCatFormModel> Catteries { get; set; } = new List<CatteryViewForCatFormModel>();

        public AvailabilityStatus AvailabilityStatus { get; set; } = AvailabilityStatus.Available;
    }
}
