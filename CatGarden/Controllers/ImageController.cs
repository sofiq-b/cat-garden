using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.ViewModels.ImageGallery;
using CatGarden.Web.Infrastructure.Extensions;

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
