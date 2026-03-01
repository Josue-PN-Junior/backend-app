using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_app.Core.Domain.Entities;

[Table("password_reset_tokens")]
public class TokenPasswordEntity
{
    [Key]
    [Column("id")]
    public int Id { get; private set; }

    [Column("token")]

    public string Token { get; private set; }

    [Column("expiration")]

    public DateTime Expiration { get; private set; }

    [Column("code")]
    public string Code { get; set; }

    [Column("user_id")]

    public int UserId { get; private set; }

    [NotMapped]
    public bool IsExpired => DateTime.UtcNow > Expiration;
    public TokenPasswordEntity(string token, DateTime expiration, string code, int userId)
    {
        Token = token;
        Expiration = expiration;
        Code = code;
        UserId = userId;
    }
}
