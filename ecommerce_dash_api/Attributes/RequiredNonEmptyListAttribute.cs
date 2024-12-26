using System.Collections;
using System.ComponentModel.DataAnnotations;

public class RequiredNonEmptyListAttribute : ValidationAttribute
{
    public RequiredNonEmptyListAttribute() : base("The list cannot be empty.") { }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is List<string> list && list.Count == 0)
        {
            return new ValidationResult("The list cannot be empty.");
        }

        return ValidationResult.Success;
    }
}
