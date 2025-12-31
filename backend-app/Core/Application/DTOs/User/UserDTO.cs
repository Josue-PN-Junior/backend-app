namespace backend_app.Core.Application.DTOs.User;

public class UserDTO
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public string? Nickname { get; set; }
    public required string Email { get; set; }

}
