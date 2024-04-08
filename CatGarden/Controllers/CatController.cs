using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.Infrastructure.Extensions;
using CatGarden.Web.ViewModels.Cat;
using CatGarden.Web.ViewModels.Cattery;
using CatGarden.Web.ViewModels.ImageGallery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
           
            if (formModel.CoverPhoto != null)
            {
                string folder = "cats/cover/";
                formModel.CoverImageUrl = await imageService.UploadImage(folder, formModel.CoverPhoto);
            }

            if (formModel.ImageFiles != null)
            {
                string folder = "cats/gallery/";

                foreach (var file in formModel.ImageFiles)
                {
                    var gallery = new ImageModel()
                    {
                        Name = file.FileName,
                        URL = await imageService.UploadImage(folder, file)
                    };
                    formModel.Images.Add(gallery);
                }
            }

            try
            {
                int catId = await catService.CreateAndReturnIdAsync(formModel);

                TempData[SuccessMessage] = "Cat was added successfully!";
                return RedirectToAction("Details", "Cat", new { id = catId });
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new house!Pleasetryagainlater or contact administrator!");
                formModel.Catteries = await catteryService.AllCatteriesAsync(User.GetId()!);

                return View(formModel);
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
