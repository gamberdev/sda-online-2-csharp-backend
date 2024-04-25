public class User
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreateAt { get; set; }
    public string Role { get; set; }
    public bool IsBanned { get; set; }
    
  
}

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    
    
   
}


public class Product{

public int ProductId { get; set; }
public string Name { get; set; }
public string Slug { get; set; }
public decimal Price { get; set; }
public string Description { get; set; }
public string Image { get; set; }


}
public class Orders{

public int OrderId  { get; set; }
public decimal TotalPrice { get; set; }
public DateTime OrderDate { get; set; }
public string PaymentMethod { get; set; }
public string OrderStatus { get; set; }


}
public class OrderItem{

public int ItemId { get; set; }
public int Quantity { get; set; }
public decimal Price { get; set; }
}
