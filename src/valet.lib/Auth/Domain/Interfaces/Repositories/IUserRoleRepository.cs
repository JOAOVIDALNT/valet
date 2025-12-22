using valet.lib.Auth.Domain.Entities;
using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Auth.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Defines repository operations for the <see cref="UserRole"/> entity,
    /// extending the generic <see cref="IRepository{T}"/> interface.
    /// </summary>
    public interface IUserRoleRepository : IRepository<UserRole>
    {
    }
}
