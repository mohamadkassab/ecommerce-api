using System;
using System.ComponentModel.DataAnnotations;

public class RangeValidationAttribute : ValidationAttribute
{
    private readonly string _minValueString;
    private readonly string _maxValueString;

    // Constructor to accept min and max values as strings
    public RangeValidationAttribute(string minValue, string maxValue)
    {
        _minValueString = minValue;
        _maxValueString = maxValue;
    }

    public override bool IsValid(object value)
    {
        if (value is decimal decimalValue)
        {
            // Convert the min and max values from strings to decimals
            decimal minValue = Convert.ToDecimal(_minValueString);
            decimal maxValue = Convert.ToDecimal(_maxValueString);

            // Check if the value is within the valid range
            return decimalValue >= minValue && decimalValue <= maxValue;
        }

        // If the value is not a decimal, validation fails
        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} must be between {_minValueString} and {_maxValueString}.";
    }
}
