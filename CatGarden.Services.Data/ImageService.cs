using CatGarden.Data;
using CatGarden.Data.Models;
using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.ViewModels.ImageGallery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace CatGarden.Services.Data
{
    public class ImageService : IImageService
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly CatGardenDbContext dbContext;

        public ImageService(IWebHostEnvironment webHostEnvironment, CatGardenDbContext dbContext)
        {
            this.dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadImageAsync(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            var image = new Image
            {
                Name = file.FileName,
                URL = "/" + folderPath
            };

            dbContext.Images.Add(image);

            await dbContext.SaveChangesAsync();

            return image.URL;
        }

        public bool DeleteImageByNameAndCatId(string imageName, int catId)
        {
            try
            {
                var image = dbContext.Images.FirstOrDefault(i => i.Name == imageName && i.CatId == catId);

                if (image != null)
                {
                    dbContext.Images.Remove(image);
                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> EditImageAsync(string folderPath, int imageId, IFormFile newFile)
        {
            var existingImage = await dbContext.Images.FindAsync(imageId);

            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, existingImage!.URL.TrimStart('/'));
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            string newImagePath = await UploadImageAsync(folderPath, newFile);

            existingImage.Name = newFile.FileName;
            existingImage.URL = newImagePath;

            await dbContext.SaveChangesAsync();

            return newImagePath;
        }


        public async Task<int?> FindImageIdByFileNameAsync(string fileUrl)
        {
            try
            {
                int index = 0;
                if (fileUrl.Contains("catteries"))
                {
                    index = fileUrl.IndexOf("/catteries");
                }
                else
                {
                    index = fileUrl.IndexOf("/cats");
                }

                string relativeUrl = fileUrl.Substring(index);
                var image = await dbContext.Images.FirstOrDefaultAsync(i => i.URL == relativeUrl);

                return image?.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ImageModel>> GetCatImagesAsync(Cat cat)
        {
            var imagesData = await dbContext.Images.Where(i=>i.CatId == cat.Id).ToListAsync();

            var images = new List<ImageModel>();
            foreach (var imageData in imagesData)
            {
                var imageModel = new ImageModel
                {
                    Id = imageData.Id,
                    Name = imageData.Name,
                    URL = imageData.URL,
                    IsCover = imageData.IsCover,
                };
                images.Add(imageModel);
            }

            return images;
        }

        public async Task<List<ImageModel>> GetCatteryImagesAsync(Cattery cattery)
        {
            var imagesData = await dbContext.Images.Where(i => i.CatteryId == cattery.Id).ToListAsync();

            var images = new List<ImageModel>();
            foreach (var imageData in imagesData)
            {
                var imageModel = new ImageModel
                {
                    Id = imageData.Id,
                    Name = imageData.Name,
                    URL = imageData.URL,
                    IsCover = imageData.IsCover,
                };
                images.Add(imageModel);
            }
            return images;
        }


    }

}

