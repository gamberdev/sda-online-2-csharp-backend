using System;
using System.Collections.Generic;
using System.Linq;
using ecommerce.EF;
using ecommerce.Models;
using ecommerce.Tables;
using ecommerce.utils;

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
        await Task.CompletedTask;
        var products = _appDbContext.Products.ToList();
        return products;
    }

    // Get product by ID
    public async Task<Product?> GetProductById(Guid id)
    {
        await Task.CompletedTask;
        var ProductsDb = _appDbContext.Products.ToList();
        var foundProduct = ProductsDb.FirstOrDefault(product => product.ProductId == id);
        return foundProduct;
    }

    // Create a new product
    public async Task<ProductModel> CreateProduct(ProductModel newProduct)
    {
        await Task.CompletedTask;
        Product product = new Product
        {
            ProductId = Guid.NewGuid(),
            Name = newProduct.Name,
            Price = newProduct.Price,
            Slug = Function.GetSlug(newProduct.Name ?? ""),
            Description = newProduct.Description,
            Image = newProduct.Image ?? ""
        };
        _appDbContext.Products.Add(product);
        _appDbContext.SaveChanges();
        return newProduct;
    }

    // Update an existing product
    public async Task<Product?> UpdateProduct(Guid id, ProductModel updatedProduct)
    {
        await Task.CompletedTask;
        var ProductsDb = _appDbContext.Products.ToList();
        var foundProduct = ProductsDb.FirstOrDefault(product => product.ProductId == id);
        if (foundProduct != null)
        {
            foundProduct.Name = updatedProduct.Name;
            foundProduct.Price = updatedProduct.Price;
            foundProduct.Slug = Function.GetSlug(updatedProduct.Name ?? "");
            foundProduct.Description = updatedProduct.Description;
            foundProduct.Image = updatedProduct.Image ?? "";
        }
        _appDbContext.SaveChanges();
        return foundProduct;
    }

    // Delete a product
    public async Task<bool> DeleteProduct(Guid id)
    {
        await Task.CompletedTask;
        var productDb = _appDbContext.Products.ToList();
        var foundProduct = productDb.FirstOrDefault(product => product.ProductId == id);
        if (foundProduct != null)
        {
            _appDbContext.Products.Remove(foundProduct);
            _appDbContext.SaveChanges();
            return true;
        }
        return false;
    }
}
