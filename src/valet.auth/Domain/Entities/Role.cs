using valet.core.Domain.Entities;

namespace valet.auth.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
