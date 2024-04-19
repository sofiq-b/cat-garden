using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.Infrastructure.Extensions;
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
            IEnumerable<IndexViewModel> viewModel = await this.catService.LastThreeCatsAsync();

            string userId = User.GetId()!;
            bool isCatteryOwner = await catteryOwnerService.CatteryOwnerExistsByUserIdAsync(userId);

            if (isCatteryOwner)
            {
                ViewData["IsCatteryOwner"] = true;
            }
            else
            {
                ViewData["IsCatteryOwner"] = false;
            }
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
