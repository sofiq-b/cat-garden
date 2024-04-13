using CatGarden.Services.Data;
using CatGarden.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static CatGarden.Common.Enums;

namespace CatGarden.Web.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int entityId, string entityType, IFormFile file, string folderPath)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }

            if (!Enum.TryParse(entityType, out EntityTypes entityTypeValue))
            {
                return BadRequest("Invalid entity type");
            }

            var imageUrl = await imageService.UploadImageAsync(folderPath,file, entityId, entityTypeValue);

            return Ok(new { imageUrl });
        }
        [HttpPost]
        public async Task<IActionResult> SetAsCover(int imageId)
        {
            try
            {
                // Call a service method to update the IsCover property of the image
                await imageService.SetImageAsCoverAsync(imageId);

                // Optionally, return a success response
                return Ok(new { message = "Image set as cover successfully" });
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return StatusCode(500, new { message = "An error occurred while setting image as cover", error = ex.Message });
            }
        }


    }

}
