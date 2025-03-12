namespace valet.core.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
