using valet.lib.Auth.Domain.Entities;
using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Auth.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> UserExists(string email);
        Task<User> GetUserWithRolesAsync(string email);
    }
}
