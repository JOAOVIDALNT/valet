using valet.lib.Auth.Domain.Interfaces;

namespace valet.lib.Auth.Service.Hash
{
    internal class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPass = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPass;
        }

        public bool VerifyPassword(string givenPassword, string storedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(givenPassword, storedPassword);
        }
    }
}
