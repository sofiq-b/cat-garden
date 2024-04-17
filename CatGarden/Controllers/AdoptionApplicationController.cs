using CatGarden.Services.Data;
using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static CatGarden.Common.NotificationMessagesConstants;

namespace CatGarden.Web.Controllers
{
    [Authorize]
    public class AdoptionApplicationController : Controller
    {
        private readonly ICatService catService;
        private readonly ICatteryService catteryService;
        private readonly ICatteryOwnerService catteryOwnerService;
        private readonly IAdoptionApplicationService adoptionApplicationService;
        public AdoptionApplicationController(ICatService catService, ICatteryService catteryService, ICatteryOwnerService catteryOwnerService, IAdoptionApplicationService adoptionApplicationService)
        {
            this.catService = catService;
            this.catteryService = catteryService;
            this.catteryOwnerService = catteryOwnerService;
            this.adoptionApplicationService = adoptionApplicationService;
        }

        [HttpPost]
        public async Task<IActionResult> SendApplication(int catId)
        {

            string userId = User.GetId();

            if (userId == null)
            {
                TempData[ErrorMessage] = "User not logged in.";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var cat = await catService.GetByIdAsync(catId);
                if (cat == null)
                {
                    TempData[ErrorMessage] = "Cat not found.";
                    return RedirectToAction("All", "Cat");
                }
                var userIsCatteryOwner = await catteryOwnerService.CatteryOwnerExistsByUserIdAsync(userId);
                if (userIsCatteryOwner)
                {
                    TempData[ErrorMessage] = "User is cattery owner.";
                    return RedirectToAction("All", "Cattery");
                }
                if (await adoptionApplicationService.HasUserAlreadySentApplicationForCat(new Guid(userId), catId))
                {
                    TempData[ErrorMessage] = "Adoption application for this cat has already been sent.";
                    return RedirectToAction("Details", "Cat", new { id = catId });
                }
                await adoptionApplicationService.SendApplication(new Guid(userId), catId);
                TempData["SuccessMessage"] = "Adoption application sent successfully!";
                return RedirectToAction("MyApplications", "AdoptionApplication");
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> MyApplications()
        {
            var userId = User.GetId();

            if (userId == null)
            {
                TempData[ErrorMessage] = "User not logged in.";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                // Call the service method to retrieve the adoption applications of the user
                var applications = await adoptionApplicationService.GetUserAdoptionApplications(new Guid(userId));
                if (applications == null || applications.Count == 0)
                {
                    return View("NoAdoptionApplications");
                }
                return View(applications);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
           
        }

        private IActionResult HandleException(Exception ex)
        {
            if (ex is WebException we)
            {
                // Get the status code from the HTTP response
                HttpStatusCode statusCode = ((HttpWebResponse)we.Response).StatusCode;

                // Redirect to the error handler action with the status code
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = (int)statusCode });
            }
            else
            {
                TempData[ErrorMessage] = "Unexpected error occurred! Please try again later or contact administrator";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
