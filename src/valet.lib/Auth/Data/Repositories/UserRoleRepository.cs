using valet.lib.Auth.Domain.Entities;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using valet.lib.Core.Data.Repositories;

namespace valet.lib.Auth.Data.Repositories
{
    public class UserRoleRepository<TContext>(TContext db) : Repository<UserRole>(db), IUserRoleRepository where TContext : AuthDbContext
    {
    }
}
