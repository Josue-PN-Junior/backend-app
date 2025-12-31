using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_app.Models.User;

[Table("user")]
public class UserEntity
{
    [Key]
    [Column("id")]
    public int Id { get; private set; }

    [Column("fullname")]
    public string FullName { get; private set; }

    [Column("nickname")]
    public string? Nickname { get; private set; }

    [Column("email")]
    public string Email { get; private set; }

    [Column("password")]
    public string Password { get; private set; }

    public UserEntity(string fullName, string? nickname, string email, string password)
    {
        this.FullName = fullName ?? throw new ArgumentNullException(nameof(fullName)); ;
        this.Nickname = nickname;
        this.Email = email ?? throw new ArgumentNullException(nameof(email)); ;
        this.Password = password;
    }

    public void UpdateUserNameAndNickname(string? _fullName = null, string? _nickname = null)
    {
        this.FullName = _fullName ?? this.FullName;
        this.Nickname = _nickname ?? this.Nickname;
    }

    public void UpdateUserEmail(string _email)
    {
        this.Email = _email;
    }

    public void UpdatePassword(string _password)
    {
        this.Password = _password;
    }
}
