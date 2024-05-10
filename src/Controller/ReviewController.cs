using System.Security.Claims;
using ecommerce.EntityFramework;
using ecommerce.Middleware;
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

    public ReviewController(ReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet]
    public async Task<IActionResult> GetReviews()
    {
        var reviews = await _reviewService.GetReviews();
        if (!reviews.Any())
        {
            throw new NotFoundException("There is no Reviews");
        }

        return ApiResponse.Success(reviews, "All Reviews inside E-commerce system");
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetReviewById(Guid id)
    {
        var foundReview =
            await _reviewService.GetReviewById(id)
            ?? throw new NotFoundException("The Review not found");

        return ApiResponse.Success(foundReview, "Review Detail");
    }

    [HttpPost]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> AddReview(ReviewModel newReview)
    {
        //identify id depend on the login user
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        newReview.UserId = Guid.Parse(userIdString!);

        await _reviewService.AddReview(newReview);
        return ApiResponse.Created(newReview, "The Review is Added");
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> UpdateReview(Guid id, ReviewModel updateData)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Guid userId = Guid.Parse(userIdString!);
        var found = await _reviewService.UpdateReview(id, updateData, userId);

        return ApiResponse.Success(found, "Review updated");
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> DeleteReview(Guid id)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Guid userId = Guid.Parse(userIdString!);
        await _reviewService.DeleteReview(id, userId);
        return ApiResponse.Success(id, "Review Deleted");
    }
}
