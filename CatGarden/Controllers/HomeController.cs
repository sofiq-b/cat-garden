using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CatGarden.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatService catService;

        public HomeController(ICatService catService)
        {
            this.catService = catService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexViewModel> viewModel = await this.catService.LastThreeCatsAsync();
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
