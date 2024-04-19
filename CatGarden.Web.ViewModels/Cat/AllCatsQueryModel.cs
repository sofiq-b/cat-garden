using CatGarden.Web.ViewModels.Cat.Enums;
using System.ComponentModel.DataAnnotations;
using static CatGarden.Common.Enums;
using static CatGarden.Common.GeneralApplicationConstants;

namespace CatGarden.Web.ViewModels.Cat
{
    public class AllCatsQueryModel
    {
        public AllCatsQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.CatsPerPage = EntitiesPerPage;
        }
        public string? Cattery { get; set; }
        [Display(Name = "Search by text")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Cats By")]
        public CatSorting CatSorting { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Cats Per Page")]
        public int CatsPerPage { get; set; }

        public int TotalCats { get; set; }

        public Breed? Breed { get; set; }

        public Gender? Gender { get; set; }

        public Color? Color { get; set; }

        [Display(Name = "Coat Length")]
        public CoatLength? CoatLength { get; set; }

        public City? City { get; set; }

        public IEnumerable<string> Catteries { get; set; } = new HashSet<string>();

        public IEnumerable<CatDisplayViewModel> Cats { get; set; } = new HashSet<CatDisplayViewModel>();    

    }
}
