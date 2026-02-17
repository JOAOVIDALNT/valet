using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Core.Audition;

public class AuditSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ISystemClock _clock;

    public AuditSaveChangesInterceptor(ISystemClock clock)
    {
        _clock = clock;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        ApplyAudit(eventData.Context);
        return result;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAudit(eventData.Context);
        return ValueTask.FromResult(result);
    }

    private void ApplyAudit(DbContext? context)
    {
        if (context is null) return;

        var entries = context.ChangeTracker.Entries<IAuditable>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added) 
                entry.Entity.CreatedAt = _clock.UtcNow;
            
            if (entry.State == EntityState.Modified) 
                entry.Entity.UpdatedAt = _clock.UtcNow;
        }
    }
}