using ecommerce.EntityFramework;
using ecommerce.Models;
using ecommerce.utils;
using Microsoft.AspNetCore.Mvc;
using ecommerce.service;


namespace ecommerce.Controller;

[ApiController]
[Route("/categories")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(AppDbContext appDbContext)
    {
        _categoryService = new CategoryService(appDbContext);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            var categories = await _categoryService.GetCategories();
            if (categories.Count() <= 0)
            {
                return ApiResponse.NotFound("There is no categories found");
            }

            return ApiResponse.Success(
                categories,
                "All categories inside E-commerce system are returned"
            );
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        try
        {
            var foundCategory = await _categoryService.GetCategoryById(id);
            if (foundCategory == null)
            {
                return ApiResponse.NotFound("There is no category found matching");
            }

            return ApiResponse.Success(foundCategory, "Category are returned successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(CategoryModel newCategory)
    {
        try
        {
            var AddCategory = await _categoryService.AddCategory(newCategory);
            return ApiResponse.Created(AddCategory, "The category is Added");
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, CategoryModel updateData)
    {
        try
        {
            var found = await _categoryService.UpdateCategoryName(id, updateData);
            if (found == null)
            {
                return ApiResponse.NotFound("The category not found");
            }
            return ApiResponse.Success(found, "Category are updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        try
        {
            var deleted = await _categoryService.DeleteCategory(id);
            if (!deleted)
            {
                return ApiResponse.NotFound("The category not found");
            }
            return ApiResponse.Success(id, "Category are Deleted successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
        }
    }
}
