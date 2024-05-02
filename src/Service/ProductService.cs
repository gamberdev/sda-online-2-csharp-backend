using System;
using System.Collections.Generic;
using System.Linq;
using ecommerce.EF;
using ecommerce.Models;
using ecommerce.Tables;
using ecommerce.utils;
using Microsoft.EntityFrameworkCore;

// Products data
namespace ecommerce.EF;

public class ProductService
{
    private readonly AppDbContext _appDbContext;

    public ProductService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    // Get all products
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        var products = await _appDbContext
            .Products.Include(c => c.Category)
            .Include(r => r.Reviews)
            .Include(i => i.OrderItems)
            .ToListAsync();
        return products;
    }

    // Get product by ID
    public async Task<Product?> GetProductById(Guid id)
    {
        var ProductsDb = _appDbContext.Products.ToList();
        var foundProduct = await _appDbContext
            .Products.Include(c => c.Category)
            .Include(r => r.Reviews)
            .Include(i => i.OrderItems)
            .FirstOrDefaultAsync(product => product.ProductId == id);
        return foundProduct;
    }

    // Create a new product
    public async Task<ProductModel> CreateProduct(ProductModel newProduct)
    {
        Product product = new Product
        {
            ProductId = Guid.NewGuid(),
            Name = newProduct.Name,
            Price = newProduct.Price,
            Slug = Function.GetSlug(newProduct.Name ?? ""),
            Description = newProduct.Description,
            Image = newProduct.Image ?? "",
            CategoryId = newProduct.CategoryId
        };
        await _appDbContext.Products.AddAsync(product);
        await _appDbContext.SaveChangesAsync();
        return newProduct;
    }

    // Update an existing product
    public async Task<Product?> UpdateProduct(Guid id, ProductModel updatedProduct)
    {
        var foundProduct = await _appDbContext.Products.FirstOrDefaultAsync(product =>
            product.ProductId == id
        );
        if (foundProduct != null)
        {
            foundProduct.Name = updatedProduct.Name;
            foundProduct.Price = updatedProduct.Price;
            foundProduct.Slug = Function.GetSlug(updatedProduct.Name ?? "");
            foundProduct.Description = updatedProduct.Description;
            foundProduct.Image = updatedProduct.Image ?? "";
        }
        await _appDbContext.SaveChangesAsync();
        return foundProduct;
    }

    // Delete a product
    public async Task<bool> DeleteProduct(Guid id)
    {
        await Task.CompletedTask;
        var foundProduct = await _appDbContext.Products.FirstOrDefaultAsync(product =>
            product.ProductId == id
        );
        if (foundProduct != null)
        {
            _appDbContext.Products.Remove(foundProduct);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
