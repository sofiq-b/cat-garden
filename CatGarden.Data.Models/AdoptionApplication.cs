using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CatGarden.Common.Enums;

namespace CatGarden.Data.Models
{
    public class AdoptionApplication
    {
        public AdoptionApplication()
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
