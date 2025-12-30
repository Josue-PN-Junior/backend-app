namespace backend_app.Models.TokenPassword.DTOs;

public class TokenPasswordDTO
{
    public required string Token { get; set; }
    public required string Code { get; set; }
    public required DateTime Expiration { get; set; }
    public required int UserId { get; set; }
}
