using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.Attributes
{
    public class AtLeastOneRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is List<int> list && list.Count > 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("At least one permission ID must be provided.");
        }
    }
}
