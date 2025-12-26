using System.ComponentModel.DataAnnotations;

namespace backend_app.Models.User;

public class UserCreateDTO
{
    [Required]
    public string fullName {get; set;} 

    public string? nickname {get; set;} 

    [Required]
    [EmailAddress]
    public string email {get; set;} 

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string password {get; set;} 
}
