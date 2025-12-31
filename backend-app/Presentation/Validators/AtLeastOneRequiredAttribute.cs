using System.ComponentModel.DataAnnotations;

namespace backend_app.Presentation.Validators;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class AtLeastOneRequiredAttribute : ValidationAttribute
{
    private readonly string[] _propertyNames;

    public AtLeastOneRequiredAttribute(params string[] propertyNames)
    {
        _propertyNames = propertyNames;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return new ValidationResult(ErrorMessage);

        foreach (var propertyName in _propertyNames)
        {
            var property = validationContext.ObjectType.GetProperty(propertyName);
            if (property == null)
                continue;

            var propertyValue = property.GetValue(validationContext.ObjectInstance);
            if (propertyValue != null && !string.IsNullOrWhiteSpace(propertyValue.ToString()))
                return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }
}