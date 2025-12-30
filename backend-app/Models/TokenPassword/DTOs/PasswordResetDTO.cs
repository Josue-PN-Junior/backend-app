using System.ComponentModel.DataAnnotations;

namespace backend_app.Models.TokenPassword.DTOs;

public class PasswordResetDTO
{

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 100 caracteres")]
    public required string NewPassword {get; set;} 
}
