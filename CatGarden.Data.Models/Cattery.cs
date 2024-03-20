using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CatGarden.Common.EntityValidationConstants.Cattery;

namespace CatGarden.Data.Models
{
    public class Cattery
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OwnerId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public DateTime EstablishmentDate { get; set; }

        [Required]
        public ContactInfo ContactInformation { get; set; } = null!;
        public IList<Cat> Cats { get; set; } = new List<Cat>();

        public IList<Review> Reviews { get; set; } = new List<Review>();

        public IList<AdoptionApplication> AdoptionApplications { get; set; } = new List<AdoptionApplication>();
    }
}
