using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;
using ecommerce.EntityFramework;
using ecommerce.EntityFramework.Table;
using ecommerce.utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ecommerce.service;


namespace ecommerce.Controller
{
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
        public async Task<IActionResult> GetAllProducts([FromQuery] int page = 1, [FromQuery] int limit = 2)
        {
            try
            {
                var products = await _productService.GetAllProducts();
                if (products.Count() <= 0)
                {
                    return ApiResponse.NotFound("There is no products found");
                }
                var pagination = products.Skip((page - 1) * limit).Take(limit).ToList();

                return ApiResponse.Success(
                    pagination,
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
        public async Task<IActionResult> SearchProducts(string keyword)
        {
            // Retrieve products based on the search keyword
            var products = await _productService.SearchProducts(keyword);

            if (products == null || !products.Any())
            {
                return ApiResponse.NotFound("No products found matching the search criteria.");

            }
            return ApiResponse.Success(products);

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
}
