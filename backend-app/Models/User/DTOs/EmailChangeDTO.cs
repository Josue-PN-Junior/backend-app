using System.ComponentModel.DataAnnotations;

namespace backend_app.Models.Generic.DTOs;

public class EmailChangeDTO
{
    [Required]
    [EmailAddress(ErrorMessage = "Email must have a valid format.")]
    [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
    public required string Email { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Email must have a valid format.")]
    [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
    public required string NewEmail { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
    public required string Password { get; set; }
}
