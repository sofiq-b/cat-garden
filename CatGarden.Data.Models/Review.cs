using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatGarden.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CatteryId { get; set; }

        [Required]
        [ForeignKey(nameof(CatteryId))]
        public Cattery Cattery { get; set; } = null!;

        [Required]
        public string UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        [Required]
        [Range(0.0, 5.0)] 
        public double Rating { get; set; } 


        public string? Comment { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }
    }
}
