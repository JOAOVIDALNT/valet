using valet.lib.Core.Domain.Entities;

namespace valet.lib.Auth.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public virtual ICollection<UserRole> UserRoles { get; set; } = [];

        public void SetHashedPassword(string hashedPassword)
        {
            Password = hashedPassword;
        }
    }
}
