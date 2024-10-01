using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.Attributes
{
    public class AtLeastOneRequiredAttribute<T> : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is List<T> list && list.Count > 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("At least one item must be provided.");
        }
    }
}
