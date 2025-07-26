using System.Linq.Expressions;

namespace valet.lib.Core.Domain.Interfaces
{
    /// <summary>
    /// Defines a generic repository interface for basic data access operations.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves a list of entities from the database,
        /// optionally filtered, paginated, and asynchronously loaded.
        /// </summary>
        /// <param name="filter">An optional filter expression to apply.</param>
        /// <param name="pageSize">The number of items per page. If 0, pagination is ignored.</param>
        /// <param name="pageNumber">The page number to retrieve (1-based).</param>
        /// <returns>A list of entities matching the criteria.</returns>
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, int pageSize = 0, int pageNumber = 1);

        /// <summary>
        /// Retrieves a single entity matching the specified filter,
        /// optionally tracking changes in the context.
        /// </summary>
        /// <param name="filter">An optional filter expression to apply.</param>
        /// <param name="tracked">
        /// If <c>true</c>, the entity will be tracked by the context;
        /// if <c>false</c>, no tracking will be applied (read-only).
        /// </param>
        /// <returns>The first entity matching the filter or <c>null</c> if none found.</returns>
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);

        /// <summary>
        /// Asynchronously adds a new entity to the database context.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        Task CreateAsync(T entity);

        /// <summary>
        /// Removes the specified entity from the database context.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Delete(T entity);

        /// <summary>
        /// Updates the specified entity in the database context.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);
    }
}
