using ecommerce.EF;
using ecommerce.Models;
using ecommerce.Tables;
using ecommerce.utils;
using Microsoft.AspNetCore.Mvc;

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
                return NotFound(new ErrorResponse { Message = "There is no categories" });
            }
            return Ok(
                new SuccessResponse<IEnumerable<Category>>
                {
                    Message = "All categories inside E-commerce system",
                    Data = categories
                }
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ErrorResponse { Message = "There is an error on getting the categories" }
            );
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
                return BadRequest(new ErrorResponse { Message = "The category not found" });
            }
            return Ok(
                new SuccessResponse<Category> { Message = "Category Detail", Data = foundCategory }
            );
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ErrorResponse { Message = "There is an error on getting the category" }
            );
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(CategoryModel newCategory)
    {
        try
        {
            await _categoryService.AddCategory(newCategory);
            return Ok(new SuccessResponse<CategoryModel> { Message = "The category is Added" });
        }
        catch (Exception)
        {
            return StatusCode(500, new ErrorResponse { Message = "Cannot add the category" });
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
                return NotFound(new ErrorResponse { Message = "The category not found" });
            }
            return Ok(new SuccessResponse<Category> { Message = "Category updated", Data = found });
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ErrorResponse { Message = "There is an error on updating category" }
            );
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
                return NotFound(new ErrorResponse { Message = "The category not found" });
            }
            return Ok(new SuccessResponse<bool> { Message = "Category Deleted" });
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ErrorResponse { Message = "There is an error on deleting category" }
            );
        }
    }
}
