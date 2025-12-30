using System.ComponentModel.DataAnnotations;

namespace backend_app.Models.User.DTOs;

public class EmailDTO
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
}
