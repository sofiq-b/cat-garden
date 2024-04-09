using CatGarden.Services.Data;
using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.Infrastructure.Extensions;
using CatGarden.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
                // User is a cattery owner
                ViewData["IsCatteryOwner"] = true;
            }
            else
            {
                // User is not a cattery owner
                ViewData["IsCatteryOwner"] = false;
            }
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
