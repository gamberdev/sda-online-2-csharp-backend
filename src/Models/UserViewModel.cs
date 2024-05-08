using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_EF.Models;

public class UserViewModel
{
    public Guid UserId { get; set; }

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateTime CreatedAt { get; set; }
}