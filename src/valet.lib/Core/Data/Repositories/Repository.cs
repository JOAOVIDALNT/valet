using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Core.Data.Repositories
{
    /// <summary>
    /// Generic repository implementation providing common data access operations
    /// for entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// The Entity Framework database context.
        /// </summary>
        protected readonly DbContext _db;

        /// <summary>
        /// The <see cref="DbSet{T}"/> representing the entities in the context.
        /// </summary>
        protected DbSet<T> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class
        /// with the specified database context.
        /// </summary>
        /// <param name="db">The database context.</param>
        public Repository(DbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        /// <summary>
        /// Asynchronously adds a new entity to the database context.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task CreateAsync(T entity) => await dbSet.AddAsync(entity);
        
        /// <summary>
        /// Adds a new entity to the database context.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public void Create(T entity) => dbSet.Add(entity);

        /// <summary>
        /// Removes the specified entity from the database context.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        public void Delete(T entity) => dbSet.Remove(entity);

        /// <summary>
        /// Updates the specified entity in the database context.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public void Update(T entity) => dbSet.Update(entity);

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
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, 
            int pageSize = 0, 
            int pageNumber = 1,
            bool tracked = true,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null
            )
        {
            IQueryable<T> query = dbSet;
            
            if (!tracked)
                query = query.AsNoTracking();
            
            if (include != null)
                query = include(query);

            if (filter != null)
                query = query.Where(filter);

            if (pageSize > 0)
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

            return await query.ToListAsync();
        }
        
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
        public List<T> GetAll(Expression<Func<T, bool>>? filter = null, 
            int pageSize = 0, 
            int pageNumber = 1,
            bool tracked = true,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null
        )
        {
            IQueryable<T> query = dbSet;
            
            if (!tracked)
                query = query.AsNoTracking();
            
            if (include != null)
                query = include(query);

            if (filter != null)
                query = query.Where(filter);

            if (pageSize > 0)
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

            return query.ToList();
        }

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
        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null,
            bool tracked = true,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null
            )
        {
            IQueryable<T> query = dbSet;

            if (!tracked)
                query = query.AsNoTracking();
            
            if (include != null)
                query = include(query);

            if (filter != null)
                query = query.Where(filter);

#pragma warning disable CS8603 // Possible null reference return.
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }
        
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
        public T Get(Expression<Func<T, bool>>? filter = null,
            bool tracked = true,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null
        )
        {
            IQueryable<T> query = dbSet;

            if (!tracked)
                query = query.AsNoTracking();
            
            if (include != null)
                query = include(query);

            if (filter != null)
                query = query.Where(filter);

#pragma warning disable CS8603 // Possible null reference return.
            return query.FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
