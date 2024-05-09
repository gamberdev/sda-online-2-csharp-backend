using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models
{
   public class UserStatusUpdateModel
{
    public bool? IsBanned { get; set; }
    public Role? Role { get; set; }
}
}