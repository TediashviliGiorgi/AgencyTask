using Microsoft.EntityFrameworkCore;
using UserManagementSystem.DB.Entities;

namespace UserManagementSystem.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserRoleEntity> Roles { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships and constraints
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // Seed roles
            modelBuilder.Entity<UserRoleEntity>().HasData(
                new UserRoleEntity { Id = 1, Name = "Admin" },
                new UserRoleEntity { Id = 2, Name = "User" },
                new UserRoleEntity { Id = 3, Name = "Guest" }
            );
        }
    }
}
