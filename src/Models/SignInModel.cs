using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models;

public class SignIn
{
    [Required(ErrorMessage = "User Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "User Password is required")]
    public string? Password { get; set; }
}
