using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models;
public enum OrderStatus
{
    Pending,
    Processing,
    Cancelled
}
public class OrderModel
{
    public Guid OrderId { get; set; }
    public double TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

    //Foreign Key
    public Guid UserId { get; set; }

    // Navigation properties
    // public UserModel? User { get; set; }
    // public ShipmentModel? Shipment { get; set; }
    // public List<OrderItemModel>? OrderItems { get; set; }
}
