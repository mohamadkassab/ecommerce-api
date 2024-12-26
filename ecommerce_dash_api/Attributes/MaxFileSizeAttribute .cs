using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Linq;

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly long _maxSize;

    public MaxFileSizeAttribute(long maxSize)
    {
        _maxSize = maxSize;
        ErrorMessage = $"File size cannot exceed {_maxSize / (1024 * 1024)} MB.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            // Single file validation
            if (file.Length > _maxSize)
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        else if (value is IEnumerable enumerable)
        {
            // List or collection validation
            foreach (var item in enumerable)
            {
                if (item is IFormFile fileInList && fileInList.Length > _maxSize)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
        }

        return ValidationResult.Success;
    }
}
