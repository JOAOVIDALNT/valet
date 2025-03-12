using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using valet.core.Domain.Interfaces;

namespace valet.core.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _db;
        protected DbSet<T> dbSet;

        public Repository(DbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task CreateAsync(T entity) => await dbSet.AddAsync(entity);
        public void Delete(T entity) => dbSet.Remove(entity);
        public void Update(T entity) => dbSet.Update(entity);

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, int pageSize = 0, int pageNumber = 1)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (pageSize > 0)
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;

            if (tracked)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

#pragma warning disable CS8603 // Possible null reference return.
            return await query.FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
