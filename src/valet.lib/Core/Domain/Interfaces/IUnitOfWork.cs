namespace valet.lib.Core.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
