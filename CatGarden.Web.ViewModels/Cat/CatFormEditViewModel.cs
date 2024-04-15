using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatGarden.Web.ViewModels.Cat
{
    public class CatFormEditViewModel :CatFormModel
    {
        public int Id { get; set; }
        public string FolderPathUrl { get; set; } = string.Empty;
    }
}
