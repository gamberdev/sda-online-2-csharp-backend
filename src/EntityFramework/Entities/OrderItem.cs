using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.EntityFramework.Table;

[Table("order_item")]
public class OrderItem
{
    [Key]
    [Column("orderItem_id")]
    public Guid OrderItemId { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    [Column("quantity")]
    public int Quantity { get; set; }


    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    [Column("price")]
    public double Price { get; set; }

    //Foreign Key
    [ForeignKey("Product")]
    [Column("product_id")]
    public Guid ProductId { get; set; }
    
    [ForeignKey("User")]
    [Column("user_id")]      
    public Guid UserId { get; set; }

    [ForeignKey("Order")]
    [Column("order_id")]
    public Guid? OrderId { get; set; }

    //Navigation properties
    public Product? Product { get; set; }
    public Order? Order { get; set; }
    public User? User { get; set; }

   // public OrderItem()
       // {
            // Initialize navigation properties to avoid null reference exceptions
            //Product = new Product();
          //  Order = new Order();
        //    User = new User();
      //  }
    
}
