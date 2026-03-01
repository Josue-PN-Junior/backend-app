using System.ComponentModel.DataAnnotations;

namespace backend_app.Core.Application.DTOs.TokenPassword;

public class PasswordResetDTO
{

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
    public required string NewPassword {get; set;} 
}
