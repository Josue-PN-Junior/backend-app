using System.ComponentModel.DataAnnotations;

namespace backend_app.Core.Application.DTOs.TokenPassword;

public class EmailDTO
{
    [Required]
    [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
    [StringLength(255, ErrorMessage = "Email não pode exceder 255 caracteres")]
    public required string Email { get; set; }
}