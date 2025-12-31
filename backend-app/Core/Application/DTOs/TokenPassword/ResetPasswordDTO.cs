using System.ComponentModel.DataAnnotations;

namespace backend_app.Core.Application.DTOs.TokenPassword;

public class ResetPasswordDTO
{
    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 100 caracteres")]
    public required string NewPassword { get; set; }
    
    [Required]
    public required string Code { get; set; }
}