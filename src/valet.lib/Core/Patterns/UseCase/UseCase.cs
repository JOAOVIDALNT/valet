using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Core.Patterns.UseCase;

public abstract class UseCase<TRequest, TResponse> where TRequest : ISignature
{
    public abstract Task<TResponse?> Execute(TRequest signature);
}