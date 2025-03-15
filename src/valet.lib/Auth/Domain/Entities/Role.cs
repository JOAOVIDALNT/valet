using valet.lib.Core.Domain.Entities;

namespace valet.lib.Auth.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
