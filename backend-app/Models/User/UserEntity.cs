using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_app.Models.User;

[Table("user")]
public class UserEntity
{
    [Key]

    public int id {get; private set;} 
    
    [Column("fullname")]
    public string fullName {get; private set;} 
    public string? nickname {get; private set;} 
    public string email {get; private set;} 

    public string password {get; private set;} 

    public UserEntity(string fullName, string? nickname, string email, string password)
    {
        this.fullName = fullName ?? throw new ArgumentNullException(nameof(fullName));;
        this.nickname = nickname;
        this.email = email ?? throw new ArgumentNullException(nameof(email));;
        this.password = password;
    }
}
