using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models;

public class OrderItem
{
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }

    //Foreign Key
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public int OrderId { get; set; }

    //Navigation properties
    public Product? Product { get; set; }
    public Order? Order { get; set; }
    public User? User { get; set; }
}
