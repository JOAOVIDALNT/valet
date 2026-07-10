using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Core.Patterns.UseCases;

/// <summary>
/// Defines a command that executes an operation using a request signature
/// and returns a response.
/// </summary>
public interface ICommand<TRequest, TResponse> where TRequest : ISignature
{
    Task<TResponse> Execute(TRequest signature);
}

/// <summary>
/// Defines a command that executes an operation using a request signature
/// without returning a response.
/// </summary>
public interface ICommand<TRequest> where TRequest : ISignature
{
    Task Execute(TRequest signature);
}

/// <summary>
/// Defines a query that retrieves data without requiring a request signature.
/// </summary>
public interface IQuery<TResponse>
{
    Task<TResponse> Execute();
}

/// <summary>
/// Defines a query that retrieves data using a request signature and returns a response.
/// </summary>
public interface IQuery<TRequest, TResponse> where TRequest : ISignature
{
    Task<TResponse> Execute(TRequest request);
}

/// <summary>
/// Represents a command that executes an operation using a request signature
/// and returns a response.
/// </summary>
/// <typeparam name="TRequest">
/// The request type, which must implement <see cref="ISignature"/> and encapsulate
/// its own validation rules.
/// </typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
/// <remarks>
/// Commands represent operations that may change application state.
/// The request signature is expected to be validated before execution.
/// </remarks>
public abstract class Command<TRequest, TResponse> : ICommand<TRequest, TResponse> where TRequest : ISignature
{
    /// <summary>
    /// Executes the command using the specified request signature.
    /// </summary>
    /// <param name="signature">The request signature.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the command response.
    /// </returns>
    public abstract Task<TResponse> Execute(TRequest signature);
}

/// <summary>
/// Represents a command that executes an operation using a request signature
/// without returning a response.
/// </summary>
/// <typeparam name="TRequest">
/// The request type, which must implement <see cref="ISignature"/> and encapsulate
/// its own validation rules.
/// </typeparam>
/// <remarks>
/// Commands represent operations that may change application state
/// and do not produce a result.
/// </remarks>
public abstract class Command<TRequest> : ICommand<TRequest> where TRequest : ISignature
{
    /// <summary>
    /// Executes the command using the specified request signature.
    /// </summary>
    /// <param name="signature">The request signature.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public abstract Task Execute(TRequest signature);
}

/// <summary>
/// Represents a query that retrieves data without requiring a request signature.
/// </summary>
/// <typeparam name="TResponse">The response type.</typeparam>
/// <remarks>
/// Queries represent read-only operations and should not modify application state.
/// </remarks>
public abstract class Query<TResponse> : IQuery<TResponse>
{
    /// <summary>
    /// Executes the query and returns the requested data.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the query response.
    /// </returns>
    public abstract Task<TResponse> Execute();
}

/// <summary>
/// Represents a query that retrieves data using a request signature
/// and returns a response.
/// </summary>
/// <typeparam name="TRequest">
/// The request type, which must implement <see cref="ISignature"/> and encapsulate
/// its own validation rules.
/// </typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
/// <remarks>
/// Queries represent read-only operations and must not modify application state.
/// The request signature is expected to be validated before execution.
/// </remarks>
public abstract class Query<TRequest, TResponse> : IQuery<TRequest, TResponse> where TRequest : ISignature
{
    /// <summary>
    /// Executes the query using the specified request.
    /// </summary>
    /// <param name="request">The request signature.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the query response.
    /// </returns>
    public abstract Task<TResponse> Execute(TRequest request);
}

