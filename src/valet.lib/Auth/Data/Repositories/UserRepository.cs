using Microsoft.EntityFrameworkCore;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using valet.lib.Core.Data.Repositories;

namespace valet.lib.Auth.Data.Repositories
{
    internal class UserRepository<TContext>(TContext db) : Repository<User>(db), IUserRepository where TContext : AuthDbContext
    {
        public async Task<bool> UserExistsAsync(string username, CancellationToken cancellationToken = default) => await dbSet
            .AnyAsync(u => u.Username.Equals(username), cancellationToken);
        public async Task<bool> UserExistsAsync(Guid identifier, CancellationToken cancellationToken = default) => await dbSet
            .AnyAsync(u => u.Id.Equals(identifier), cancellationToken);
        public async Task<User> GetUserWithRolesAsync(string username, CancellationToken cancellationToken = default) => await dbSet
            .Include(u => u.UserRoles) 
            .ThenInclude(ur => ur.Role) 
            .FirstAsync(u => u.Username.Equals(username), cancellationToken);
    }
}
