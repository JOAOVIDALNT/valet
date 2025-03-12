using Microsoft.EntityFrameworkCore;
using valet.auth.Domain.Entities;
using valet.auth.Domain.Interfaces.Repositories;
using valet.core.Data.Repositories;
using valet.core.Domain.Interfaces;

namespace valet.auth.Data.Repositories
{
    public class RoleRepository<TContext>(TContext db) : Repository<Role>(db), IRoleRepository where TContext : AuthDbContext
    {
        public async Task<bool> RoleExistsAsync(string name) => await dbSet.AnyAsync(x => x.Name == name);
    }
}
