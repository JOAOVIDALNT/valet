namespace valet.lib.Core.Domain.Interfaces
{
    /// <summary>
    /// Represents a unit of work that coordinates the writing of changes to the database.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commits all changes made in the current transaction asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous commit operation.</returns>
        Task CommitAsync();
    }
}
