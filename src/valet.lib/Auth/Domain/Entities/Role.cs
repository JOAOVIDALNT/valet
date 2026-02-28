using valet.lib.Auth.Data;
using valet.lib.Core.Domain.Entities;

namespace valet.lib.Auth.Domain.Entities
{
    /// <summary>
    /// Represents a role within the application, typically used for authorization purposes.
    /// </summary>
    public class Role : BaseEntity
    {
        /// <summary>
        /// Protected parameterless constructor for EF Core.
        /// </summary>
        protected Role() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class with the specified name.
        /// </summary>
        /// <param name="name">The name of the role. Must be between 3 and 30 characters.</param>
        /// <exception cref="ArgumentException">Thrown when the name is null, empty, or outside the valid length range.</exception>
        public Role(string name)
        {
            SetName(name);
        }

        /// <summary>
        /// Gets the name of the role.
        /// </summary>
        public string Name { get; protected set; } = string.Empty;

        /// <summary>
        /// Navigation property for the users associated with this role.
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; protected set; } = [];

        /// <summary>
        /// Sets the name of the role, enforcing validation rules.
        /// </summary>
        /// <param name="name">The new role name. Must be between 3 and 30 characters.</param>
        /// <exception cref="ArgumentException">Thrown when the name is invalid.</exception>
        protected void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Role name cannot be null or empty.", nameof(name));

            name = name.Trim();

            if (name.Length < 3 || name.Length > 30)
                throw new ArgumentException("Role name must be between 3 and 30 characters long.", nameof(name));

            this.Name = name;
        }

        /// <summary>
        /// Adds a user-role association to this role.
        /// </summary>
        /// <param name="userRole">The <see cref="UserRole"/> to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when the userRole is null.</exception>
        public void AddUserRole(UserRole userRole)
        {
            if (userRole == null)
                throw new ArgumentNullException(nameof(userRole), "User role cannot be null.");

            if (UserRoles.Contains(userRole))
                return;

            UserRoles.Add(userRole);
        }

        /// <summary>
        /// Removes a user-role association from this role.
        /// </summary>
        /// <param name="userRole">The <see cref="UserRole"/> to remove.</param>
        /// <exception cref="ArgumentNullException">Thrown when the userRole is null.</exception>
        public void RemoveUserRole(UserRole userRole)
        {
            if (userRole == null)
                throw new ArgumentNullException(nameof(userRole), "User role cannot be null.");
            if (!UserRoles.Contains(userRole))
                return;
            UserRoles.Remove(userRole);
        }

        /// <summary>
        /// Updates the name of the role.
        /// </summary>
        /// <param name="name">The new name of the role. Must be between 3 and 30 characters.</param>
        public void UpdateName(string name)
        {
            SetName(name);
        }
    }
}
