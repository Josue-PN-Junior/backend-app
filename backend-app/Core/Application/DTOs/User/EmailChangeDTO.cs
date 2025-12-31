using System.ComponentModel.DataAnnotations;

namespace backend_app.Core.Application.DTOs.User;

public class EmailChangeDTO
{
    [Required]
    [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
    [StringLength(255, ErrorMessage = "Email não pode exceder 255 caracteres")]
    public required string Email { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
    [StringLength(255, ErrorMessage = "Email não pode exceder 255 caracteres")]
    public required string NewEmail { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 100 caracteres")]
    public required string Password { get; set; }
}