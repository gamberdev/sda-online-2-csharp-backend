using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ecommerce.Models;

namespace ecommerce.EntityFramework.Table;

[Table("users")]
public class User
{
    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("full_name")]
    public string? FullName { get; set; }

    [Column("phone")]
    public string? Phone { get; set; }
    [Required]
    [EmailAddress]
    [Column("email")]
    public string? Email { get; set; }

    [Required]
    [Column("password")]
    public string? Password { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("role")]
    public Role Role { get; set; } = Role.Customer;

    [Column("is_banned")]
    public bool IsBanned { get; set; } = false;

     // Navigation properties
    public ICollection<Order> Orders { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public ICollection<Review> Reviews { get; set; }

  public User()
        {
            // Initialize navigation properties to avoid null reference exceptions
            Orders = new List<Order>();
            OrderItems = new List<OrderItem>();
            Reviews = new List<Review>();
        }

}
