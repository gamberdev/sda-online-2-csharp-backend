using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.EF;
using ecommerce.Models;
using ecommerce.Tables;
using ecommerce.utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace api.Controllers
{
    [ApiController]
    [Route("/api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(AppDbContext appDbContext)
        {
            _productService = new ProductService(appDbContext);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProducts();

                if (products.ToList().Count < 1)
                {
                    return NotFound(
                    new ErrorResponse { Success = false, Message = "No products found" }

                    );
                }

                return Ok(new SuccessResponse<IEnumerable<Product>>
                {

                    Success = true,
                    Message = "all products are returned successfully",
                    Data = products
                });

            }
            catch (Exception ex)
            {

                Console.Write($"an error occured while retrieving all products");

                return StatusCode(500, new ErrorResponse { Message = ex.Message });
            }

        }


        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            try
            {

                // return BadRequest("Invalid product ID Format");

                var foundProduct = await _productService.GetProductById(id);
                if (foundProduct == null)
                {
                    return NotFound(new ErrorResponse { Message = "The category not found" });
                }

                return Ok(new SuccessResponse<Product>
                {

                    Success = true,
                    Message = "all products are returned successfully",
                    Data = foundProduct
                });

            }
            catch (Exception ex)
            {
                Console.Write($"An error occurred while retrieving the productId");
                return StatusCode(500, new ErrorResponse { Message = ex.Message });
            }
        }




        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel newProduct)
        {

            try
            {
                await _productService.CreateProduct(newProduct);
                return Ok(new SuccessResponse<ProductModel> { Message = "The product is Added" });
            }
            catch (Exception ex)

            {
                Console.Write($"An error occurred while creating the product");
                return StatusCode(500, new ErrorResponse { Message = ex.Message });
            }
        }



        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductModel updatedProduct)
        {

            try
            {
                var found = await _productService.UpdateProduct(id, updatedProduct);
                if (found == null)
                {
                    return NotFound(new ErrorResponse { Message = "The product not found" });
                }
                return Ok(new SuccessResponse<Product> { Message = "Product updated", Data = found });
            }
            catch (Exception ex)
            {
                Console.Write($"An error occurred while updating the product");

                return StatusCode(
                    500,
                    new ErrorResponse { Message = ex.Message }
                );
            }


        }



        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {

            try
            {
                var deleted = await _productService.DeleteProduct(id);
                if (!deleted)
                {
                    return NotFound(new ErrorResponse { Message = "The product not found" });
                }
                return Ok(new SuccessResponse<bool> { Message = "Product Deleted" });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new ErrorResponse { Message = ex.Message }
                );
            }
        }

    }
}