using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CatGarden.Common.EntityValidationConstants.Cattery;
using static CatGarden.Common.Enums;

namespace CatGarden.Data.Models
{
    public class Cattery
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid OwnerId { get; set; } 

        [Required]
        [ForeignKey(nameof(OwnerId))]
        public CatteryOwner Owner { get; set; } = null!;

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public City City { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public DateTime EstablishmentDate { get; set; }

        public IList<Cat> Cats { get; set; } = new List<Cat>();

        public IList<Review> Reviews { get; set; } = new List<Review>();

    }
}
