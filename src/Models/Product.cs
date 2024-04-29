using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models;

public class Product
{
    public int ProductId { get; set; }
    public required string Name { get; set; }
    public required double Price { get; set; }
    public string? Description { get; set; }
    public string Image { get; set; } = string.Empty;

    //Foreign Key
    public int CategoryId { get; set; }

    //Navigation properties
    public Category? Category { get; set; }
    public List<Review>? Reviews { get; set; }
    public List<OrderItem>? OrderItems { get; set; }
}
