namespace ecommerce.Models;


public class Review{
     public int ReviewId { get; set; }
    public int Comment { get; set; }
    public List<Product>? Products { get; set; }
    public List<User>? users { get; set; }
}

