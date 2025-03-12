using valet.auth.Core.Interfaces;

namespace valet.auth.Service.Hash
{
    public class PasswordHasher : IPasswordHasher
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
