using CatGarden.Services.Data;
using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.Infrastructure.Extensions;
using CatGarden.Web.ViewModels.Image;
using CatGarden.Web.ViewModels.ImageGallery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CatGarden.Web.Controllers
{
    public class ImageController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IImageService imageService;
        private readonly ICatService catService;

        public ImageController(IWebHostEnvironment webHostEnvironment, IImageService imageService, ICatService catService)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.imageService = imageService;
            this.catService = catService;
        }   

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";

                string tempFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "images", "temp").Replace('\\', '/');

                Directory.CreateDirectory(tempFolderPath);

                string tempFilePath = Path.Combine(tempFolderPath, uniqueFileName).Replace('\\', '/'); 

                // Save the uploaded file to the temporary location within the wwwroot folder
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }


                // Retrieve uploaded images from session or create a new list if it doesn't exist
                var uploadedImages = HttpContext.Session.Get<List<ImageModel>>("UploadedImages") ?? new List<ImageModel>();

                // Add the new image to the list
                uploadedImages.Add(new ImageModel { Name = file.FileName, URL = tempFilePath, IsCover = false, CatId = null, CatteryId = null });

                // Store the updated list back in the session
                HttpContext.Session.Set("UploadedImages", uploadedImages);

                // Return the generated image URL
                return Ok(tempFilePath);
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
                // Iterate through each image model
                foreach (var imageModel in uploadedImages)
                {
                    // If the image matches the request, update its IsCover property
                    if (imageModel.Name == request.Name && imageModel.URL == request.Url)
                    {
                        imageModel.IsCover = request.IsCover;
                    }
                    else if (request.IsCover) // If the request sets IsCover to true
                    {
                        // Set IsCover to false for all other images
                        imageModel.IsCover = false;
                    }
                }

                // Store the updated uploaded images back in the session
                HttpContext.Session.Set("UploadedImages", uploadedImages);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


        [HttpPost]
        public IActionResult RemoveImageModel([FromBody] RemoveImageRequest request)
        {
            try
            {
                // Retrieve uploaded images from session
                var uploadedImages = HttpContext.Session.Get<List<ImageModel>>("UploadedImages");
                if (uploadedImages != null)
                {
                    // Find the index of the corresponding ImageModel object
                    var index = uploadedImages.FindIndex(image => image.Name == request.Name && image.URL == request.Url);
                    if (index != -1)
                    {
                        // Get the path of the image file
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, request.Url);

                        // Delete the image file if it exists
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        // Remove the ImageModel from the array
                        uploadedImages.RemoveAt(index);
                        // Store the updated uploaded images back in the session
                        HttpContext.Session.Set("UploadedImages", uploadedImages);
                        return Json(new { success = true });
                    }
                }
                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetImageInfo(string folderPath)
        {
            // Check if folderPath is provided and valid
            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            {
                return BadRequest("Invalid folder path.");
            }

            // Get all files in the specified folder
            string[] fileNames = Directory.GetFiles(folderPath);
            for (int i = 0; i < fileNames.Length; i++)
            {
                // Replace backslashes with forward slashes in each file path
                fileNames[i] = fileNames[i].Replace('\\', '/');
            }
            var fileInfoList = new List<object>();

            // Iterate through each file to get its name and size
            foreach (var fileName in fileNames)
            {
                // Get the serverID asynchronously
                var serverID = await imageService.FindImageIdByFileNameAsync($"{folderPath}/{Path.GetFileName(fileName)}");

                var fileInfo = new
                {
                    name = Path.GetFileName(fileName),
                    size = new FileInfo(fileName).Length,
                    serverID = serverID
                };
                fileInfoList.Add(fileInfo);
            }


            // Return the file info list as JSON response
            return Json(fileInfoList);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveServerImage([FromBody] RemoveImageRequest request)
        {
            try
            {
                // Extract cat ID from folder information
                int catId;
                if (int.TryParse(Regex.Match(request.Url, @"\d+").Value, out catId))
                {
                    var cat = await catService.GetByIdAsync(catId);
                    var image = cat.Images.FirstOrDefault(img => img.Name == request.Name);
                    if (image != null && image.IsCover==true)
                    {
                        // Return error response if image deletion from the database failed
                        return Json(new { success = false, error = "Cover image cannot be removed." });
                    }
                    // Delete the image from the database
                    bool isDeleted = imageService.DeleteImageByNameAndCatId(request.Name, catId);

                    if (isDeleted)
                    {
                        // Get the path of the image file
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, request.Url)
                            .Replace("\\", "/")
                            .Replace("/", "/");

                        // Delete the image file if it exists
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        // Return success response
                        return Json(new { success = true });
                    }
                    else
                    {
                        // Return error response if image deletion from the database failed
                        return Json(new { success = false, error = "Failed to delete image from the database." });
                    }
                }
                else
                {
                    // Return error response if cat ID extraction failed
                    return Json(new { success = false, error = "Invalid folder information." });
                }
            }
            catch (Exception ex)
            {
                // Return error response for any exception occurred
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIsCoverForServer([FromBody] UpdateIsCoverRequest request)
        {
            try
            {
                // Extract cat ID from folder information
                int catId;
                if (int.TryParse(Regex.Match(request.Url, @"\d+").Value, out catId))
                {
                    // Get the cat from the database
                    var cat = await catService.GetByIdAsync(catId); 

                    if (cat != null)
                    {
                        // Find the image in the cat's images by name
                        var image = cat.Images.FirstOrDefault(img => img.Name == request.Name);

                        if (image != null)
                        {
                            // Update the image's isCover property accordingly
                            if (image.IsCover == false)
                            {
                                image.IsCover = true;
                                foreach (var otherImage in cat.Images.Where(img => img != image))
                                {
                                    otherImage.IsCover = false;
                                }
                                var uploadedImages = HttpContext.Session.Get<List<ImageModel>>("UploadedImages");
                                if (uploadedImages != null)
                                {
                                    // Iterate through each image model
                                    foreach (var imageModel in uploadedImages)
                                    {
                                        imageModel.IsCover = false;
                                    }
                                    HttpContext.Session.Set("UploadedImages", uploadedImages);
                                }

                            }
                            else
                            {
                                image.IsCover = false;
                            }



                            await imageService.UpdateImageAsync(image);

                            // Return success response
                            return Json(new { success = true });
                        }
                    }
                    else
                    {
                        // Return error response if cat with the specified ID was not found
                        return Json(new { success = false, error = "Cat not found." });
                    }
                }
                else
                {
                    // Return error response if cat ID extraction failed
                    return Json(new { success = false, error = "Invalid folder information." });
                }
            }
            catch (Exception ex)
            {
                // Return error response for any exception occurred
                return Json(new { success = false, error = ex.Message });
            }

            // Return error response if no action was taken
            return Json(new { success = false, error = "Image not found." });
        }




    }
}
