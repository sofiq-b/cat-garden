using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatGarden.Web.ViewModels.ImageGallery
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public bool IsCover { get; set; }
        public int? CatId { get; set; }
        public int? CatteryId { get; set; }
    }
}
    