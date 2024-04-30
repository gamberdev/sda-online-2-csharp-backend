using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.EF;
using ecommerce.Models;
using ecommerce.Tables;
using ecommerce.utils;

public class CategoryService
{
    private readonly AppDbContext _appDbContext;

    public CategoryService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        await Task.CompletedTask;
        var categories = _appDbContext.Categories.ToList();
        return categories;
    }

    public async Task<Category?> GetCategoryById(Guid id)
    {
        await Task.CompletedTask;
        var categoriesDb = _appDbContext.Categories.ToList();
        var foundCategory = categoriesDb.FirstOrDefault(category => category.CategoryId == id);
        return foundCategory;
    }

    public async Task<CategoryModel> AddCategory(CategoryModel newCategory)
    {
        await Task.CompletedTask;
        Category category = new Category
        {
            CategoryId = Guid.NewGuid(),
            Name = newCategory.Name,
            Slug = Function.GetSlug(newCategory.Name ?? "")
        };
        _appDbContext.Categories.Add(category);
        _appDbContext.SaveChanges();
        return newCategory;
    }

    public async Task<Category?> UpdateCategoryName(Guid id, CategoryModel updateCategory)
    {
        await Task.CompletedTask;
        var categoriesDb = _appDbContext.Categories.ToList();
        var foundCategory = categoriesDb.FirstOrDefault(category => category.CategoryId == id);
        if (foundCategory != null)
        {
            foundCategory.Name = updateCategory.Name;
            foundCategory.Slug = Function.GetSlug(updateCategory.Name ?? "");
        }
        _appDbContext.SaveChanges();
        return foundCategory;
    }

    public async Task<bool> DeleteCategory(Guid id)
    {
        await Task.CompletedTask;
        var categoryDb = _appDbContext.Categories.ToList();
        var foundCategory = categoryDb.FirstOrDefault(category => category.CategoryId == id);
        if (foundCategory != null)
        {
            _appDbContext.Categories.Remove(foundCategory);
            _appDbContext.SaveChanges();
            return true;
        }
        return false;
    }
}
