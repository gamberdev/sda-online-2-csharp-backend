using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Tables;

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
