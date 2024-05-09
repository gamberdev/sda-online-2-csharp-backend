using System.ComponentModel.DataAnnotations.Schema;
using ecommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.EntityFramework.Table;

[Table("order")]
public class Order
{
    [Key]
    [Column("order_id")]
    public Guid OrderId { get; set; }

    [Column("total_price")]
    public double? TotalPrice { get; set; }

    [Column("order_date")]
    public DateTime OrderDate { get; set; }

    [Column("delivery_date")]
    public DateTime DeliveryDate { get; set; }

    [Required(ErrorMessage = "Delivery address is required.")]
    [MaxLength(100, ErrorMessage = "Delivery address must be at most 100 characters.")]
    [Column("delivery_address")]
    public required string DeliveryAddress { get; set; }

    [Required]
    [Column("payment_method")]
    public string PaymentMethod { get; set; } = string.Empty;

    [Column("order_status")]
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

    // Foreign Key
    [ForeignKey("User")]
    [Column("user_id")]
    public Guid UserId { get; set; }

    // Navigation properties
    public User? User { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; }

      // public Order()
        //{
          //  OrderItems = new List<OrderItem>(); // Initialize navigation property to avoid null reference exceptions
       // }


}
