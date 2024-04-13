using CatGarden.Services.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using static CatGarden.Common.Enums;
using CatGarden.Data.Models;
using CatGarden.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public async Task<string> UploadImageAsync(string folderPath, IFormFile file, int entityId, EntityTypes entityType)
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

            // Determine the entity type and set the corresponding foreign key
            switch (entityType)
            {
                case EntityTypes.Cat:
                    image.CatId = entityId;
                    break;
                case EntityTypes.Cattery:
                    image.CatteryId = entityId;
                    break;
                default:
                    throw new ArgumentException("Invalid entity type");
            }

            // Add the new Image object to the context
            dbContext.Images.Add(image);

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return image.URL;
        }

        public async Task SetImageAsCoverAsync(int imageId)
        {
            var image = await dbContext.Images.FirstOrDefaultAsync(i => i.Id == imageId);

            if (image != null)
            {
                // Set the IsCover property to true for the selected image
                image.IsCover = true;

                // Save changes to the database
                await dbContext.SaveChangesAsync();
            }
            else
            {
                // Handle the case when the image with the specified ID is not found
                throw new ArgumentException("Image not found");
            }
        }



        public async Task<string> EditImageAsync(string folderPath, int imageId, IFormFile newFile, EntityTypes entityType, int entityId)
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
            string newImagePath = await UploadImageAsync(folderPath, newFile, entityId, entityType);

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

            // Remove the image entity from the associated entity's image list
            if (existingImage.CatId != null)
            {
                var cat = await dbContext.Cats.FindAsync(existingImage.CatId);
                cat.Images.Remove(existingImage);
            }
            else 
            {
                var cattery = await dbContext.Catteries.FindAsync(existingImage.CatteryId);
                cattery.Images.Remove(existingImage);
            }

            // Remove the image entity from the context
            dbContext.Images.Remove(existingImage);

            // Save changes to the database
            await dbContext.SaveChangesAsync();
        }


    }

}

