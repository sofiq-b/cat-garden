using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.Infrastructure.Extensions;
using CatGarden.Web.ViewModels.Cattery;
using CatGarden.Web.ViewModels.ImageGallery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static CatGarden.Common.NotificationMessagesConstants;

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
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
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
            catch (Exception ex)
            {
                return HandleException(ex);
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
                formModel.Images = uploadedImages;

                int catteryId = await catteryService.InsertImagesAndReturnCatteryIdAsync(formModel, User.GetId());
                TempData[SuccessMessage] = "Cattery was added successfully!";
                HttpContext.Session.Remove("UploadedImages");

                return RedirectToAction("Details", "Cattery", new { id = catteryId });
            }
            catch (Exception ex)
            {
                return HandleException(ex);
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
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string userId = User.GetId()!;

            var model = await catteryService.LoadEditCatteryAsync(id);

            if (model == null)
            {
                TempData[ErrorMessage] = "Cattery with the selected id doesn't exist.";

                return RedirectToAction("All", "Cattery");
            }
            var isOwnedByUser = await catteryService.IsCatteryOwnedByUserAsync(userId, id);
            if (!isOwnedByUser)
            {
                TempData[ErrorMessage] = "Unauthorized to edit cattery!";
                return BadRequest();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CatteryFormEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingCattery = await catteryService.GetByIdAsync(model.Id);

            if (existingCattery == null)
            {
                TempData[ErrorMessage] = "Cattery with the selected id doesn't exist.";
                return RedirectToAction("All", "Cattery");
            }

            string userId = User.GetId()!;

            var isOwnedByUser = await catteryService.IsCatteryOwnedByUserAsync(userId, model.Id);
            if (!isOwnedByUser)
            {
                TempData[ErrorMessage] = "Unauthorized to edit cattery!";
                return BadRequest();
            }

            try
            {
                await catteryService.UpdateCatteryAsync(model);
                TempData[SuccessMessage] = "Cattery updated successfully.";
                return RedirectToAction("Details", "Cattery", new { id = model.Id });
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isOwnedByUser = await catteryService.IsCatteryOwnedByUserAsync(User.GetId()!,id);
            if (!isOwnedByUser)
            {
                TempData[ErrorMessage] = "Unauthorized to delete cattery!";
                return BadRequest();
            }
            try
            {
                var isDeleted = await catteryService.DeleteCatteryAsync(id);
                TempData[SuccessMessage] = "Cattery was deleted successfully!";
                return RedirectToAction("All", "Cattery");
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
                HttpStatusCode statusCode = ((HttpWebResponse)we.Response).StatusCode;

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
