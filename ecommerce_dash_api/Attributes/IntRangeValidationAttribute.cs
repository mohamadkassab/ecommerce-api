using System;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class IntRangeValidationAttribute : ValidationAttribute
{
    private readonly int _min;
    private readonly int _max;

    public IntRangeValidationAttribute(int min, int max) : base("The value must be between {1} and {2}.")
    {
        _min = min;
        _max = max;
    }

    public override bool IsValid(object value)
    {

        if (value is int intValue)
        {
            return intValue >= _min && intValue <= _max;
        }

        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return string.Format(ErrorMessage, name, _min, _max);
    }
}
