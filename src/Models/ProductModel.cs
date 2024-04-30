using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models;

public class ProductModel
{
    public Guid ProductId { get; set; }
    public required string Name { get; set; }
    public required double Price { get; set; }
    public string? Description { get; set; }
    public string Image { get; set; } = string.Empty;

    //Foreign Key
    public Guid CategoryId { get; set; }

    //Navigation properties
    // public CategoryModel? Category { get; set; }
    // public List<ReviewModel>? Reviews { get; set; }
    // public List<OrderItemModel>? OrderItems { get; set; }
}
