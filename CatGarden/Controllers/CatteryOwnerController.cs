using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CatGarden.Common.NotificationMessagesConstants;

namespace CatGarden.Web.Controllers
{
    [Authorize]
    public class CatteryOwnerController : Controller
    {
        private readonly ICatteryOwnerService catteryOwnerService;

        public CatteryOwnerController(ICatteryOwnerService catteryOwnerService)
        {
            this.catteryOwnerService = catteryOwnerService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();
            bool isCatteryOwner = await this.catteryOwnerService.CatteryOwnerExistsByUserId(userId);

            if (isCatteryOwner)
            {
                TempData[ErrorMessage] = "You are already a cattery owner!";

                return this.RedirectToAction("Index", "Home");

            }

            return View();
        }
    }
}
