using CatGarden.Data.Models;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
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
            _webHostEnvironment = webHostEnvironment;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Get the current user's ID
            string userId = User.GetId()!;

            // Check if the user is a cattery owner
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
           
            

            try
            {
                int catId = await catService.CreateAndReturnIdAsync(formModel);

                TempData[SuccessMessage] = "Cat was added successfully!";


                if (formModel.CoverPhoto != null)
                {
                    string folder = "cats/cover/";
                    formModel.CoverImageUrl = await imageService.UploadImageAsync(folder, formModel.CoverPhoto, catId);
                }

                if (formModel.ImageFiles != null)
                {
                    string folder = "cats/gallery/";

                    foreach (var file in formModel.ImageFiles)
                    {
                        var gallery = new ImageModel()
                        {
                            Name = file.FileName,
                            URL = await imageService.UploadImageAsync(folder, file, catId)
                        };
                        formModel.Images.Add(gallery);
                    }
                }
                return RedirectToAction("Details", "Cat", new { id = catId });
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new cat! Please try again later or contact administrator!");
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
            bool catExists = await catService
                .ExistsByIdAsync(catId);
            if (!catExists)
            {
                TempData[ErrorMessage] = "Cat with the provided id does not exist!";

                return RedirectToAction("All", "Cat");
            }

            // Get the current user's Id
            string userId = User.GetId()!;

            if (await catteryOwnerService.CatteryOwnerExistsByUserIdAsync(userId))
            {
                TempData[ErrorMessage] = "Favorites tab is inaccessible to cattery owners.";

                return RedirectToAction("All", "Cat");
            }

            // Check if the cat is already in the user's favorites
            var isFavorite = await catService.IsFavoritedByUserWithIdAsync(catId, userId);

            if (!isFavorite)
            {
                // Add the cat to the user's favorites
                await catService.AddCatToFavoritesAsync(catId, userId);

                return RedirectToAction("Favorites", "Cat");

            }

            return RedirectToAction("RemoveFavorite", "Cat");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(int catId)
        {
            bool catExists = await catService.ExistsByIdAsync(catId);
            if (!catExists)
            {
                TempData[ErrorMessage] = "Cat with the provided id does not exist!";
                return RedirectToAction("All", "Cat");
            }
            string userId = User.GetId()!;

            if (await catteryOwnerService.CatteryOwnerExistsByUserIdAsync(userId))
            {
                TempData[ErrorMessage] = "Favorites tab is inaccessible to cattery owners.";

                return RedirectToAction("All", "Cat");
            }
            // Check if the cat is favorited by the user
            var isFavorite = await catService.IsFavoritedByUserWithIdAsync(catId, userId);
            if (!isFavorite)
            {
                TempData[ErrorMessage] = "Unauthorized to remove cat from favorites!";

                return RedirectToAction("All", "Cat");
            }
            await catService.RemoveFavoriteAsync(catId, userId);
            return RedirectToAction("Favorites", "Cat");
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
            // Retrieve the cat details by ID
            var catDetails = await catService.GetCatForEdit(id, userId);

            // Check if the cat exists
            if (catDetails == null)
            {
                return BadRequest(); // or return NotFound() if you prefer
            }

            // Check if the current user has permission to edit the cat
            if (!await catService.IsCatPartOfOwnedCattery(id, userId))
            {
                return Unauthorized();
            }
            IEnumerable<CatteryViewForCatFormModel> ownedCatteries = await catteryService.AllCatteriesAsync(userId);
            // Populate the view model with the cat details
            var model = new CatFormModel()
            {
                Name = catDetails.Name,
                Age = catDetails.Age,
                Gender = catDetails.Gender,
                Breed = catDetails.Breed,
                Color = catDetails.Color,
                CoatLength = catDetails.CoatLength,
                Description = catDetails.Description,
                SelectedCatteryId = catDetails.SelectedCatteryId,
                Catteries = ownedCatteries // Assuming Catteries property is populated in GetCatForEdit method
            };

            return View(model);
        }



        private IActionResult GeneralError()
        {
            TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later or contact administrator";

            return RedirectToAction("Index", "Home");
        }
    }
}
