using Microsoft.EntityFrameworkCore;
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
        protected readonly DbSet<T> dbSet;

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
        
        public async Task CreateAsync(T entity) => await dbSet.AddAsync(entity);

        public void Delete(T entity) => dbSet.Remove(entity);

        public void Update(T entity) => dbSet.Update(entity);
        
        public async Task<IReadOnlyList<T>> GetAllAsync(
            Func<IQueryable<T>, IQueryable<T>>? query = null,
            int pageSize = 0, 
            int pageNumber = 1,
            bool tracked = false,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> q = tracked ? dbSet : dbSet.AsNoTracking();
            
            if (query != null)
                q = query(q);

            if (pageSize > 0)
                q = q.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

            return await q.ToListAsync(cancellationToken);
        }

        public async Task<int> CountAsync(
            Func<IQueryable<T>, IQueryable<T>>? query = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> q = dbSet.AsNoTracking();

            if (query != null)
                q = query(q);

            return await q.CountAsync(cancellationToken);
        }
        
        public async Task<T?> GetAsync(
            Func<IQueryable<T>, IQueryable<T>>? query = null,
            bool tracked = false,
            CancellationToken cancellationToken = default)
        {
            IQueryable<T> q = tracked ? dbSet : dbSet.AsNoTracking();

            if (query != null)
                q = query(q);
            
            return await q.FirstOrDefaultAsync(cancellationToken);
        }
        
    }
}
