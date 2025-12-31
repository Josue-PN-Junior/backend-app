using System.ComponentModel.DataAnnotations;

namespace backend_app.Helpers.RequestExceptions;

public class CodeValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        string? code = value as string;

        if (code != null)
        {
            if (!string.IsNullOrWhiteSpace(code) && code.All(char.IsDigit) && code.Length == 4)
            {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult(ErrorMessage ?? "The code must have exactly 4 numeric digits.");
    }
}
