    using valet.lib.Core.Domain.Entities;

namespace valet.lib.Auth.Domain.Entities
{
    /// <summary>
    /// Represents an application user with personal details and associated roles.
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Protected parameterless constructor for EF Core.
        /// </summary>
        protected User() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class 
        /// with the specified username and password hash.
        /// </summary>
        /// <param name="username">
        /// The unique username used to identify the user. Cannot be null or empty.
        /// </param>
        /// <param name="password">
        /// The password associated with the user. Cannot be null or empty.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="username"/> or <paramref name="password"/> is null or empty.
        /// </exception>
        public User(string username, string password)
        {
            SetUsername(username);
            SetPassword(password);
        }
        
        /// <summary>
        /// Gets the unique username used to identify the user.
        /// </summary>
        public string Username { get; protected set; } = string.Empty;

        /// <summary>
        /// Gets the password of the user.
        /// </summary>
        public string Password { get; protected set; } = string.Empty;

        /// <summary>
        /// Navigation property for the roles associated with the user.
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; protected set; } = [];

        /// <summary>
        /// Updates the user's password.
        /// </summary>
        /// <param name="password">The new password. Cannot be null or empty.</param>
        public void UpdatePassword(string password)
        {
            SetPassword(password);
        }

        /// <summary>
        /// Updates the user's username.
        /// </summary>
        /// <param name="username">
        /// The new username. Cannot be null or empty.
        /// </param>
        public void UpdateUsername(string username)
        {
            SetUsername(username);
        }

        /// <summary>
        /// Sets the user's username with validation.
        /// </summary>
        /// <param name="username">
        /// The username to assign. Cannot be null or empty.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="username"/> is null or empty.
        /// </exception>
        protected void SetUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Login cannot be null or empty.", nameof(username));
            this.Username = username;
        }

        /// <summary>
        /// Sets the user's password with validation.
        /// </summary>
        /// <param name="password">The password to set. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="password"/> is null or empty.</exception>
        protected void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));
            this.Password = password;
        }

        /// <summary>
        /// Adds a user-role association to this user.
        /// </summary>
        /// <param name="userRole">The <see cref="UserRole"/> to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="userRole"/> is null.</exception>
        public void AddUserRole(UserRole userRole)
        {
            if (userRole == null)
                throw new ArgumentNullException(nameof(userRole), "User role cannot be null.");

            if (UserRoles.Contains(userRole))
                return;

            UserRoles.Add(userRole);
        }

        /// <summary>
        /// Removes a user-role association from this user.
        /// </summary>
        /// <param name="userRole">The <see cref="UserRole"/> to remove.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="userRole"/> is null.</exception>
        public void RemoveUserRole(UserRole userRole)
        {
            if (userRole == null)
                throw new ArgumentNullException(nameof(userRole), "User role cannot be null.");
            if (!UserRoles.Contains(userRole))
                return;
            UserRoles.Remove(userRole);
        }
    }
}
