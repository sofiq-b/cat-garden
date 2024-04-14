using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static CatGarden.Common.Enums;

namespace CatGarden.Services.Data.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(string folderPath, IFormFile file );
        Task<string> EditImageAsync(string folderPath, int imageId, IFormFile newFile); 
        Task DeleteImageAsync(int imageId);
    }
}
