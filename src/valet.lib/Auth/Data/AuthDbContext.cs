using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using valet.lib.Auth.Domain.Entities;

namespace valet.lib.Auth.Data
{
    public class AuthDbContext(DbContextOptions options) : DbContext(options)
    {
        protected virtual DbSet<User> Users { get; set; }
        protected virtual DbSet<Role> Roles { get; set; }
        protected virtual DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
        }
    }
}
