using System.ComponentModel.DataAnnotations;

namespace backend_app.Models.User.DTOs;

public class UserCreateDTO
{
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public required string FullName {get; set;} 

    public string? Nickname {get; set;} 

    [Required]
    [EmailAddress(ErrorMessage = "Email must have a valid format.")]
    [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
    public required string Email {get; set;} 

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
    public required string Password {get; set;} 
}
