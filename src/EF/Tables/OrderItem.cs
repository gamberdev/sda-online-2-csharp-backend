using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Tables;

[Table("OrderItem")]
public class OrderItem
{
     [Key , Required]
    public Guid OrderItemId { get; set; }
   
    public int Quantity { get; set; }
    public double Price { get; set; }

    //Foreign Key
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }

    //Navigation properties
    // public Product? Product { get; set; }
    // public Order? Order { get; set; }
    // public User? User { get; set; }
}
