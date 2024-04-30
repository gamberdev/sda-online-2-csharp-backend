namespace ecommerce.Models;

public class ReviewModel
{
    public Guid ReviewId { get; set; }
    public required string Comment { get; set; }

    //Foreign Key
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }

    //Navigation properties
    // public List<ProductModel>? Products { get; set; }
    // public List<UserModel>? Users { get; set; }
}
