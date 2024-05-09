using System.ComponentModel.DataAnnotations;
using ecommerce.Models;

public class ExistingRoleAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null && value is Role)
        {
            Role role = (Role)value;
            // Check if the role is valid (exists in the enum)
            if (!Enum.IsDefined(typeof(Role), role))
            {
                return new ValidationResult("Invalid role.");
            }
        }
        return ValidationResult.Success!;
    }
}
