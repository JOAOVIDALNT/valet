namespace valet.lib.Core.Patterns.UseCase;

public abstract class ParameterlessUseCase<TResponse>
{
    public abstract Task<TResponse?> Execute();
}
