using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatGarden.Data.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;

        [Required]
        public int CatId { get; set; }

        [Required]
        [ForeignKey(nameof(CatId))]
        public Cat Cat { get; set; } = null!;
    }
}
