using System.ComponentModel.DataAnnotations;

namespace backend_app.Core.Application.DTOs.User;

public class EmailDTO
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
}
