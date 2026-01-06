using Microsoft.IdentityModel.Tokens;

namespace valet.lib.Auth.Domain.Interfaces
{
    /// <summary>
    /// Defines a contract for validating authentication tokens and extracting user information.
    /// </summary>
    /// <remarks>
    /// Implementations are responsible for validating the token integrity and authenticity
    /// and extracting the user identifier embedded in the token claims.
    /// </remarks>
    public interface ITokenValidator
    {
        /// <summary>
        /// Validates the specified authentication token and returns the associated user identifier.
        /// </summary>
        /// <param name="token">The authentication token to validate.</param>
        /// <returns>
        /// The unique identifier (<see cref="Guid"/>) of the user associated with the token.
        /// </returns>
        Guid ValidateAndGetUserIdentifier(string token);
    }
}