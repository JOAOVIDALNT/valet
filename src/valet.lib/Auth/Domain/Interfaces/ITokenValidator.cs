namespace valet.lib.Auth.Domain.Interfaces
{
    internal interface ITokenValidator
    {
        Guid ValidateAndGetUserIdentifier(string token);
    }
}