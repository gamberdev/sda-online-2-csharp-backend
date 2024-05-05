using ecommerce.EntityFramework;
using ecommerce.EntityFramework.Table;
using ecommerce.Models;
using ecommerce.service;
using ecommerce.utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ecommerce.Controller;

public enum OrderBy
{
    ASC,
    DESC
}

public enum SortBy
{
    Name,
    Date
}

[ApiController]
[Route("/products")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(AppDbContext appDbContext)
    {
        _productService = new ProductService(appDbContext);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts(
        [FromQuery] int page = 1,
        [FromQuery] int limit = 5,
        [FromQuery] SortBy sortBy = SortBy.Date,
        [FromQuery] OrderBy orderBy = OrderBy.ASC,
        [FromQuery] string? keyword = null, //name of product or description
        [FromQuery] double? minPrice = 1,
        [FromQuery] double? maxPrice = 20000.00
    )
    {
        try
        {
            // Get all products from the service
            var products = await _productService.GetAllProducts();

            if (products.Count() <= 0)
            {
                return ApiResponse.NotFound("There are no Products");
            }

            // Apply filtering based on keyword and price range
             keyword = keyword!.ToLower();
            if (!string.IsNullOrEmpty(keyword))
            {
                products = products.Where(p =>
                    p.Name!.ToLower().Contains(keyword) || p.Description!.ToLower().Contains(keyword)
                );
            }

            if (minPrice.HasValue && minPrice < maxPrice && maxPrice.HasValue)
            {
                products = products.Where(p =>
                    p.Price >= minPrice.Value && p.Price <= maxPrice.Value
                );
            }

            // Apply sorting based on sort by and sort order
            switch (sortBy)
            {
                case SortBy.Name:
                    products =
                        orderBy == OrderBy.ASC
                            ? products.OrderBy(p => p.Name)
                            : products.OrderByDescending(p => p.Name);
                    break;
                default:
                    // Default sorting by date
                    products =
                        orderBy == OrderBy.ASC
                            ? products.OrderBy(p => p.CreatedAt)
                            : products.OrderByDescending(p => p.CreatedAt);
                    break;
            }

            // Apply pagination
            var paginatedProducts = products.Skip((page - 1) * limit).Take(limit).ToList();

            return ApiResponse.Success(
                paginatedProducts,
                "All products inside E-commerce system are returned"
            );
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        try
        {
            var foundProduct = await _productService.GetProductById(id);
            if (foundProduct == null)
            {
                return ApiResponse.NotFound("There is no product found matching");
            }

            return ApiResponse.Success(foundProduct, "Product are returned successfully");
        }
        catch (Exception ex)
        {
            Console.Write($"An error occurred while retrieving the product Id");
            return ApiResponse.ServerError(ex.Message);
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProducts(string searchKeyword)
    {
        try
        {
            // Retrieve products based on the search keyword
            var products = await _productService.SearchProducts(searchKeyword);

            if (products == null || !products.Any())
            {
                return ApiResponse.NotFound("No products found matching the search criteria.");
            }
            return ApiResponse.Success(products);
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductModel newProduct)
    {
        try
        {
            var AddProduct = await _productService.CreateProduct(newProduct);
            return ApiResponse.Created(AddProduct, "The product is Added");
        }
        catch (Exception ex)
        {
            Console.Write($"An error occurred while creating the product");
            return ApiResponse.ServerError(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, ProductModel updatedProduct)
    {
        try
        {
            var found = await _productService.UpdateProduct(id, updatedProduct);
            if (found == null)
            {
                return ApiResponse.NotFound("The product not found");
            }

            return ApiResponse.Success(found, "Product are updated successfully");
        }
        catch (Exception ex)
        {
            Console.Write($"An error occurred while updating the product");

            return ApiResponse.ServerError(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var deleted = await _productService.DeleteProduct(id);
            if (!deleted)
            {
                return ApiResponse.NotFound("The product not found");
            }
            return ApiResponse.Success(id, "product are Deleted successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
        }
    }
}
