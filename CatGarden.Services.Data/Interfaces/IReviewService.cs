using CatGarden.Data.Models;
using CatGarden.Web.ViewModels.Review;

namespace CatGarden.Services.Data.Interfaces
{
    public interface IReviewService
    {
        Task AddReviewAsync(ReviewFormViewModel reviewForm, string userId);
        Task<ReviewFormViewModel> LoadEditReviewAsync(int reviewId);
        Task UpdateReviewAsync(ReviewFormEditViewModel model);
        Task<ReviewFormEditViewModel> GetReviewForDeleteAsync(int reviewId);
        Task<bool> DeleteReviewAsync(int reviewId);

        Task<bool> CanUserEditReviewAsync(string userId, int reviewId);
        Task<Review> GetReviewByIdAsync(int id);
    }
}
