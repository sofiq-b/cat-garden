using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CatGarden.Common.Enums;

namespace CatGarden.Data.Models
{
    public class Cat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public Breed Breed { get; set; }

        [Required]
        public int CatteryId { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        [ForeignKey(nameof(CatteryId))]
        public Cattery Cattery { get; set; } = null!;

        public string Location => Cattery.Location;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public AvailabilityStatus AvailabilityStatus { get; set; }

    }
}
