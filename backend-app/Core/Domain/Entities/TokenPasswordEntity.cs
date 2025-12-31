using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_app.Core.Domain.Entities;

[Table("reset_tokens")]
public class TokenPasswordEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("user_id")]
    public int UserId { get; set; }

    [Required]
    [Column("code")]
    [StringLength(100)]
    public required string Code { get; set; }

    [Required]
    [Column("expires_at")]
    public DateTime ExpiresAt { get; set; }

    [Column("used")]
    public bool Used { get; set; } = false;

    [ForeignKey("UserId")]
    public UserEntity? User { get; set; }
}