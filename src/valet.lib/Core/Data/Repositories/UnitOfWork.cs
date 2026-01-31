using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Core.Data.Repositories
{
    internal class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        protected readonly TContext _context;
        private IDbContextTransaction? _transaction;
        
        public UnitOfWork(TContext context) => _context = context;
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
                throw new InvalidOperationException("Transaction already started.");
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
                throw new InvalidOperationException("No active transaction to commit.");

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                await _transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackAsync(cancellationToken);
                throw;
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
                return;
            await _transaction.RollbackAsync(cancellationToken);
            await DisposeTransactionAsync();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) 
            => await _context.SaveChangesAsync(cancellationToken); 
        
        private async Task DisposeTransactionAsync()
        {
            await _transaction!.DisposeAsync();
            _transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
