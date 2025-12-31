using System.ComponentModel.DataAnnotations;
using backend_app.Helpers.RequestExceptions;

namespace backend_app.Models.TokenPassword.DTOs;

public class ResetPasswordDTO
{
    [Required]
    [CodeValidation]
    public required string Code { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    
    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
    public required string NewPassword {get; set;} 
}
