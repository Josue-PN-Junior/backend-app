using backend_app.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Infrastructure.Data.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<UserEntity> User { get; set; }
    public DbSet<TokenPasswordEntity> ResetToken { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
            "Server=localhost;" +
            "Port=5432; Database=postgres;" +
            "User Id=postgres;" +
            "password=123"
        );
}