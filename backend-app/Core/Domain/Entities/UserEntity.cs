using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_app.Core.Domain.Entities;

[Table("users")]
public class UserEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("full_name")]
    [StringLength(100)]
    public required string FullName { get; set; }

    [Column("nickname")]
    [StringLength(50)]
    public string? Nickname { get; set; }

    [Required]
    [Column("email")]
    [StringLength(255)]
    public required string Email { get; set; }

    [Required]
    [Column("password_hash")]
    [StringLength(255)]
    public required string PasswordHash { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}