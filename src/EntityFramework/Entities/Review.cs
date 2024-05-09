using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.EntityFramework.Table;

[Table("review")]
public class Review
{
    [Column("review_id")]
    public Guid ReviewId { get; set; }

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
