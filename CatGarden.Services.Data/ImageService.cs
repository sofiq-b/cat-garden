using CatGarden.Services.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using static CatGarden.Common.Enums;
using CatGarden.Data.Models;
using CatGarden.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using CatGarden.Web.ViewModels.ImageGallery;


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

            // Create a new Image entity object
            var image = new Image
            {
                Name = file.FileName,
                URL = "/" + folderPath
            };

            // Add the new Image object to the context
            dbContext.Images.Add(image);

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return image.URL;
        }

        public bool DeleteImageByNameAndCatId(string imageName, int catId)
        {
            try
            {
                // Find the image by name and catId
                var image = dbContext.Images.FirstOrDefault(i => i.Name == imageName && i.CatId == catId);

                if (image != null)
                {
                    // Remove the image from the database
                    dbContext.Images.Remove(image);
                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    // Image not found
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw ex;
            }
        }

        public async Task<string> EditImageAsync(string folderPath, int imageId, IFormFile newFile)
        {
            // Retrieve the existing image entity from the database
            var existingImage = await dbContext.Images.FindAsync(imageId);

            // Delete the existing image file from the server
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, existingImage!.URL.TrimStart('/'));
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            // Upload the new image file
            string newImagePath = await UploadImageAsync(folderPath, newFile);

            // Update the properties of the existing image entity with the new image information
            existingImage.Name = newFile.FileName;
            existingImage.URL = newImagePath;

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return newImagePath;
        }


        public async Task UpdateImageAsync(Image image)
        {
            // Ensure the image entity is tracked by the context
            dbContext.Entry(image).State = EntityState.Modified;

            try
            {
                // Save changes to the database
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle exception, if necessary
                throw; // Or handle the exception and return false or throw a custom exception
            }
        }


        public async Task DeleteImageAsync(int imageId)
        {
            // Retrieve the existing image entity from the database
            var existingImage = await dbContext.Images.FindAsync(imageId);

            if (existingImage != null)
            {
                // Remove the image entity from the context
                dbContext.Images.Remove(existingImage);

                // Save changes to the database
                await dbContext.SaveChangesAsync();
            }
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

                // Extract the portion of the URL starting from "/cats"
                string relativeUrl = fileUrl.Substring(index);
                // Find the image by file name in the database
                var image = await dbContext.Images.FirstOrDefaultAsync(i => i.URL == relativeUrl);

                // Return the image ID if found, otherwise return null
                return image?.Id;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw ex;
            }
        }

        public async Task<List<ImageModel>> GetCatImagesAsync(Cat cat)
        {
            var imagesData = await dbContext.Images.Where(i=>i.CatId == cat.Id).ToListAsync();

            // Map the data to ImageModel objects
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

            // Map the data to ImageModel objects
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

