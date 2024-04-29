using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models;
public class Category
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }

    // Navigation properties
    public List<Product>? Products { get; set; }
}
