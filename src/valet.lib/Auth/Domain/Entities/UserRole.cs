using valet.lib.Core.Domain.Entities;

namespace valet.lib.Auth.Domain.Entities
{
    /// <summary>
    /// Represents the association between a <see cref="User"/> and a <see cref="Role"/>,
    /// establishing a many-to-many relationship.
    /// </summary>
    public class UserRole : BaseEntity
    {
        /// <summary>
        /// Protected parameterless constructor for EF Core.
        /// </summary>
        protected UserRole() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRole"/> class
        /// by associating a user with a role.
        /// </summary>
        /// <param name="user">The user to associate.</param>
        /// <param name="role">The role to associate.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="user"/> or <paramref name="role"/> is <c>null</c>.
        /// </exception>
        public UserRole(User user, Role role)
        {
            Role = role ?? throw new ArgumentNullException(nameof(role), "Role cannot be null.");
            User = user ?? throw new ArgumentNullException(nameof(user), "User cannot be null.");
            UserId = user.Id;
            RoleId = role.Id;
        }

        /// <summary>
        /// Gets or sets the associated user.
        /// </summary>
        public virtual User? User { get; protected set; }

        /// <summary>
        /// Gets or sets the associated role.
        /// </summary>
        public virtual Role? Role { get; protected set; }

        /// <summary>
        /// Gets or sets the foreign key identifier for the user.
        /// </summary>
        public Guid UserId { get; protected set; }

        /// <summary>
        /// Gets or sets the foreign key identifier for the role.
        /// </summary>
        public Guid RoleId { get; protected set; }
    }
}
