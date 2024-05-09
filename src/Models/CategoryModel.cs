using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models;

public class CategoryModel
{
    public Guid CategoryId { get; set; }
    
    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters.")]
    
    public string? Name { get; set; }
    public string Slug { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<ProductModel>? Products { get; set; }
}
