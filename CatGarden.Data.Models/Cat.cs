using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CatGarden.Common.Enums;
using static CatGarden.Common.EntityValidationConstants.Cat;

namespace CatGarden.Data.Models
{
    public class Cat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(AgeMaxLength)]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; } 

        [Required]
        public Breed Breed { get; set; }

        [Required]
        public Color Color { get; set; }

        [Required]
        public CoatLength CoatLength { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public int CatteryId { get; set; }

        [Required]
        [ForeignKey(nameof(CatteryId))]
        public Cattery Cattery { get; set; } = null!;

        [Required]
        public AvailabilityStatus AvailabilityStatus { get; set; }

        public Guid? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        public IList<UserFavCat> UserFavCats { get; set; } = new List<UserFavCat>();

        public IList<AdoptionApplication> AdoptionApplications { get; set; } = new List<AdoptionApplication>();
    }
}
