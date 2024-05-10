using ecommerce.EntityFramework;
using ecommerce.Middleware;
using ecommerce.Models;
using ecommerce.service;
using ecommerce.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Controller;

[ApiController]
[Route("/categories")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetCategories();
        if (!categories.Any())
        {
            throw new NotFoundException("There is no categories found");
        }

        return ApiResponse.Success(
            categories,
            "All categories inside E-commerce system are returned"
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var foundCategory =
            await _categoryService.GetCategoryById(id)
            ?? throw new NotFoundException("There is no category found matching");

        return ApiResponse.Success(foundCategory, "Category are returned successfully");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> AddCategory(CategoryModel newCategory)
    {
        var addCategory = await _categoryService.AddCategory(newCategory);
        return ApiResponse.Created(addCategory, "The category is Added");
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> UpdateCategory(Guid id, CategoryModel updateData)
    {
        var found =
            await _categoryService.UpdateCategoryName(id, updateData)
            ?? throw new NotFoundException("The category not found");

        return ApiResponse.Success(found, "Category are updated successfully");
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var deleted = await _categoryService.DeleteCategory(id);
        if (!deleted)
        {
            throw new NotFoundException("The category not found");
        }
        return ApiResponse.Success(id, "Category are Deleted successfully");
    }
}
