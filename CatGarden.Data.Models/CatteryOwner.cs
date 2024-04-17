using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CatGarden.Common.EntityValidationConstants.CatteryOwner;

namespace CatGarden.Data.Models
{
    public class CatteryOwner
    {
        public CatteryOwner()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = string.Empty;

        public IList<Cattery> Catteries { get; set; } = new List<Cattery>();
    }
}
