using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatGarden.Web.ViewModels.Cat
{
    public class CatDisplayViewModel
    {
        public int Id { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public bool IsFavorite { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
