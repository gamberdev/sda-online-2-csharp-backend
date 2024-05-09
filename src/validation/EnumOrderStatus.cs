using System.ComponentModel.DataAnnotations;
using ecommerce.Models;

public class ExistingOrderStatusAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null && value is OrderStatus)
        {
            OrderStatus orderStatus = (OrderStatus)value;
            // Check if the orderStatus is valid (exists in the enum)
            if (!Enum.IsDefined(typeof(OrderStatus), orderStatus))
            {
                return new ValidationResult("Invalid orderStatus.");
            }
        }
        return ValidationResult.Success!;
    }
}
