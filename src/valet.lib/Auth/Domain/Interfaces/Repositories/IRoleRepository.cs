using valet.lib.Auth.Domain.Entities;
using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Auth.Domain.Interfaces.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<bool> RoleExistsAsync(string name);
    }
}
