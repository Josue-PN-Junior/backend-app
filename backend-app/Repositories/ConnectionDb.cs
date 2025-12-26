using backend_app.Models.User;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Repositories;

public class ConnectionDb : DbContext
{
    public DbSet<UserEntity> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
            "Server=localhost;" +
            "Port=5432; Database=postgres;" +
            "User Id=postgres;" +
            "password=123"
        );
}
