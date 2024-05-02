using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Tables

{
    [Table("Order")]

    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        [Required(ErrorMessage = "TotalPrice is required")]
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        [Required(ErrorMessage = "Delivery Date is required")]
        public DateTime DeliveryDate { get; set; }
        public required string DeliveryAddress { get; set; }
        [Required(ErrorMessage = "Payment Method is required")]
        public string PaymentMethod { get; set; } = string.Empty;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        //Foreign Key
        public Guid UserId { get; set; }

        // Navigation properties
        // public UserModel? User { get; set; }
        // public List<OrderItemModel>? OrderItems { get; set; }
    }
}
