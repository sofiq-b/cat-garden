using CatGarden.Services.Data;
using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.Infrastructure.Extensions;
using CatGarden.Web.ViewModels.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static CatGarden.Common.NotificationMessagesConstants;

namespace CatGarden.Web.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {

        private readonly ICatteryService catteryService;
        private readonly ICatteryOwnerService catteryOwnerService;
        private readonly IReviewService reviewService;
        public ReviewController(ICatteryService catteryService, ICatteryOwnerService catteryOwnerService, IReviewService reviewService)
        {
            this.catteryService = catteryService;
            this.catteryOwnerService = catteryOwnerService;
            this.reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            string userId = User.GetId();

            if (userId == null)
            {
                TempData[ErrorMessage] = "User not logged in.";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var catteryExists = await catteryService.GetByIdAsync(id);
                if (catteryExists == null)
                {
                    TempData[ErrorMessage] = "Cattery not found.";
                    return RedirectToAction("All", "Cattery");
                }
                var userIsCatteryOwner = await catteryOwnerService.CatteryOwnerExistsByUserIdAsync(userId);
                if (userIsCatteryOwner)
                {
                    TempData[ErrorMessage] = "User is cattery owner.";
                    return RedirectToAction("All", "Cattery");
                }

                var viewModel = new ReviewFormViewModel
                {
                    CatteryId = id
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add(ReviewFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            string userId = User.GetId();

            if (userId == null)
            {
                TempData[ErrorMessage] = "User not logged in.";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var catteryExists = await catteryService.GetByIdAsync(viewModel.CatteryId);
                if (catteryExists == null)
                {
                    TempData[ErrorMessage] = "Cattery not found.";
                    return RedirectToAction("All", "Cattery");
                }
                var userIsCatteryOwner = await catteryOwnerService.CatteryOwnerExistsByUserIdAsync(userId);
                if (userIsCatteryOwner)
                {
                    TempData[ErrorMessage] = "User is cattery owner.";
                    return RedirectToAction("All", "Cattery");
                }

                // Add the review to the database
                await reviewService.AddReviewAsync(viewModel, userId);

                TempData[SuccessMessage] = "Review added successfully!";
                return RedirectToAction("Details", "Cattery", new { id = viewModel.CatteryId });
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
            var review = await reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                TempData[ErrorMessage] = "Review with the selected id doesn't exist.";
                return RedirectToAction("All", "Review");
            }

            if (!await reviewService.CanUserEditReviewAsync(userId, review.Id))
            {
                TempData[ErrorMessage] = "Unauthorized to edit review!";
                return RedirectToAction("All", "Review");
            }

            try
            {
                var model = await reviewService.LoadEditReviewAsync(id);
                return View(model);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReviewFormEditViewModel model)
        {
            string userId = User.GetId()!;
            var review = await reviewService.GetReviewByIdAsync(model.Id);
            if (review == null)
            {
                TempData[ErrorMessage] = "Review with the selected id doesn't exist.";
                return RedirectToAction("All", "Review");
            }

            if (!await reviewService.CanUserEditReviewAsync(userId, review.Id))
            {
                TempData[ErrorMessage] = "Unauthorized to edit review!";
                return RedirectToAction("All", "Review");
            }

            try
            {
                await reviewService.UpdateReviewAsync(model);
                TempData[SuccessMessage] = "Review updated successfully.";
                return RedirectToAction("Details", "Cattery", new { id = model.CatteryId });
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
