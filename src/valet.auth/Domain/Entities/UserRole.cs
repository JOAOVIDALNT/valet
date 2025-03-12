using valet.core.Domain.Entities;

namespace valet.auth.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public virtual User? User { get; set; }
        public virtual Role? Role { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
