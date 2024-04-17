using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.Infrastructure.Extensions;
using CatGarden.Web.ViewModels.Image;
using CatGarden.Web.ViewModels.ImageGallery;
using Microsoft.AspNetCore.Mvc;

namespace CatGarden.Web.Controllers
{
    public class ImageController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IImageService imageService;

        public ImageController(IWebHostEnvironment webHostEnvironment, IImageService imageService)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.imageService = imageService;
           
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

                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }


                var uploadedImages = HttpContext.Session.Get<List<ImageModel>>("UploadedImages") ?? new List<ImageModel>();

                uploadedImages.Add(new ImageModel { Name = file.FileName, URL = tempFilePath, IsCover = false, CatId = null, CatteryId = null });

                HttpContext.Session.Set("UploadedImages", uploadedImages);

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
            var uploadedImages = HttpContext.Session.Get<List<ImageModel>>("UploadedImages");
            if (uploadedImages != null)
            {
                foreach (var imageModel in uploadedImages)
                {
                    if (imageModel.Name == request.Name && imageModel.URL == request.Url)
                    {
                        imageModel.IsCover = request.IsCover;
                    }
                    else if (request.IsCover)
                    {
                        imageModel.IsCover = false;
                    }
                }

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
                var uploadedImages = HttpContext.Session.Get<List<ImageModel>>("UploadedImages");
                if (uploadedImages != null)
                {
                    var index = uploadedImages.FindIndex(image => image.Name == request.Name && image.URL == request.Url);
                    if (index != -1)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, request.Url);

                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        uploadedImages.RemoveAt(index);
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
            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            {
                return BadRequest("Invalid folder path.");
            }

            string[] fileNames = Directory.GetFiles(folderPath);
            for (int i = 0; i < fileNames.Length; i++)
            {
                
                fileNames[i] = fileNames[i].Replace('\\', '/');
            }
            var fileInfoList = new List<object>();

            foreach (var fileName in fileNames)
            {
                var serverID = await imageService.FindImageIdByFileNameAsync($"{folderPath}/{Path.GetFileName(fileName)}");

                var fileInfo = new
                {
                    name = Path.GetFileName(fileName),
                    size = new FileInfo(fileName).Length,
                    serverID = serverID
                };
                fileInfoList.Add(fileInfo);
            }


            return Json(fileInfoList);
        }
    }
}
