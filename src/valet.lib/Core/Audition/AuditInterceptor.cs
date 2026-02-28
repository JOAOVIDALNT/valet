using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Core.Audition;

public class AuditInterceptor : SaveChangesInterceptor, IValetAuditInterceptor
{
    private readonly ISystemClock _clock;

    public AuditInterceptor(ISystemClock clock)
    {
        _clock = clock;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplyAudit(DbContext? context)
    {
        if (context is null) return;

        var entries = context.ChangeTracker.Entries<IAuditable>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = _clock.Now;
                entry.Entity.UpdatedAt = _clock.Now;
            }
            
            if (entry.State == EntityState.Modified) 
                entry.Entity.UpdatedAt = _clock.Now;
        }
    }
}