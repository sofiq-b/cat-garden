using CatGarden.Data.Models;
using CatGarden.Web.ViewModels.ImageGallery;
using Microsoft.AspNetCore.Http;

namespace CatGarden.Services.Data.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(string folderPath, IFormFile file );
        Task<string> EditImageAsync(string folderPath, int imageId, IFormFile newFile);
        bool DeleteImageByNameAndCatId(string imageName, int catId);
        Task UpdateImageAsync(Image image);
        Task<int?> FindImageIdByFileNameAsync(string fileUrl);
        Task<List<ImageModel>> GetCatImagesAsync(Cat cat);
        Task DeleteImageAsync(int imageId);
    }
}
