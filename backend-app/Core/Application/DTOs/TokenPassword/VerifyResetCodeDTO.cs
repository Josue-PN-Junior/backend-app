using System.ComponentModel.DataAnnotations;

namespace backend_app.Core.Application.DTOs.TokenPassword;

public class VerifyResetCodeDTO
{
    [Required]
    public required string Code { get; set; }
}