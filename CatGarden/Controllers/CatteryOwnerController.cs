using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.Infrastructure.Extensions;
using CatGarden.Web.ViewModels.CatteryOwner;
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
            bool isCatteryOwner = await this.catteryOwnerService.CatteryOwnerExistsByUserIdAsync(userId);

            if (isCatteryOwner)
            {
                this.TempData[ErrorMessage] = "You are already a cattery owner!";

                return this.RedirectToAction("Index", "Home");

            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeCatteryOwnerFormModel model)
        {
            string? userId = this.User.GetId();
            bool isCatteryOwner = await this.catteryOwnerService.CatteryOwnerExistsByUserIdAsync(userId);

            if (isCatteryOwner)
            {
                this.TempData[ErrorMessage] = "You are already a cattery owner!";

                return this.RedirectToAction("Index", "Home");

            }

            bool isPhoneNumberTaken = await this.catteryOwnerService.CatteryOwnerExistsByPhoneNumberAsync(model.PhoneNumber);
            if (isPhoneNumberTaken)
            {
                this.ModelState.AddModelError(nameof(model.PhoneNumber), "Cattery owner with the provided phone number already exists!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await this.catteryOwnerService.Create(userId, model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error occured while registering you as a cattery owner! Please try again later!";

                return this.RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction("All", "Cat");
        }
    }
}
