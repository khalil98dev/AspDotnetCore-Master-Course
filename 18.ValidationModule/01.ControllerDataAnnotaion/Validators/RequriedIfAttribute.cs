using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace ControllerDataAnnotation.Validators;

public class RequiredIfAttribute : ValidationAttribute
{
    private readonly string? _dependenatProperty;

    private readonly object? _targetValue;

    public RequiredIfAttribute(string? DependantProperty, object? TargetValue)
    {
        this._dependenatProperty = DependantProperty;
        this._targetValue = TargetValue;
    }


    protected override ValidationResult? IsValid(object? value,
     ValidationContext validationContext)
    {
        var containerType = validationContext.ObjectInstance.GetType();

        var field = containerType.GetProperty(_dependenatProperty);

        if (field is null)
            return new ValidationResult($"Unknow filed with name {_dependenatProperty}");

        var dependenatValue = field.GetValue(validationContext.ObjectInstance,null);


        if (Equals(dependenatValue, _targetValue))
        {
            if (value == null || (value is string str && string.IsNullOrEmpty(str)))
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} is required"); 
        }

        return ValidationResult.Success;
    }

}