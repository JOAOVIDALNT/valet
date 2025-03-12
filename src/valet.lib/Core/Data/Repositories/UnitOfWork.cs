using Microsoft.EntityFrameworkCore;
using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Core.Data.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        protected readonly TContext _db;
        public UnitOfWork(TContext db) => _db = db;
        public async Task Commit() => await _db.SaveChangesAsync();
    }
}
