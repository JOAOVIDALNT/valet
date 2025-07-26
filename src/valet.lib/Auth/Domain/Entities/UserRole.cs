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
        /// Gets or sets the associated user.
        /// </summary>
        public virtual User? User { get; set; }

        /// <summary>
        /// Gets or sets the associated role.
        /// </summary>
        public virtual Role? Role { get; set; }

        /// <summary>
        /// Gets or sets the foreign key identifier for the user.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the foreign key identifier for the role.
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
