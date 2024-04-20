using CatGarden.Services.Data.Interfaces;
using static CatGarden.Common.GeneralApplicationConstants;
using CatGarden.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;


namespace CatGarden.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatService catService;
        private readonly ICatteryOwnerService catteryOwnerService;
        public HomeController(ICatService catService, ICatteryOwnerService catteryOwnerService)
        {
            this.catService = catService;
            this.catteryOwnerService = catteryOwnerService;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User.IsInRole(AdminRoleName))
            {
                return this.RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }

            IEnumerable<IndexViewModel> viewModel = await this.catService.LastThreeCatsAsync();

            
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400 || statusCode == 404)
            {
                return View("Error404");
            }

            if (statusCode == 401)
            {
                return View("Error401");
            }

            return View();
        }
    }
}
