using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models;

public class OrderItemModel
{
    public Guid OrderItemId { get; set; }

    [Required]
    public int Quantity { get; set; }

    public double Price { get; set; }

    //Foreign Key
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public Guid? OrderId { get; set; }

    //Navigation properties
    public ProductModel? Product { get; set; }
    public OrderModel? Order { get; set; }
    public UserModel? User { get; set; }
}
