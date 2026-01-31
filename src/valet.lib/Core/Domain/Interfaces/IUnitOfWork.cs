namespace valet.lib.Core.Domain.Interfaces
{
    /// <summary>
    /// Defines a unit of work that coordinates changes across multiple repositories
    /// and ensures atomicity through transactional boundaries.
    /// </summary>
    /// <remarks>
    /// A unit of work represents a single application-level operation, typically
    /// corresponding to one use case or one HTTP request.
    /// It is responsible for managing database transactions and persisting changes
    /// in a consistent and reliable manner.
    /// </remarks>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Begins a new database transaction.
        /// </summary>
        /// <param name="cancellationToken">
        /// A token used to cancel the asynchronous operation.
        /// </param>
        /// <remarks>
        /// After calling this method, all subsequent changes tracked by the underlying
        /// data context will be executed within the same transactional scope until
        /// <see cref="CommitAsync"/> or <see cref="RollbackAsync"/> is called.
        /// </remarks>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Commits all changes made during the current transaction.
        /// </summary>
        /// <param name="cancellationToken">
        /// A token used to cancel the asynchronous operation.
        /// </param>
        /// <remarks>
        /// This method persists all pending changes and finalizes the current transaction.
        /// If no explicit transaction was started, it commits the changes using the
        /// underlying data context default behavior.
        /// </remarks>
        Task CommitAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Rolls back all changes made during the current transaction.
        /// </summary>
        /// <param name="cancellationToken">
        /// A token used to cancel the asynchronous operation.
        /// </param>
        /// <remarks>
        /// After a rollback, all changes performed within the transaction scope
        /// are discarded and the database state is restored to its previous state.
        /// </remarks>
        Task RollbackAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// Persists all pending changes to the underlying data store without
        /// explicitly managing a transaction.
        /// </summary>
        /// <param name="cancellationToken">
        /// A token used to cancel the asynchronous operation.
        /// </param>
        /// <remarks>
        /// This method delegates directly to the data context save mechanism.
        /// When called outside an explicit transaction, the persistence behavior
        /// depends on the underlying ORM implementation.
        /// </remarks>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
