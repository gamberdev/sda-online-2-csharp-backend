using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models;

public class ReviewModel
{
    public Guid ReviewId { get; set; }

    [Required(ErrorMessage = "Comment text is required")]
    [MinLength(5, ErrorMessage = "Comment text should be at least 5 letter")]
    public string? Comment { get; set; }

    //Foreign Key
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }

    //Navigation properties
    // public List<ProductModel>? Products { get; set; }
    // public List<UserModel>? Users { get; set; }
}
