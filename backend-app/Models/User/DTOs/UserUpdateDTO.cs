using System.ComponentModel.DataAnnotations;
using backend_app.Attributes;

namespace backend_app.Models.User.DTOs;

[AtLeastOneRequiredAttribute(nameof(FullName), nameof(Nickname))]
public class UserUpdateDTO
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string? FullName { get; set; }
    public string? Nickname { get; set; }
}
