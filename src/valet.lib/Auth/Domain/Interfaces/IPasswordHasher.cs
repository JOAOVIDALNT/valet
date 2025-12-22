namespace valet.lib.Auth.Domain.Interfaces
{
    /// <summary>
    /// Defines methods for hashing passwords and verifying hashed passwords.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashes the specified plain-text password.
        /// </summary>
        /// <param name="password">The plain-text password to hash.</param>
        /// <returns>The hashed representation of the password.</returns>
        string HashPassword(string password);

        /// <summary>
        /// Verifies whether the given plain-text password matches the stored hashed password.
        /// </summary>
        /// <param name="givenPassword">The plain-text password to verify.</param>
        /// <param name="storedPassword">The stored hashed password to compare against.</param>
        /// <returns><c>true</c> if the given password matches the stored hash; otherwise, <c>false</c>.</returns>
        bool VerifyPassword(string givenPassword, string storedPassword);
    }
}
