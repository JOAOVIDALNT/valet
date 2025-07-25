using valet.lib.Core.Domain.Entities;

namespace valet.lib.Auth.Domain.Entities
{
    public class Role : BaseEntity
    {
        protected Role() { }
        public Role(string name)
        {
            SetName(name);
        }
        public string Name { get; protected set; } = string.Empty;
        public virtual ICollection<UserRole> UserRoles { get; protected set; } = [];

        protected void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Role name cannot be null or empty.", nameof(name));

            name = name.Trim();

            if (name.Length < 3 || name.Length > 30)
                throw new ArgumentException("Role name must be between 3 and 30 characters long.", nameof(name));

            this.Name = name;
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

        public void UpdateName(string name)
        {
            SetName(name);
            Touch();
        }
    }
}
