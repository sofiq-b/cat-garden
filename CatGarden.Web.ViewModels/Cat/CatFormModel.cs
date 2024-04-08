using CatGarden.Common;
using System.ComponentModel.DataAnnotations;
using static CatGarden.Common.Enums;
using static CatGarden.Common.EntityValidationConstants.Cat;
using static CatGarden.Common.GeneralApplicationConstants;
using CatGarden.Web.ViewModels.Cattery;
using Microsoft.AspNetCore.Http;
using CatGarden.Web.ViewModels.ImageGallery;

namespace CatGarden.Web.ViewModels.Cat
{
    public class CatFormModel
    {
        [Required(ErrorMessage = CatAttributeRequired)]
        [StringLength(NameMaxLength,
            MinimumLength = NameMinLength)]
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
        [Display(Name = "Coat Length")]
        public CoatLength CoatLength { get; set; }

        [Required(ErrorMessage = CatAttributeRequired)]
        [StringLength(DescriptionMaxLength,
            MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Required(ErrorMessage = CatAttributeRequired)]
        [Display(Name = "Choose the cover photo of your cat")]
        public IFormFile CoverPhoto { get; set; } = null!;
        public string CoverImageUrl { get; set; } = string.Empty;

        [Display(Name = "Choose the gallery images of your cat")]
        [Required]
        public IFormFileCollection ImageFiles { get; set; } = null!;

        public List<ImageModel> Images { get; set; } = new List<ImageModel>();

        [Required(ErrorMessage = "Please select a cattery.")]
        [Display(Name = "Cattery")]
        public int SelectedCatteryId { get; set; }

        public IEnumerable<CatteryViewForCatFormModel> Catteries { get; set; } = new List<CatteryViewForCatFormModel>();

        public AvailabilityStatus AvailabilityStatus { get; set; } = AvailabilityStatus.Available;
    }
}
