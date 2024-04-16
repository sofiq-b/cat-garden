using Microsoft.AspNetCore.Mvc;
using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.ViewModels.Cattery;
using CatGarden.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using System.Linq;
using static CatGarden.Common.NotificationMessagesConstants;
using CatGarden.Web.ViewModels.Cat;
using CatGarden.Services.Data;
using CatGarden.Web.ViewModels.ImageGallery;
using CatGarden.ViewModels.Cat;
using System.Net.Mail;
using System.Net;

namespace CatGarden.Web.Controllers
{
    [Authorize]
    public class CatteryController : Controller
    {
        private readonly ICatteryService catteryService;
        private readonly ICatteryOwnerService catteryOwnerService;

        public CatteryController(ICatteryService catteryService, ICatteryOwnerService catteryOwnerService)
        {
            this.catteryService = catteryService;
            this.catteryOwnerService = catteryOwnerService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            string userId = User.GetId()!;
            var allCatteries = await catteryService.AllCatteriesAsync();

            if (!allCatteries.Any())
            {
                return View("NoCatteriesFound");
            }

            return View(allCatteries);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isCatteryOwner = await catteryOwnerService.CatteryOwnerExistsByUserIdAsync(User.GetId()!);

            if (!isCatteryOwner)
            {
                TempData[ErrorMessage] = "You must become a cattery owner in order to add new catteries!";

                return RedirectToAction("Become", "CatteryOwner");
            }

            try
            {
                CatteryFormModel formModel = new CatteryFormModel();

                return View(formModel);
            }
            catch (WebException we)
            {
                // Get the status code from the HTTP response
                HttpStatusCode statusCode = ((HttpWebResponse)we.Response).StatusCode;

                // Redirect to the error handler action with the status code
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = (int)statusCode });
            }
            catch (Exception)
            {
                return GeneralError();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add(CatteryFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return View(formModel);
            }

            bool isCatteryOwner = await catteryOwnerService.CatteryOwnerExistsByUserIdAsync(User.GetId()!);

            if (!isCatteryOwner)
            {
                TempData[ErrorMessage] = "You must become a cattery owner in order to add new catteries!";

                return RedirectToAction("Become", "CatteryOwner");
            }

            var uploadedImages = HttpContext.Session.Get<List<ImageModel>>("UploadedImages");

            if (!uploadedImages.Any())
            {
                TempData[ErrorMessage] = "Please upload at least one image before submitting the form.";
                return View(formModel);
            }

            try
            {
                // Associate the image URLs with the cat model
                formModel.Images = uploadedImages;

                int catteryId = await catteryService.InsertImagesAndReturnCatteryIdAsync(formModel, User.GetId());
                TempData[SuccessMessage] = "Cattery was added successfully!";
                // Remove temporarily stored image data
                HttpContext.Session.Remove("UploadedImages");

                return RedirectToAction("Details", "Cattery", new { id = catteryId });
            }
            catch (WebException we)
            {
                // Get the status code from the HTTP response
                HttpStatusCode statusCode = ((HttpWebResponse)we.Response).StatusCode;

                // Redirect to the error handler action with the status code
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = (int)statusCode });
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            bool catteryExists = await catteryService
                .ExistsByIdAsync(id);
            if (!catteryExists)
            {
                TempData[ErrorMessage] = "Cattery with the provided id does not exist!";

                return RedirectToAction("All", "Cattery");
            }

            try
            {
                CatteryDetailsViewModel viewModel = await catteryService
                    .GetDetailsByIdAsync(id);
                return View(viewModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

        


        private IActionResult GeneralError()
        {
            TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator";
            return RedirectToAction("Index", "Home");

        }
    }
}
