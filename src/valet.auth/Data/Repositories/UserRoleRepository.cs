using valet.auth.Domain.Entities;
using valet.auth.Domain.Interfaces.Repositories;
using valet.core.Data.Repositories;

namespace valet.auth.Data.Repositories
{
    public class UserRoleRepository<TContext>(TContext db) : Repository<UserRole>(db), IUserRoleRepository where TContext : AuthDbContext
    {
    }
}
