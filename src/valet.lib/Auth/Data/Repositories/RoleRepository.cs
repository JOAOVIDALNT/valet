using Microsoft.EntityFrameworkCore;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using valet.lib.Core.Data.Repositories;

namespace valet.lib.Auth.Data.Repositories
{
    public class RoleRepository<TContext>(TContext db) : Repository<Role>(db), IRoleRepository where TContext : AuthDbContext
    {
        public async Task<bool> RoleExistsAsync(string name) => await dbSet.AnyAsync(x => x.Name == name);
    }
}
