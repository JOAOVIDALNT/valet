using Microsoft.EntityFrameworkCore;
using valet.core.Domain.Interfaces;

namespace valet.core.Data.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        protected readonly TContext _db;
        public UnitOfWork(TContext db) => _db = db;
        public async Task Commit() => await _db.SaveChangesAsync();
    }
}
