using valet.lib.Auth.Domain.Entities;

namespace valet.lib.Auth.Domain.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}
