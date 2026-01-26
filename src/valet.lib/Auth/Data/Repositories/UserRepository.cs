using Microsoft.EntityFrameworkCore;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using valet.lib.Core.Data.Repositories;

namespace valet.lib.Auth.Data.Repositories
{
    internal class UserRepository<TContext>(TContext db) : Repository<User>(db), IUserRepository where TContext : AuthDbContext
    {
        public async Task<bool> UserExistsAsync(string login) => await dbSet
            .AnyAsync(u => u.Login.Equals(login));
        public bool UserExists(string login) => dbSet.Any(u => u.Login.Equals(login));
        public async Task<bool> UserExistsAsync(Guid identifier) => await dbSet
            .AnyAsync(u => u.Id.Equals(identifier));
        public bool UserExists(Guid identifier) => dbSet.Any(u => u.Id.Equals(identifier));
        
        public async Task<User> GetUserWithRolesAsync(string login) => await dbSet
            .Include(u => u.UserRoles) 
            .ThenInclude(ur => ur.Role) 
            .FirstAsync(u => u.Login.Equals(login));
        public User GetUserWithRoles(string login) => dbSet
            .Include(u => u.UserRoles) 
            .ThenInclude(ur => ur.Role) 
            .First(u => u.Login.Equals(login));
        
    }
}
