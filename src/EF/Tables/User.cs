using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;

namespace ecommerce.EF;

[Table("users")]
public class User
{
    [Key, Required]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("full_name")]
    public string? FullName { get; set; }

    [Required]
    [Column("phone")]
    public string? Phone { get; set; }

    [Required]
    [EmailAddress]
    [Column("email")]
    public string? Email { get; set; }

    [MaxLength(20)]
    [Required]
    [MinLength(5)]
    [Column("password")]
    public string? Password { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Role Role { get; set; } = Role.Customer;

    [Column("is_banned")]
    public bool IsBanned { get; set; } = false;
}
