using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models;

public class ProductModel
{
    public Guid ProductId { get; set; }
    
    [Required(ErrorMessage = "Product Name is required")]
    [MinLength(2, ErrorMessage = "Product Name should be at least 2 letter")]
    public string? Name { get; set; }

    public string Slug { get; set; } = string.Empty;

    [Required(ErrorMessage = "Product Price is required")]
    [Range(0.01, 20000.00, ErrorMessage = "Price must be between 0.01 and 20000.00")]
    public double Price { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }

    //Foreign Key
    public Guid CategoryId { get; set; }

    //Navigation properties
    // public CategoryModel? Category { get; set; }
    // public List<ReviewModel>? Reviews { get; set; }
    // public List<OrderItemModel>? OrderItems { get; set; }
}
