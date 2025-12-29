using System.ComponentModel.DataAnnotations;

namespace backend_app.Models.User.DTOs;

public class UserIdDTO
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "O Id deve ser maior que 0")]
    public int Id {get; set;}
}
