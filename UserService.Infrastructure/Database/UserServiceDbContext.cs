
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
namespace UserService.Infrastructure.Database;

public partial class UserServiceDbContext : DbContext
{
    public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Configurations.RoleConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.UserConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
