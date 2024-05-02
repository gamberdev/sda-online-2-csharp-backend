using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.EF;

namespace ecommerce.Tables;

[Table("order_item")]
public class OrderItem
{
    [Column("orderItem_id")]
    public Guid OrderItemId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("price")]
    public double Price { get; set; }

    //Foreign Key
    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("order_id")]
    public Guid? OrderId { get; set; }

    //Navigation properties
    public Product? Product { get; set; }
    public Order? Order { get; set; }
    public User? User { get; set; }
}
