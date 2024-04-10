using CatGarden.Services.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using CatGarden.Data.Models;
using CatGarden.Data;


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

        public async Task<string> UploadImageAsync(string folderPath, IFormFile file, int catId)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            // Create a new Image entity object
            var image = new Image
            {
                Name = file.FileName,
                URL = "/" + folderPath,
                CatId = catId
            };

            // Add the new Image object to the context
            dbContext.Images.Add(image);

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return "/" + folderPath;
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
            string newImagePath = await UploadImageAsync(folderPath, newFile, existingImage.CatId);

            // Update the properties of the existing image entity with the new image information
            existingImage.Name = newFile.FileName;
            existingImage.URL = newImagePath;

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return newImagePath;
        }

        public async Task DeleteImageAsync(int imageId)
        {
            // Retrieve the existing image entity from the database
            var existingImage = await dbContext.Images.FindAsync(imageId);

            // Delete the existing image file from the server
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, existingImage.URL.TrimStart('/'));
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            // Remove the image entity from the context
            dbContext.Images.Remove(existingImage);

            // Save changes to the database
            await dbContext.SaveChangesAsync();
        }

    }
}
