using ecommerce.EntityFramework;
using ecommerce.EntityFramework.Table;
using ecommerce.Middleware;
using ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.service;

public class ReviewService
{
    private readonly AppDbContext _appDbContext;

    public ReviewService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Review>> GetReviews()
    {
        var reviews = await _appDbContext
            .Reviews.Include(u => u.User)
            .Include(p => p.Product)
            .ToListAsync();
        return reviews;
    }

    public async Task<Review?> GetReviewById(Guid id)
    {
        var foundReview = await _appDbContext
            .Reviews.Include(u => u.User)
            .Include(p => p.Product)
            .FirstOrDefaultAsync(review => review.ReviewId == id);
        return foundReview;
    }

    public async Task<ReviewModel> AddReview(ReviewModel newReview)
    {
        Review review = new Review
        {
            ReviewId = Guid.NewGuid(),
            Comment = newReview.Comment,
            ProductId = newReview.ProductId,
            UserId = newReview.UserId
        };
        await _appDbContext.Reviews.AddAsync(review);
        await _appDbContext.SaveChangesAsync();
        return newReview;
    }

    public async Task<Review?> UpdateReview(Guid id, ReviewModel updateData, Guid userId)
    {
        var reviewToUpdate =
            await _appDbContext.Reviews.FindAsync(id)
            ?? throw new NotFoundException("The review was not found.");

        // Check if the user is authorized to update the review
        if (reviewToUpdate.UserId != userId)
        {
            throw new UnauthorizedAccessException("You are not authorized to update this review.");
        }
        // Update the review data
        reviewToUpdate.Comment = updateData.Comment;
        // Save changes to the database
        await _appDbContext.SaveChangesAsync();
        return reviewToUpdate;
    }

    public async Task<bool> DeleteReview(Guid id, Guid userId)
    {
        var reviewToDelete =
            await _appDbContext.Reviews.FindAsync(id)
            ?? throw new NotFoundException("The review was not found.");

        // Check if the user is authorized to delete the review
        if (reviewToDelete.UserId != userId)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this review.");
        }
        _appDbContext.Reviews.Remove(reviewToDelete);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}
