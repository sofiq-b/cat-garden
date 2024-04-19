using CatGarden.Services.Data.Interfaces;
using CatGarden.Services.Data.Models.Cat;
using CatGarden.ViewModels.Cat;
using CatGarden.Web.Infrastructure.Extensions;
using CatGarden.Web.ViewModels.Cat;
using CatGarden.Web.ViewModels.Cattery;
using CatGarden.Web.ViewModels.ImageGallery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        public async Task<IActionResult> All([FromQuery]AllCatsQueryModel queryModel)
        {
            string userId = User.GetId()!;
            AllCatsFilteredAndPagedServiceModel serviceModel = await this.catService.AllAsync(queryModel, userId);

            queryModel.Cats = serviceModel.Cats;
            queryModel.TotalCats = serviceModel.TotalCatsCount;
            queryModel.Catteries = await this.catteryService.AllCatteryNamesAsync();

            if (queryModel.TotalCats == 0)
            {
                return View("NoCatsFound");
            }

            return this.View(queryModel);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            string userId = User.GetId()!;

            bool isCatteryOwner =
                await catteryOwnerService.CatteryOwnerExistsByUserIdAsync(User.GetId()!);
            if (!isCatteryOwner)
            {
                TempData[ErrorMessage] = "You must become a cattery owner in order to add new cats!";

                return RedirectToAction("Become", "CatteryOwner");
            }

            IEnumerable<CatteryViewForCatFormModel> ownedCatteries = await catteryService.OwnedCatteriesAsync(userId);

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
                formModel.Catteries = await catteryService.OwnedCatteriesAsync(User.GetId()!);
                return View(formModel);
            }
            var uploadedImages = HttpContext.Session.Get<List<ImageModel>>("UploadedImages");
            if (!uploadedImages.Any())
            {
                TempData[ErrorMessage] = "Please upload at least one image before submitting the form.";
                formModel.Catteries = await catteryService.OwnedCatteriesAsync(User.GetId()!);
                return View(formModel);
            }

            try
            {
                formModel.Images = uploadedImages;

                int catId = await catService.InsertImagesAndReturnCatIdAsync(formModel);
                TempData[SuccessMessage] = "Cat was added successfully!";
                HttpContext.Session.Remove("UploadedImages");

                return RedirectToAction("Details", "Cat", new { id = catId });
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
                return BadRequest();
            }

            var isFavorite = await catService.IsFavoritedByUserWithIdAsync(catId, userId);
            if (!isFavorite)
            {
                TempData[ErrorMessage] = "Unauthorized to remove cat from favorites!";
                return BadRequest();
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
            await catService.RemoveAllCatsFromFavoritesAsync(userId);

            return RedirectToAction("Favorites", "Cat");
        }


        public async Task<IActionResult> Favorites()
        {
            string userId = User.GetId()!;

            if (await catteryOwnerService.CatteryOwnerExistsByUserIdAsync(userId))
            {
                TempData[ErrorMessage] = "Favorites tab is inaccessible to cattery owners.";

                return RedirectToAction("All", "Cat");
            }
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
                return BadRequest();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CatFormEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingCat = await catService.GetByIdAsync(model.Id);

            if (existingCat == null)
            {
                TempData[ErrorMessage] = "Cat with the selected id doesn't exist.";
                return RedirectToAction("All", "Cat");
            }

            string userId = User.GetId()!;

            var isOwnedByUser = await catService.IsCatPartOfOwnedCattery(model.Id, userId);
            if (!isOwnedByUser)
            {
                TempData[ErrorMessage] = "Unauthorized to edit cat!";
                return BadRequest();
            }

            try
            {
                await catService.UpdateCatAsync(model);
                TempData[SuccessMessage] = "Cat updated successfully.";
                return RedirectToAction("Details", "Cat", new { id = model.Id });
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isOwnedByUser = await catService.IsCatPartOfOwnedCattery(id, User.GetId()!);
            if (!isOwnedByUser)
            {
                TempData[ErrorMessage] = "Unauthorized to delete cat!";
                return BadRequest();
            }
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
