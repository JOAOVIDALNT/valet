using Microsoft.EntityFrameworkCore.Diagnostics;

namespace valet.lib.Core.Audition;

public interface IValetAuditInterceptor : ISaveChangesInterceptor
{
    
}