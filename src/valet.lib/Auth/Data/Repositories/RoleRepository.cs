using Microsoft.EntityFrameworkCore;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using valet.lib.Core.Data.Repositories;

namespace valet.lib.Auth.Data.Repositories
{
    internal class RoleRepository<TContext>(TContext db) : Repository<Role>(db), IRoleRepository where TContext : AuthDbContext
    {
        public async Task<bool> RoleExistsAsync(string name) => await dbSet.AnyAsync(x => x.Name == name);

        public async Task<Role> EnsureRoleExistsAsync(string name)
        {
            var role = await dbSet.FirstOrDefaultAsync(x => x.Name == name);
            
            if (role != null)
                return role;
            
            role = new Role(name);
            await dbSet.AddAsync(role);
            return role;
        }
    }
}
