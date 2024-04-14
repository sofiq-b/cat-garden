using CatGarden.Services.Data.Interfaces;
using System.Text.Json;

using CatGarden.Web.Infrastructure.Extensions;
using CatGarden.Web.ViewModels.Image;
using CatGarden.Web.ViewModels.ImageGallery;
using Microsoft.AspNetCore.Mvc;

namespace CatGarden.Web.Controllers
{
    public class ImageController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageService;

        public ImageController(IWebHostEnvironment webHostEnvironment, IImageService imageService)
        {
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
        }

        [HttpPost]
        public IActionResult UploadImage(IFormFile file)
        {
            try
            {
                // Generate a unique filename
                string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";

                // Define the folder path where the image will be stored
                string folderPath = "images/";

                // Combine the folder path and the unique filename to create the full URL
                string imageUrl = $"{folderPath}{uniqueFileName}";

                // Retrieve uploaded images from session or create a new list if it doesn't exist
                var uploadedImages = HttpContext.Session.Get<List<ImageModel>>("UploadedImages") ?? new List<ImageModel>();

                // Add the new image to the list
                uploadedImages.Add(new ImageModel { Name = file.FileName, URL = imageUrl, IsCover = false, CatId = null, CatteryId = null });

                // Store the updated list back in the session
                HttpContext.Session.Set("UploadedImages", uploadedImages);

                // Return the generated image URL
                return Ok(imageUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        public IActionResult UpdateIsCoverForFile([FromBody] UpdateIsCoverRequest request)
        {
            // Retrieve uploaded images from session
            var uploadedImages = HttpContext.Session.Get<List<ImageModel>>("UploadedImages");
            if (uploadedImages != null)
            {
                // Find the corresponding ImageModel object and update its isCover property
                var imageModel = uploadedImages.Find(image => image.Name == request.Name && image.URL == request.Url);
                if (imageModel != null)
                {
                    imageModel.IsCover = request.IsCover;
                    // Store the updated uploaded images back in the session
                    HttpContext.Session.Set("UploadedImages", uploadedImages);
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult RemoveImageModel([FromBody] RemoveImageRequest request)
        {
            // Retrieve uploaded images from session
            var uploadedImages = HttpContext.Session.Get<List<ImageModel>>("UploadedImages");
            if (uploadedImages != null)
            {
                // Find the index of the corresponding ImageModel object
                var index = uploadedImages.FindIndex(image => image.Name == request.Name && image.URL == request.Url);
                if (index != -1)
                {
                    // Remove the ImageModel from the array
                    uploadedImages.RemoveAt(index);
                    // Store the updated uploaded images back in the session
                    HttpContext.Session.Set("UploadedImages", uploadedImages);
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }

        [HttpPut("{imageId}")]
        public async Task<IActionResult> Edit(int imageId, IFormFile newFile)
        {
            try
            {
                if (newFile == null || newFile.Length == 0)
                    return BadRequest("Invalid file");

                string folderPath = "images/";

                string newImageUrl = await _imageService.EditImageAsync(folderPath, imageId, newFile);

                return Ok(newImageUrl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{imageId}")]
        public async Task<IActionResult> Delete(int imageId)
        {
            try
            {
                await _imageService.DeleteImageAsync(imageId);
                return Ok("Image deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
