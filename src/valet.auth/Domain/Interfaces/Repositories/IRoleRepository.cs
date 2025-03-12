using valet.auth.Domain.Entities;
using valet.core.Domain.Interfaces;

namespace valet.auth.Domain.Interfaces.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<bool> RoleExistsAsync(string name);
    }
}
