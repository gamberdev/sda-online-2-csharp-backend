using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.EntityFramework.Table

{
    [Table("products")]

    public class Product
    {       
        [Key]
        [Column("product_id")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [MinLength(2, ErrorMessage = "Product Name should be at least 2 letter")]
        [Column("name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Product Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        
        [Column("price")]
        public double Price { get; set; }

        [Column("slug")]
         public string Slug { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("image")]
        public string Image { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
        
        //Foreign Key
        [Column("category_id")]
        public Guid? CategoryId { get; set; }

        //Navigation properties
         public Category Category { get; set; }
         public ICollection<Review> Reviews { get; set; }
         public ICollection<OrderItem> OrderItems { get; set; }

          public Product()
        {
            // Initialize navigation properties to avoid null reference exceptions
            Category = new Category();
            Reviews = new List<Review>();
            OrderItems = new List<OrderItem>();
        }
    }
}