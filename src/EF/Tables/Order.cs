using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;

namespace ecommerce.Tables;

[Table("order")]
public class Order
{
    public Guid OrderId { get; set; }

    public double TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }

    public DateTime DeliveryDate { get; set; }
    public required string DeliveryAddress { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

    //Foreign Key
    public Guid UserId { get; set; }

    // Navigation properties
    // public UserModel? User { get; set; }
    // public List<OrderItemModel>? OrderItems { get; set; }
}
