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
        /// <param name="firstName">User's first name. Cannot be null or empty.</param>
        /// <param name="lastName">User's last name. Cannot be null or empty.</param>
        /// <param name="email">User's email. Cannot be null or empty.</param>
        /// <param name="password">User's password. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Thrown when any parameter is null or empty.</exception>
        public User(string firstName, string lastName, string email, string password)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetEmail(email);
            SetPassword(password);
        }

        /// <summary>
        /// Gets the user's first name.
        /// </summary>
        public string FirstName { get; protected set; } = string.Empty;

        /// <summary>
        /// Gets the user's last name.
        /// </summary>
        public string LastName { get; protected set; } = string.Empty;

        /// <summary>
        /// Gets the user's email address.
        /// </summary>
        public string Email { get; protected set; } = string.Empty;

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
            Touch();
        }

        /// <summary>
        /// Updates the user's email address.
        /// </summary>
        /// <param name="email">The new email. Cannot be null or empty.</param>
        public void UpdateEmail(string email)
        {
            SetEmail(email);
            Touch();
        }

        /// <summary>
        /// Updates the user's first and last name.
        /// </summary>
        /// <param name="firstName">The new first name. Cannot be null or empty.</param>
        /// <param name="lastName">The new last name. Cannot be null or empty.</param>
        public void UpdateName(string firstName, string lastName)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            Touch();
        }

        /// <summary>
        /// Sets the user's first name with validation.
        /// </summary>
        /// <param name="firstName">The first name to set. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="firstName"/> is null or empty.</exception>
        protected void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));

            this.FirstName = firstName;
        }

        /// <summary>
        /// Sets the user's last name with validation.
        /// </summary>
        /// <param name="lastName">The last name to set. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="lastName"/> is null or empty.</exception>
        protected void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));

            this.LastName = lastName;
        }

        /// <summary>
        /// Sets the user's email address with validation.
        /// </summary>
        /// <param name="email">The email to set. Cannot be null or empty.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="email"/> is null or empty.</exception>
        protected void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            this.Email = email;
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
            Touch();
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
            Touch();
        }
    }
}
