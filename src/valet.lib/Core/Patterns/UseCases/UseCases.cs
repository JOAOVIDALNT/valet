using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Core.Patterns.UseCases;

public abstract class Command<TRequest, TResponse> where TRequest : ISignature
{
    public abstract Task<TResponse> Execute(TRequest signature);
}

public abstract class Command<TRequest> where TRequest : ISignature
{
    public abstract Task Execute(TRequest signature);
}

public abstract class Query<TResponse>
{
    public abstract Task<TResponse> Execute();
}

public abstract class Query<TRequest, TResponse> where TRequest : ISignature
{
    public abstract Task<TResponse> Execute(TRequest request);
}

