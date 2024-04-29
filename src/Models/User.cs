using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models;

public class User
{
    public int UserId { get; set; }
    public required string FullName { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Role { get; set; } = "customer";
    public bool IsBanned { get; set; } = false;

    // Navigation properties
    public List<Order>? Orders { get; set; }
    public List<OrderItem>? OrderItems { get; set; }
    public List<Shipment>? Shipments { get; set; }
    public List<Review>? Reviews { get; set; }
}
