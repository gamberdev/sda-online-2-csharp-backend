namespace ecommerce.Models;

public class Shipment
{
    public int ShipmentId { get; set; }
    public DateTime DeliveryDate { get; set; }
    public required string DeliveryAddress { get; set; }
    public string ShipmentStatus { get; set; } = "Processing";

    //Foreign Key
    public int UserId { get; set; }
    public int OrderId { get; set; }

    //Navigation properties
    public Order? order { get; set; }
    public User? user { get; set; }
}
