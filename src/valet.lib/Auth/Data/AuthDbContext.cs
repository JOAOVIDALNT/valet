using Microsoft.EntityFrameworkCore;
using valet.lib.Auth.Domain.Entities;

namespace valet.lib.Auth.Data
{
    /// <summary>
    /// Represents the pre-configured Entity Framework database context used by the Valet authentication module.
    /// </summary>
    /// <remarks>
    /// This context defines the DbSets for <see cref="User"/>, <see cref="Role"/> and <see cref="UserRole"/> entities,
    /// and automatically applies entity configurations from its assembly during model creation.
    /// </remarks>
    /// <param name="options">The options for this context.</param>
    public class AuthDbContext(DbContextOptions options) : DbContext(options)
    {
        protected virtual DbSet<User> Users { get; set; }
        protected virtual DbSet<Role> Roles { get; set; }
        protected virtual DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Configures the model by applying all entity configurations from the current assembly.
        /// To apply your own configurations, use the <see cref="IEntityTypeConfiguration{TEntity}"/> interface
        /// and register them in this method. 
        /// Be sure to call <c>base.OnModelCreating(modelBuilder)</c> to include Valet's default configurations.
        ///
        /// <para>
        /// Example:
        /// <code>
        /// protected override void OnModelCreating(ModelBuilder modelBuilder)
        /// {
        ///     base.OnModelCreating(modelBuilder); // Apply Valet configurations
        ///     
        ///     modelBuilder.ApplyConfiguration(new MyCustomUserConfiguration()); 
        ///     -> or modelBuilder.ApplyConfiguration(typeof(MyCustomDbContext).Assembly); automatically apply all configurations from your own assembly.
        /// }
        /// </code>
        /// </para>
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
        }
    }
}
