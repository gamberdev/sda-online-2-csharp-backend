namespace ecommerce.Models;
 public class Shipment{
    public int ShipmentId { get; set;}
    public int DeliveryDate { get; set;}
    public int DeliveryAddress { get; set;}
    public int ShipmentStatus { get; set;}

    public int UserId { get; set;}
    public int OrderId { get; set;}

    public Order? order { get; set;}
    public User? user { get; set;}
    
 }