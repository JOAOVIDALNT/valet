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
        /// Initializes a new instance of the <see cref="User"/> class with specified personal data.
        /// </summary>
        /// <param name="login">User's login. Cannot be null or empty.</param>
        /// <param name="password">User's password. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Thrown when any parameter is null or empty.</exception>
        public User(string login, string password)
        {
            SetLogin(login);
            SetPassword(password);
        }
        
        /// <summary>
        /// Gets the user's login.
        /// </summary>
        public string Login { get; protected set; } = string.Empty;

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
        /// Updates the user's login.
        /// </summary>
        /// <param name="login">The new login. Cannot be null or empty.</param>
        public void UpdateLogin(string login)
        {
            SetLogin(login);
        }

        /// <summary>
        /// Sets the user's login with validation.
        /// </summary>
        /// <param name="login">The login to set. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="login"/> is null or empty.</exception>
        protected void SetLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Login cannot be null or empty.", nameof(login));
            this.Login = login;
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
