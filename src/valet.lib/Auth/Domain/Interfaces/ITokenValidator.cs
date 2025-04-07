namespace valet.lib.Auth.Domain.Interfaces
{
    public interface ITokenValidator
    {
        Guid ValidateAndGetUserIdentifier(string token);
    }
}