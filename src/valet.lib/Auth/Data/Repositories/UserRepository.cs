using Microsoft.EntityFrameworkCore;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using valet.lib.Core.Data.Repositories;

namespace valet.lib.Auth.Data.Repositories
{
    internal class UserRepository<TContext>(TContext db) : Repository<User>(db), IUserRepository where TContext : AuthDbContext
    {
        public async Task<bool> UserExistsAsync(string email) => await dbSet
            .AnyAsync(u => u.Email.Equals(email));
        public bool UserExists(string email) => dbSet.Any(u => u.Email.Equals(email));
        public async Task<bool> UserExistsAsync(Guid identifier) => await dbSet
            .AnyAsync(u => u.Id.Equals(identifier));
        public bool UserExists(Guid identifier) => dbSet.Any(u => u.Id.Equals(identifier));
        
        public async Task<User> GetUserWithRolesAsync(string email) => await dbSet
            .Include(u => u.UserRoles) 
            .ThenInclude(ur => ur.Role) 
            .FirstAsync(u => u.Email.Equals(email));
        public User GetUserWithRoles(string email) => dbSet
            .Include(u => u.UserRoles) 
            .ThenInclude(ur => ur.Role) 
            .First(u => u.Email.Equals(email));
        
    }
}
