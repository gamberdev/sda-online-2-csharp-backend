using System.ComponentModel.DataAnnotations.Schema;
using ecommerce.Models;

namespace ecommerce.EntityFramework.Table;

[Table("order")]
public class Order
{
    [Column("order_id")]
    public Guid? OrderId { get; set; }

    [Column("total_price")]
    public double? TotalPrice { get; set; }

    [Column("order_date")]
    public DateTime OrderDate { get; set; }

    [Column("delivery_date")]
    public DateTime DeliveryDate { get; set; }

    [Column("delivery_address")]
    public required string DeliveryAddress { get; set; }

    [Column("payment_method")]
    public string PaymentMethod { get; set; } = string.Empty;

    [Column("order_status")]
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

    // Foreign Key
    [Column("user_id")]
    public Guid? UserId { get; set; }

    // Navigation properties
    public User? User { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; }
}
