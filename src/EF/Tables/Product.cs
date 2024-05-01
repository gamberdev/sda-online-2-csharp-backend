using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Tables

{
    [Table("products")]

    public class Product
    {

        [Key]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
         public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Image { get; set; } = string.Empty;

        //Foreign Key
        public Guid CategoryId { get; set; }

        //Navigation properties
        // public Category? Category { get; set; }
        // public List<Review>? Reviews { get; set; }
        // public List<OrderItem>? OrderItems { get; set; }


    }
}