using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models;

public class Order
{
    public int OrderId { get; set; }
    public double TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string OrderStatus { get; set; } = "Pending";

    //Foreign Key
    public int UserId { get; set; }

    // Navigation properties
    public User? User { get; set; }
    public Shipment? Shipment { get; set; }
    public List<OrderItem>? OrderItems { get; set; }
}
