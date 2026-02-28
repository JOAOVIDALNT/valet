using Microsoft.EntityFrameworkCore;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using valet.lib.Core.Data.Repositories;

namespace valet.lib.Auth.Data.Repositories
{
    internal class UserRepository<TContext>(TContext db) : Repository<User>(db), IUserRepository where TContext : AuthDbContext
    {
        public async Task<bool> UserExistsAsync(string username) => await dbSet
            .AnyAsync(u => u.Username.Equals(username));
        public async Task<bool> UserExistsAsync(Guid identifier) => await dbSet
            .AnyAsync(u => u.Id.Equals(identifier));
        public async Task<User> GetUserWithRolesAsync(string username) => await dbSet
            .Include(u => u.UserRoles) 
            .ThenInclude(ur => ur.Role) 
            .FirstAsync(u => u.Username.Equals(username));
    }
}
