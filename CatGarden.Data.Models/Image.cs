using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatGarden.Data.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;

        public bool isCover { get; set; }
        public int? CatId { get; set; }

        [ForeignKey(nameof(CatId))]
        public Cat? Cat { get; set; }

        
        public int? CatteryId { get; set; }

       
        [ForeignKey(nameof(CatteryId))]
        public Cattery? Cattery { get; set; }
    }
}
