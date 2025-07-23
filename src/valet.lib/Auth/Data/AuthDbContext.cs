using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using valet.lib.Auth.Domain.Entities;

namespace valet.lib.Auth.Data
{
    /// <summary>
    /// Represents the authentication-specific Entity Framework Core database context.
    /// This context defines the core entities used for user and role management,
    /// including support for user-role relationships.
    /// </summary>
    /// <remarks>
    /// Consumers can extend <see cref="User"/>, <see cref="Role"/>, and <see cref="UserRole"/> 
    /// by deriving their own entities and overriding the respective <see cref="DbSet{TEntity}"/> properties.
    /// </remarks>
    public class AuthDbContext(DbContextOptions options) : DbContext(options)
    {
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for application users.
        /// Override this property in a derived context to use a custom user entity.
        /// </summary>
        protected virtual DbSet<User> Users { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for application roles.
        /// Override this property in a derived context to use a custom role entity.
        /// </summary>
        protected virtual DbSet<Role> Roles { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> that maps users to roles.
        /// Override this property in a derived context to use a custom user-role linking entity.
        /// </summary>
        protected virtual DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Applies entity configurations from the current assembly.
        /// This enables automatic application of <see cref="IEntityTypeConfiguration{TEntity}"/> implementations.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure entity mappings.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
        }
    }
}
