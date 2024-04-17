using CatGarden.Data;
using CatGarden.Data.Models;
using CatGarden.Services.Data.Interfaces;
using CatGarden.Web.ViewModels.Review;
using Microsoft.AspNetCore.Hosting;

namespace CatGarden.Services.Data
{
    public class ReviewService : IReviewService
    {
        private readonly CatGardenDbContext dbContext;
        private readonly ICatteryService catteryService;
        public ReviewService(CatGardenDbContext dbContext, ICatteryService catteryService)
        {
            this.dbContext = dbContext;
            this.catteryService = catteryService;
        }

        public async Task AddReviewAsync(ReviewFormViewModel reviewForm, string userId)
        {
            var cattery = await catteryService.GetByIdAsync(reviewForm.CatteryId);
            var user = dbContext.Users.FirstOrDefault(x => x.Id.ToString() == userId);
            try
            {
                var review = new Review
                {
                    UserId = new Guid(userId),
                    User = user,
                    CatteryId = reviewForm.CatteryId,
                    Cattery = cattery,
                    Rating = reviewForm.Rating,
                    Comment = reviewForm.Comment,
                    DatePosted = DateTime.Now
                };

                await dbContext.AddAsync(review);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle any exceptions, log or throw as needed
                Console.WriteLine($"Error occurred while adding review: {ex.Message}");
                throw; // Optionally rethrow the exception for the caller to handle
            }
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await dbContext.Reviews.FindAsync(id);
        }

        public async Task<bool> CanUserEditReviewAsync(string userId, int reviewId)
        {
            var review = await GetReviewByIdAsync(reviewId);
            return review.UserId.ToString() == userId;
        }

        public async Task<ReviewFormViewModel> LoadEditReviewAsync(int reviewId)
        {
            var review = await GetReviewByIdAsync(reviewId);

            var model = new ReviewFormEditViewModel()
            {
                Id = review.Id,
                CatteryId = review.CatteryId,
                Rating = review.Rating,
                Comment = review.Comment
            };

            return model;
        }
        public async Task UpdateReviewAsync(ReviewFormEditViewModel model)
        {
            var existingReview = await GetReviewByIdAsync(model.Id);

            existingReview.Rating = model.Rating;
            existingReview.Comment = model.Comment;

            await dbContext.SaveChangesAsync();
        }

        public async Task<ReviewFormEditViewModel> GetReviewForDeleteAsync(int reviewId)
        {
            var review = await GetReviewByIdAsync(reviewId);

            var viewModel = new ReviewFormEditViewModel
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                CatteryId = review.CatteryId 
            };

            return viewModel;
        }


        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            var review = await GetReviewByIdAsync(reviewId);

            dbContext.Remove(review);
            await dbContext.SaveChangesAsync();

            return true;
        }

    }
}
