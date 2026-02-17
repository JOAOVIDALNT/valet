namespace valet.lib.Core.Domain.Interfaces;

public interface ISystemClock
{
    DateTime UtcNow { get; }
}