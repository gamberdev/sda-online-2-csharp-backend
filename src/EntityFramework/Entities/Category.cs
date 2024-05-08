using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.EntityFramework.Table;

[Table("category")]
public class Category
{
    [Column("category_id")]
    public Guid CategoryId { get; set; }

    [Required]
    [MinLength(2)]
    [Column("name")]
    public string? Name { get; set; }

    [Column("slug")]
    public string Slug { get; set; } = string.Empty;

    public ICollection<Product>? Product { get; set; }
}
