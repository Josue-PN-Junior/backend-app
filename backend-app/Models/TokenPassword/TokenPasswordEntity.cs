using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_app.Models.TokenPassword;

[Table("passwordresettokens")]
public class TokenPasswordEntity
{
    [Key]
    [Column("id")]
    public int Id { get; private set; }

    [Column("token")]

    public string Token { get; private set; }

    [Column("expiration")]

    public DateTime Expiration { get; private set; }

    [Column("userid")]

    public int UserId { get; private set; }

    public TokenPasswordEntity(string token, DateTime expiration, int userId)
    {
        Token = token;
        Expiration = expiration;
        UserId = userId;
    }
}
