namespace backend_app.Core.Application.DTOs.TokenPassword;
public class TokenPasswordDTO
{
    public required string Token { get; set; }
    public required string Code { get; set; }
    public required DateTime Expiration { get; set; }
    public required int UserId { get; set; }
}
