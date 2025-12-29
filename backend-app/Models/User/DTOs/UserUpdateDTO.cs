using System.ComponentModel.DataAnnotations;
using backend_app.Attributes;

namespace backend_app.Models.User.DTOs;

[AtLeastOneRequiredAttribute(nameof(FullName), nameof(Nickname), ErrorMessage = "Corpo vazio")]
public class UserUpdateDTO
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
    public string? FullName { get; set; }
    public string? Nickname { get; set; }
}
