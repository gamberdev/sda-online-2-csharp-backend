using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models;

public enum Role
{
    Customer,
    Admin
}

public class UserModel
{
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "User Name is required")]
    [MinLength(2, ErrorMessage = "User Name should be at least 2 letter")]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "User Phone is required")]
    [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong Phone formate")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "User Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set; }

    [MaxLength(20, ErrorMessage = "Long password")]
    [Required(ErrorMessage = "User Password is required")]
    [MinLength(5, ErrorMessage = "password should be longer")]
    [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
    public string? Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public Role Role { get; set; } = Role.Customer;
    public bool IsBanned { get; set; } = false;

    // Navigation properties
    public ICollection<OrderModel>? Orders { get; set; }
    public ICollection<OrderItemModel>? OrderItems { get; set; }
    public ICollection<ReviewModel>? Reviews { get; set; }
}