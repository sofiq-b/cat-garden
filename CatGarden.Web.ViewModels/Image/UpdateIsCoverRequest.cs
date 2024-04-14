using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatGarden.Web.ViewModels.Image
{
    public class UpdateIsCoverRequest
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsCover { get; set; }
    }
}
