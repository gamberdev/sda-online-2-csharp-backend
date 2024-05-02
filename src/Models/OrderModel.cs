using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models;

public enum OrderStatus
{
    Pending,
    Processing,
    OutForDelivery,
    Delivered,
    Cancelled
}

public class OrderModel
{
    public Guid OrderId { get; set; }

    [Required(ErrorMessage = "TotalPrice is required")]
    [Range(0.01, 20000000.00, ErrorMessage = "Price must be between 0.01 and 20000000.00")]
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
