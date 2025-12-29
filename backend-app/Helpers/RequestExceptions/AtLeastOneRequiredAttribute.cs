using System.ComponentModel.DataAnnotations;

namespace backend_app.Attributes;

public class AtLeastOneRequiredAttribute : ValidationAttribute
{
    private readonly string[] _properties;

    public AtLeastOneRequiredAttribute(params string[] properties)
    {
        _properties = properties;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var properties = _properties.Select(property => validationContext.ObjectType.GetProperty(property));
        var values = properties.Select(property => property?.GetValue(validationContext.ObjectInstance, null));

        if (values.Any(val => val != null && !string.IsNullOrWhiteSpace(val.ToString())))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage ?? "Pelo menos um dos campos deve ser preenchido.");
    }
}