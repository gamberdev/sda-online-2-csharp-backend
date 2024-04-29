namespace ecommerce.Models;

public class Review
{
    public int ReviewId { get; set; }
    public required string Comment { get; set; }

    //Foreign Key
    public int ProductId { get; set; }
    public int UserId { get; set; }

    //Navigation properties
    public List<Product>? Products { get; set; }
    public List<User>? Users { get; set; }
}
