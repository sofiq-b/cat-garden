using CatGarden.Web.ViewModels.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatGarden.Services.Data.Models.Cat
{
    public class AllCatsFilteredAndPagedServiceModel
    {
        public int TotalCatsCount { get; set; }

        public IEnumerable<CatDisplayViewModel> Cats { get; set; } = new HashSet<CatDisplayViewModel>();   
    }
}
