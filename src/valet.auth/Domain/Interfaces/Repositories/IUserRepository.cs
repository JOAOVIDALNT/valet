using valet.auth.Domain.Entities;
using valet.core.Domain.Interfaces;

namespace valet.auth.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> UserExists(string email);
        Task<User> GetUserWithRolesAsync(string email);
    }
}
