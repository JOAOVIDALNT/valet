using valet.lib.Core.Domain.Entities;

namespace valet.lib.Auth.Domain.Entities
{
    public class User : BaseEntity
    {
        protected User() { }
        public User(string firstName, string lastName, string email, string password)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetEmail(email);
            SetPassword(password);
        }

        public string FirstName { get; protected set; } = string.Empty;
        public string LastName { get; protected set; } = string.Empty;
        public string Email { get; protected set; } = string.Empty;
        public string Password { get; protected set; } = string.Empty;
        public virtual ICollection<UserRole> UserRoles { get; protected set; } = [];

        public void UpdatePassword(string password)
        {
            SetPassword(password);
            Touch();
        }

        public void UpdateEmail(string email)
        {
            SetEmail(email);
            Touch();
        }

        public void UpdateName(string firstName, string lastName)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            Touch();
        }

        protected void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));

            this.FirstName = firstName;
        }

        protected void SetLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));

            this.LastName = lastName;
        }

        protected void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            this.Email = email;
        }

        protected void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));
            this.Password = password;
        }

        public void AddUserRole(UserRole userRole)
        {
            if (userRole == null)
                throw new ArgumentNullException(nameof(userRole), "User role cannot be null.");

            if (UserRoles.Contains(userRole))
                return;

            UserRoles.Add(userRole);
            Touch();
        }

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
