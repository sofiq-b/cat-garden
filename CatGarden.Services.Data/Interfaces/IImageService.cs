using Microsoft.AspNetCore.Http;

namespace CatGarden.Services.Data.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImage(string folderPath, IFormFile file);
    }
}
