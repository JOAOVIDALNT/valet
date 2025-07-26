using valet.lib.Auth.Domain.Entities;

namespace valet.lib.Auth.Domain.Interfaces
{
    /// <summary>
    /// Defines a contract for generating JSON Web Tokens (JWT) for authenticated users.
    /// </summary>
    /// <remarks>
    /// Implementations typically generate a JWT containing user-specific claims such as user ID,
    /// first name, last name, and roles, signed with a secret key and with a configurable expiration time.
    /// </remarks>
    public interface ITokenGenerator
    {
        /// <summary>
        /// Generates a signed JWT token representing the specified user,
        /// embedding claims including user ID, name, surname, and roles.
        /// The token is signed and configured to expire after a predefined time interval.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <returns>A string containing the generated JWT token.</returns>
        string GenerateToken(User user);
    }
}
