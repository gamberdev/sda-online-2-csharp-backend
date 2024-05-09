using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models;

public class SignIn
{
    [Required(ErrorMessage = "User Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set; }

    [MaxLength(20, ErrorMessage = "Long password")]
    [Required(ErrorMessage = "User Password is required")]
    [MinLength(5, ErrorMessage = "password should be longer")]
    [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
    public string? Password { get; set; }
}
