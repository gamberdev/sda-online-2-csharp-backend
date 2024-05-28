using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.EntityFramework.Table;

[Table("category")]
public class Category
{
    [Column("category_id")]
    public Guid CategoryId { get; set; }

    //CategoryName
    [Column("category_name")]
    public string? Name { get; set; }

    [Column("slug")]
    public string Slug { get; set; } = string.Empty;

    // Navigation property
        public ICollection<Product>? Product { get; set; }
}
