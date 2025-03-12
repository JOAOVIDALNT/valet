using valet.auth.Domain.Entities;

namespace valet.auth.Core.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}
