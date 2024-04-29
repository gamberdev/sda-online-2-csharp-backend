using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models;

     public class User
{
    public int UserId { get; set; }
    public string? FullName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTime CreateAt { get; set; }
    public string? Role { get; set; }
    public bool IsBanned { get; set; }

    // Navigation properties
    public List<Order>? Orders { get; set; }
    public List<OrderItem>? OrderItems { get; set; }
    public List<Shipment>? Shipments { get; set; }
    public List<Review>? Reviews { get; set; }
}