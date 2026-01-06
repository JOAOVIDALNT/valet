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
        /// <param name="filter">An optional filter expression.</param>
        /// <param name="pageSize">
        /// The number of items per page. If set to <c>0</c>, pagination is not applied.
        /// </param>
        /// <param name="pageNumber">The page number to retrieve (1-based).</param>
        /// <param name="tracked">
        /// Indicates whether the entities should be tracked by the context.
        /// </param>
        /// <param name="include">
        /// An optional function to include related navigation properties.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a list of entities matching the criteria.
        /// </returns>
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, 
            int pageSize = 0, 
            int pageNumber = 1, 
            bool tracked = true, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        
        /// <summary>
        /// Retrieves a list of entities from the database,
        /// optionally filtered, paginated, tracked, and including related data.
        /// </summary>
        /// <param name="filter">An optional filter expression.</param>
        /// <param name="pageSize">
        /// The number of items per page. If set to <c>0</c>, pagination is not applied.
        /// </param>
        /// <param name="pageNumber">The page number to retrieve (1-based).</param>
        /// <param name="tracked">
        /// Indicates whether the entities should be tracked by the context.
        /// </param>
        /// <param name="include">
        /// An optional function to include related navigation properties.
        /// </param>
        /// <returns>
        /// A list of entities matching the specified criteria.
        /// </returns>
        List<T> GetAll(Expression<Func<T, bool>>? filter = null, 
            int pageSize = 0, 
            int pageNumber = 1, 
            bool tracked = true, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        /// <summary>
        /// Asynchronously retrieves a single entity that matches the specified filter,
        /// optionally tracking changes and including related data.
        /// </summary>
        /// <param name="filter">An optional filter expression.</param>
        /// <param name="tracked">
        /// Indicates whether the entity should be tracked by the context.
        /// </param>
        /// <param name="include">
        /// An optional function to include related navigation properties.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first matching entity,
        /// or <c>null</c> if no entity is found.
        /// </returns>
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, 
            bool tracked = true, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        
        /// <summary>
        /// Retrieves a single entity that matches the specified filter,
        /// optionally tracking changes and including related data.
        /// </summary>
        /// <param name="filter">An optional filter expression.</param>
        /// <param name="tracked">
        /// Indicates whether the entity should be tracked by the context.
        /// </param>
        /// <param name="include">
        /// An optional function to include related navigation properties.
        /// </param>
        /// <returns>
        /// The first matching entity, or <c>null</c> if no entity is found.
        /// </returns>
        T Get(Expression<Func<T, bool>>? filter = null, 
            bool tracked = true, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        /// <summary>
        /// Asynchronously adds a new entity to the database context.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task CreateAsync(T entity);
        
        /// <summary>
        /// Adds a new entity to the database context.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Create(T entity);

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
