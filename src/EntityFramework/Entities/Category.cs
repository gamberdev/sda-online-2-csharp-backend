using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.EntityFramework.Table;

[Table("category")]
public class Category
{
    [Key]
    [Column("category_id")]
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters.")]
    [Column("Category_Name")]
    public string? Name { get; set; }
    //CategoryName
    
    [Column("slug")]
    public string Slug { get; set; } = string.Empty;

    // Navigation property
    public ICollection<Product>? Product { get; set; }

    // public Category()
    //     {
    //       Product = new List<Product>(); // Initialize navigation property to avoid null reference exceptions
    //   }

}
