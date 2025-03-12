using valet.lib.Core.Domain.Entities;

namespace valet.lib.Auth.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public virtual User? User { get; set; }
        public virtual Role? Role { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
