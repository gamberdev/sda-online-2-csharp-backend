using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models;

public class CategoryModel
{
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "Category Name is required")]
    [MinLength(2, ErrorMessage = "Category Name should be at least 2 letter")]
    public string? Name { get; set; }
    public string Slug { get; set; } = string.Empty;

    // Navigation properties
    // public List<ProductModel>? Products { get; set; }
}
