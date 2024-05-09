namespace ecommerce.Models;

   public class UserStatusUpdateModel
{
    public bool? IsBanned { get; set; }

    [ExistingRole(ErrorMessage = "Invalid role.")]
    public Role? Role { get; set; }
}
