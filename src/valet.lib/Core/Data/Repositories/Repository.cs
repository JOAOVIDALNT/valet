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
        
        public async Task CreateAsync(T entity) => await dbSet.AddAsync(entity);

        public void Delete(T entity) => dbSet.Remove(entity);

        public void Update(T entity) => dbSet.Update(entity);
        
        public async Task<List<T>> GetAllAsync(
            Func<IQueryable<T>, IQueryable<T>>? query = null,
            int pageSize = 0, 
            int pageNumber = 1,
            bool tracked = false
            )
        {
            IQueryable<T> q = tracked ? dbSet : dbSet.AsNoTracking();
            
            if (query != null)
                q = query(q);

            if (pageSize > 0)
                q = q.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

            return await q.ToListAsync();
        }
        
        public async Task<T> GetAsync(
            Func<IQueryable<T>, IQueryable<T>>? query = null,
            bool tracked = false
            )
        {
            IQueryable<T> q = tracked ? dbSet : dbSet.AsNoTracking();

            if (query != null)
                q = query(q);

#pragma warning disable CS8603 // Possible null reference return.
            return await q.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }
        
    }
}
