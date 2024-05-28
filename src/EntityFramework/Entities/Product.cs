using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.EntityFramework.Table
{
    [Table("products")]
    public class Product
    {
        [Column("product_id")]
        public Guid? ProductId { get; set; }

        [Column("category_id")]
        public Guid? CategoryId { get; set; }  // Corrected to PascalCase for consistency

        [Column("name")]
        public string? Name { get; set; }

        [Column("price")]
        public double Price { get; set; }

        [Column("quantity")]
        public double Quantity { get; set; } = 0;  // Corrected to PascalCase for consistency

        [Column("slug")]
        public string Slug { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("image")]
        public string Image { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Category? Category { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}