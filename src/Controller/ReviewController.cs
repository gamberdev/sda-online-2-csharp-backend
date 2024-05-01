using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.EF;
using ecommerce.Models;
using ecommerce.Tables;
using ecommerce.utils;
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
                return NotFound(new ErrorResponse { Message = "There is no Reviews" });
            }
            return Ok(
                new SuccessResponse<IEnumerable<Review>>
                {
                    Message = "All Reviews inside E-commerce system",
                    Data = reviews
                }
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ErrorResponse { Message = "There is an error on getting the Reviews" }
            );
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReviewById(Guid id)
    {
        try
        {
            var foundReview = await _reviewService.GetReviewById(id);
            if (foundReview == null)
            {
                return BadRequest(new ErrorResponse { Message = "The Review not found" });
            }
            return Ok(
                new SuccessResponse<Review> { Message = "Review Detail", Data = foundReview }
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ErrorResponse { Message = "There is an error on getting the Review" }
            );
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddReview(ReviewModel newReview)
    {
        try
        {
            await _reviewService.AddReview(newReview);
            return Ok(new SuccessResponse<ReviewModel> { Message = "The Review is Added" });
        }
        catch (Exception)
        {
            return StatusCode(500, new ErrorResponse { Message = "Cannot add the Review" });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReview(Guid id, ReviewModel updateData)
    {
        try
        {
            var found = await _reviewService.UpdateReview(id, updateData);
            if (found == null)
            {
                return NotFound(new ErrorResponse { Message = "The Review not found" });
            }
            return Ok(new SuccessResponse<Review> { Message = "Review updated", Data = found });
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ErrorResponse { Message = "There is an error on updating Review" }
            );
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(Guid id)
    {
        try
        {
            var deleted = await _reviewService.DeleteReview(id);
            if (!deleted)
            {
                return NotFound(new ErrorResponse { Message = "The Review not found" });
            }
            return Ok(new SuccessResponse<bool> { Message = "Review Deleted" });
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ErrorResponse { Message = "There is an error on deleting Review" }
            );
        }
    }
}
