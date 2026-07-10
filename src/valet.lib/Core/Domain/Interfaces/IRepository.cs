using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace valet.lib.Core.Domain.Interfaces
{
    /// <summary>
    /// Defines a generic repository interface for basic data access operations.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Asynchronously retrieves a list of entities from the database,
        /// optionally filtered, paginated, tracked, and including related data.
        /// </summary>
        /// <param name="query">
        /// An optional function used to compose the query.
        /// This function may apply filtering, includes, ordering, projections,
        /// or other transformations supported by Entity Framework.
        /// Client-side evaluation and query materialization
        /// (e.g. ToList, AsEnumerable) should be avoided.
        /// </param>
        /// <param name="pageSize">
        /// The number of items per page. If set to <c>0</c>, pagination is not applied.
        /// </param>
        /// <param name="pageNumber">The page number to retrieve (1-based).</param>
        /// <param name="tracked">
        /// Indicates whether the entities should be tracked by the context.
        /// </param>
        /// <param name="cancellationToken">A token to cancel the async operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a list of entities matching the criteria.
        /// </returns>
        Task<IReadOnlyList<T>> GetAllAsync(
            Func<IQueryable<T>, IQueryable<T>>? query = null,
            int pageSize = 0, 
            int pageNumber = 1, 
            bool tracked = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously returns the count of entities matching the optional query.
        /// </summary>
        /// <param name="query">
        /// An optional function to filter the query before counting.
        /// </param>
        /// <param name="cancellationToken">A token to cancel the async operation.</param>
        /// <returns>The number of matching entities.</returns>
        Task<int> CountAsync(
            Func<IQueryable<T>, IQueryable<T>>? query = null,
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Asynchronously retrieves a single entity that matches the specified filter,
        /// optionally tracking changes and including related data.
        /// </summary>
        /// <param name="query">
        /// An optional function used to compose the query.
        /// This function may apply filtering, includes, ordering, projections,
        /// or other transformations supported by Entity Framework.
        /// Client-side evaluation and query materialization
        /// (e.g. ToList, AsEnumerable) should be avoided.
        /// </param>
        /// <param name="tracked">
        /// Indicates whether the entity should be tracked by the context.
        /// </param>
        /// <param name="cancellationToken">A token to cancel the async operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first matching entity,
        /// or <c>null</c> if no entity is found.
        /// </returns>
        Task<T?> GetAsync(
            Func<IQueryable<T>, IQueryable<T>>? query = null,
            bool tracked = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously adds a new entity to the database context.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
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
