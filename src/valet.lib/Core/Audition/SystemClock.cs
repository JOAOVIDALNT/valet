using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Core.Audition;

public class SystemClock : ISystemClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}