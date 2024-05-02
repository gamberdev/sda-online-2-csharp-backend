using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.EF;
using ecommerce.Models;
using ecommerce.Tables;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Review?> UpdateReview(Guid id, ReviewModel updateReview)
    {
        var reviewsDb = await _appDbContext.Reviews.ToListAsync();
        var foundReview = reviewsDb.FirstOrDefault(review => review.ReviewId == id);
        if (foundReview != null)
        {
            foundReview.Comment = updateReview.Comment;
        }
        await _appDbContext.SaveChangesAsync();
        return foundReview;
    }

    public async Task<bool> DeleteReview(Guid id)
    {
        await Task.CompletedTask;
        var ReviewDb = await _appDbContext.Reviews.ToListAsync();
        var foundReview = ReviewDb.FirstOrDefault(review => review.ReviewId == id);
        if (foundReview != null)
        {
            _appDbContext.Reviews.Remove(foundReview);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
