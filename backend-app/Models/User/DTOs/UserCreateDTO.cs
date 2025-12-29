using System.ComponentModel.DataAnnotations;

namespace backend_app.Models.User.DTOs;

public class UserCreateDTO
{
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
    public required string FullName {get; set;} 

    public string? Nickname {get; set;} 

    [Required]
    [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
    [StringLength(255, ErrorMessage = "Email não pode exceder 255 caracteres")]
    public required string Email {get; set;} 

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 100 caracteres")]
    // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]", 
    // ErrorMessage = "Senha deve conter ao menos: 1 letra minúscula, 1 maiúscula, 1 número e 1 caractere especial")]
    public required string Password {get; set;} 
}
