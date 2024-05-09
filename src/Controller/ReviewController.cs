using ecommerce.EntityFramework;
using ecommerce.Models;
using ecommerce.service;
using ecommerce.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controller;

[ApiController]
[Route("/reviews")]
public class ReviewController : ControllerBase
{
    private readonly ReviewService _reviewService;

    public ReviewController(AppDbContext appDbContext)
    {
        _reviewService = new ReviewService(appDbContext);
    }

    [HttpGet]
    public async Task<IActionResult> GetReviews()
    {
        try
        {
            var reviews = await _reviewService.GetReviews();
            if (reviews.Count() <= 0)
            {
                return ApiResponse.NotFound("There is no Reviews");
            }
            return ApiResponse.Success(reviews, "All Reviews inside E-commerce system");
        }
        catch (Exception)
        {
            return ApiResponse.ServerError("There is an error on getting the Reviews");
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetReviewById(Guid id)
    {
        try
        {
            var foundReview = await _reviewService.GetReviewById(id);
            if (foundReview == null)
            {
                return ApiResponse.BadRequest("The Review not found");
            }
            return ApiResponse.Success(foundReview, "Review Detail");
        }
        catch (Exception)
        {
            return ApiResponse.ServerError("There is an error on getting the Review");
        }
    }

    [HttpPost]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> AddReview(ReviewModel newReview)
    {
        try
        {
            await _reviewService.AddReview(newReview);
            return ApiResponse.Created(newReview, "The Review is Added");
        }
        catch (Exception)
        {
            return ApiResponse.ServerError("Cannot add the Review");
        }
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> UpdateReview(Guid id, ReviewModel updateData)
    {
        try
        {
            var found = await _reviewService.UpdateReview(id, updateData);
            if (found == null)
            {
                return ApiResponse.NotFound("The Review not found");
            }
            return ApiResponse.Success(found, "Review updated");
        }
        catch (Exception)
        {
            return ApiResponse.ServerError("There is an error on updating Review");
        }
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> DeleteReview(Guid id)
    {
        try
        {
            var deleted = await _reviewService.DeleteReview(id);
            if (!deleted)
            {
                return ApiResponse.NotFound("The Review not found");
            }
            return ApiResponse.Success(id, "Review Deleted");
        }
        catch (Exception)
        {
            return ApiResponse.ServerError("There is an error on deleting Review");
        }
    }
}
