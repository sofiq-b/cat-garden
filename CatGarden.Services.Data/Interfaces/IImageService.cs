using Microsoft.AspNetCore.Http;

namespace CatGarden.Services.Data.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(string folderPath, IFormFile file, int catId);
        Task<string> EditImageAsync(string folderPath, int imageId, IFormFile newFile);
        Task DeleteImageAsync(int imageId);
    }
}
