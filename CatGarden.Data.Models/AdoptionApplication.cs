using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CatGarden.Common.Enums;

namespace CatGarden.Data.Models
{
    public class AdoptionApplication
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        [Required]
        public int CatId { get; set; }

        [Required]
        [ForeignKey(nameof(CatId))]
        public Cat Cat { get; set; } = null!;

        [Required]
        public DateTime ApplicationDate { get; set; }

        [Required]
        public ApplicationStatus ApplicationStatus { get; set; }
    }
}
