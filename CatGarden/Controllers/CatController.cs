using CatGarden.Services.Data.Interfaces;
using CatGarden.ViewModels.Cat;
using CatGarden.Web.Infrastructure.Extensions;
using CatGarden.Web.ViewModels.Cat;
using CatGarden.Web.ViewModels.Cattery;
using CatGarden.Web.ViewModels.ImageGallery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CatGarden.Common.NotificationMessagesConstants;

namespace CatGarden.Web.Controllers
{
    [Authorize]
    public class CatController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICatteryService catteryService;
        private readonly ICatteryOwnerService catteryOwnerService;
        private readonly ICatService catService;
        private readonly IImageService imageService;


        public CatController(ICatteryService catteryService, ICatteryOwnerService catteryOwnerService, ICatService catService, IImageService imageService, IWebHostEnvironment webHostEnvironment)
        {
            this.catteryService = catteryService;
            this.catteryOwnerService = catteryOwnerService;
            this.catService = catService;
            this.imageService = imageService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            string userId = User.GetId()!;

            var allCats = await catService.GetAllCatsAsync(userId);

            if (!allCats.Any())
            {
                return View("NoCatsFound");
            }

            return View(allCats);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            string userId = User.GetId()!;

            bool isCatteryOwner =
                await catteryOwnerService.CatteryOwnerExistsByUserIdAsync(User.GetId()!);
            //If user is not cattery owner redirect to become one
            if (!isCatteryOwner)
            {
                TempData[ErrorMessage] = "You must become a cattery owner in order to add new cats!";

                return RedirectToAction("Become", "CatteryOwner");
            }

            // If the user is a cattery owner, check if they have any owned catteries
            IEnumerable<CatteryViewForCatFormModel> ownedCatteries = await catteryService.AllCatteriesAsync(userId);

            // If the cattery owner doesn't have any catteries, display an error message or redirect them
            if (!ownedCatteries.Any())
            {
                TempData[ErrorMessage] = "You must register a cattery before adding cats!";
                return RedirectToAction("Add", "Cattery"); 
            }

            try
            {
                CatFormModel formModel = new CatFormModel()
                {
                    Catteries = ownedCatteries
                };

                return View(formModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(CatFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                formModel.Catteries = await catteryService.AllCatteriesAsync(User.GetId()!);
                return View(formModel);
            }
            var uploadedImages = HttpContext.Session.Get<List<ImageModel>>("UploadedImages");
            if (!uploadedImages.Any())
            {
                TempData[ErrorMessage] = "Please upload at least one image before submitting the form.";
                formModel.Catteries = await catteryService.AllCatteriesAsync(User.GetId()!);
                return View(formModel);
            }

            try
            {
                // Associate the image URLs with the cat model
                formModel.Images = uploadedImages;

                int catId = await catService.InsertImagesAndReturnCatIdAsync(formModel);
                TempData[SuccessMessage] = "Cat was added successfully!";
                // Remove temporarily stored image data
                HttpContext.Session.Remove("UploadedImages");

                return RedirectToAction("Details", "Cat", new { id = catId });
            }
            catch (Exception ex)
            {
                
                TempData[ErrorMessage] = ex.ToString();
                // Delete cat images folder and image entities
               
                formModel.Catteries = await catteryService.AllCatteriesAsync(User.GetId()!);
                return View(formModel);
            }
        }




        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            bool catExists = await catService
                .ExistsByIdAsync(id);
            if (!catExists)
            {
                TempData[ErrorMessage] = "Cat with the provided id does not exist!";

                return RedirectToAction("All", "Cat");
            }

            try
            {
                CatDetailsViewModel viewModel = await catService
                    .GetDetailsByIdAsync(id, User.GetId()!);
                return View(viewModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(int catId)
        {
            bool catExists = await catService.ExistsByIdAsync(catId);
            if (!catExists)
            {
                TempData[ErrorMessage] = "Cat with the provided id does not exist!";
                return NotFound();
            }

            string userId = User.GetId()!;

            if (await catteryOwnerService.CatteryOwnerExistsByUserIdAsync(userId))
            {
                TempData[ErrorMessage] = "Favorites tab is inaccessible to cattery owners.";
                return Forbid();
            }

            var isFavorite = await catService.IsFavoritedByUserWithIdAsync(catId, userId);

            if (isFavorite)
            {
                await catService.RemoveFavoriteAsync(catId, userId);

                var cat = await catService.GetByIdAsync(catId);
                return Json(new { LikesCount = cat.LikesCount });
            }
            else
            {
                await catService.AddCatToFavoritesAsync(catId, userId);

                var cat = await catService.GetByIdAsync(catId);
                return Json(new { LikesCount = cat.LikesCount });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(int catId)
        {
            bool catExists = await catService.ExistsByIdAsync(catId);
            if (!catExists)
            {
                TempData[ErrorMessage] = "Cat with the provided id does not exist!";
                return NotFound();
            }

            string userId = User.GetId()!;

            if (await catteryOwnerService.CatteryOwnerExistsByUserIdAsync(userId))
            {
                TempData[ErrorMessage] = "Favorites tab is inaccessible to cattery owners.";
                return Forbid();
            }

            var isFavorite = await catService.IsFavoritedByUserWithIdAsync(catId, userId);
            if (!isFavorite)
            {
                TempData[ErrorMessage] = "Unauthorized to remove cat from favorites!";
                return Forbid();
            }

            await catService.RemoveFavoriteAsync(catId, userId);

            var cat = await catService.GetByIdAsync(catId);
            return Json(new { LikesCount = cat.LikesCount });
        }

        [HttpPost]
        public async Task<IActionResult> ClearFavorites()
        {
            string userId = User.GetId()!;

            if (await catteryOwnerService.CatteryOwnerExistsByUserIdAsync(userId))
            {
                TempData[ErrorMessage] = "Favorites tab is inaccessible to cattery owners.";

                return RedirectToAction("All", "Cat");
            }
            // Call service method to remove all cats from favorites
            await catService.RemoveAllCatsFromFavoritesAsync(userId);

            return RedirectToAction("Favorites", "Cat");
        }


        public async Task<IActionResult> Favorites()
        {
            // Get the current user's ID
            string userId = User.GetId()!;

            if (await catteryOwnerService.CatteryOwnerExistsByUserIdAsync(userId))
            {
                TempData[ErrorMessage] = "Favorites tab is inaccessible to cattery owners.";

                return RedirectToAction("All", "Cat");
            }
            // Retrieve the favorite cats of the user
            var favoriteCats = await catService.GetFavoriteCatsAsync(userId);

            if (!favoriteCats.Any())
            {
                return View("NoCatsInFavorites");
                
            }
            return View(favoriteCats);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string userId = User.GetId()!;
            // Load data for editing the cat
            var model = await catService.LoadEditCatAsync(id, userId);

            if (model == null)
            {
                TempData[ErrorMessage] = "Cat with the selected id doesn't exist.";

                return RedirectToAction("All", "Cat");
            }
            var isOwnedByUser = await catService.IsCatPartOfOwnedCattery(id, userId);
            if (!isOwnedByUser)
            {
                TempData[ErrorMessage] = "Unauthorized to edit cat!";
                return Unauthorized();
            }

            return View(model);
        }


        


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await catService.DeleteCatAsync(id);

            if (isDeleted)
            {
                TempData[SuccessMessage] = "Cat was deleted successfully!";
                return RedirectToAction(nameof(All));
            }
            else
            {
                TempData[ErrorMessage] = "Cat not found or deletion failed!";
                return RedirectToAction(nameof(All));
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
