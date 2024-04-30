using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.EF;
using ecommerce.Models;
using ecommerce.Tables;

public class ReviewService
{
    private readonly AppDbContext _appDbContext;

    public ReviewService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Review>> GetReviews()
    {
        await Task.CompletedTask;
        var reviews = _appDbContext.Reviews.ToList();
        return reviews;
    }

    public async Task<Review?> GetReviewById(Guid id)
    {
        await Task.CompletedTask;
        var reviewsDb = _appDbContext.Reviews.ToList();
        var foundReview = reviewsDb.FirstOrDefault(review => review.ReviewId == id);
        return foundReview;
    }

    public async Task<ReviewModel> AddReview(ReviewModel newReview)
    {
        await Task.CompletedTask;
        Review review = new Review
        {
            ReviewId = Guid.NewGuid(),
            Comment = newReview.Comment,
            ProductId = newReview.ProductId,
            UserId = newReview.UserId
        };
        _appDbContext.Reviews.Add(review);
        _appDbContext.SaveChanges();
        return newReview;
    }

    public async Task<Review?> UpdateReview(Guid id, ReviewModel updateReview)
    {
        await Task.CompletedTask;
        var reviewsDb = _appDbContext.Reviews.ToList();
        var foundReview = reviewsDb.FirstOrDefault(review => review.ReviewId == id);
        if (foundReview != null)
        {
            foundReview.Comment = updateReview.Comment;
        }
        _appDbContext.SaveChanges();
        return foundReview;
    }

    public async Task<bool> DeleteReview(Guid id)
    {
        await Task.CompletedTask;
        var ReviewDb = _appDbContext.Reviews.ToList();
        var foundReview = ReviewDb.FirstOrDefault(review => review.ReviewId == id);
        if (foundReview != null)
        {
            _appDbContext.Reviews.Remove(foundReview);
            _appDbContext.SaveChanges();
            return true;
        }
        return false;
    }
}
