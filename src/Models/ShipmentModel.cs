namespace ecommerce.Models;

public enum ShipmentStatus
{
    Processing,
    OutForDelivery,
    Delivered
}

public class ShipmentModel
{
    public Guid ShipmentId { get; set; }
    public DateTime DeliveryDate { get; set; }
    public required string DeliveryAddress { get; set; }
    public ShipmentStatus ShipmentStatus { get; set; } = ShipmentStatus.Processing;

    //Foreign Key
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }

    //Navigation properties
    // public OrderModel? Order { get; set; }
    // public UserModel? User { get; set; }
}
