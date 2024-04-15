using Microsoft.AspNetCore.Http;
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
        public string Name { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;
        public bool IsCover { get; set; }
        public int? CatId { get; set; }
        public int? CatteryId { get; set; } 
    }
}
    