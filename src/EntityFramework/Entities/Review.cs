using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ecommerce.EntityFramework.Table;

[Table("review")]
public class Review
{
    [Key]
    [Column("review_id")]
    public Guid ReviewId { get; set; }

    [MinLength(5, ErrorMessage = "Comment must be at least 5 characters long.")]
    [Column("comment")]
    public string? Comment { get; set; }

    //Foreign Key
    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    //Navigation properties
    public Product Product { get; set; }
    public User User { get; set; }

      public Review()
        {
            // Initialize navigation properties to avoid null reference exceptions
            Product = new Product();
            User = new User();
        }
}
