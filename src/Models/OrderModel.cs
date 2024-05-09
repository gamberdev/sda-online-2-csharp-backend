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

    [Required(ErrorMessage = "Delivery address is required.")]
    [MaxLength(100, ErrorMessage = "Delivery address must be at most 100 characters.")]
    
    public string? DeliveryAddress { get; set; }

    [Required]
    public string PaymentMethod { get; set; } = string.Empty;

    [ExistingOrderStatus(ErrorMessage = "Invalid order status.")]
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

    //Foreign Key
    public Guid UserId { get; set; }

    // Navigation properties
    public UserModel? User { get; set; }
    public ICollection<OrderItemModel>? OrderItems { get; set; }
}
