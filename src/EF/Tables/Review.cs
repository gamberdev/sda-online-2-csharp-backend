using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.EF;

namespace ecommerce.Tables;

[Table("review")]
public class Review
{
    [Column("review_id")]
    public Guid ReviewId { get; set; }

    [MinLength(5)]
    [Column("comment")]
    public string? Comment { get; set; }

    //Foreign Key
    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    //Navigation properties
    public Product? Product { get; set; }
    public User? User { get; set; }
}
