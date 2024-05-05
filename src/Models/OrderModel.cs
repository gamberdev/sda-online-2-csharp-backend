using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models;

public enum OrderStatus
{
    Pending,
    Processing,
    OutForDelivery,
    Delivered,
    Canceled
}

public class OrderModel
{
    public Guid OrderId { get; set; }
    public double TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    [Required]
    public string? DeliveryAddress { get; set; }

    [Required]
    public string PaymentMethod { get; set; } = string.Empty;
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

    //Foreign Key
    public Guid UserId { get; set; }

    // Navigation properties
    public UserModel? User { get; set; }
    public ICollection<OrderItemModel>? OrderItems { get; set; }
}
