using System.ComponentModel.DataAnnotations;
using backend_app.Helpers.RequestExceptions;

namespace backend_app.Core.Application.DTOs.TokenPassword;

public class VerifyResetCodeDTO
{
    [Required]
    [CodeValidation]
    public required string Code { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }
}
