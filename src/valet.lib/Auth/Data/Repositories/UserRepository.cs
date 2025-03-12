using Microsoft.EntityFrameworkCore;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Auth.Domain.Interfaces.Repositories;
using valet.lib.Core.Data.Repositories;

namespace valet.lib.Auth.Data.Repositories
{
    public class UserRepository<TContext>(TContext db) : Repository<User>(db), IUserRepository where TContext : AuthDbContext
    {
        public async Task<bool> UserExists(string email) => await dbSet.AnyAsync(u => u.Email.Equals(email));
        public async Task<User> GetUserWithRolesAsync(string email) => await dbSet.Include(u => u.UserRoles)
                                                                            .ThenInclude(ur => ur.Role)
                                                                            .FirstAsync(u => u.Email.Equals(email));
    }
}
