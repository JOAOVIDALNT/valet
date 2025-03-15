namespace valet.lib.Auth.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string givenPassword, string storedPassword);
    }
}
