namespace valet.lib.Core.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
        protected void Touch()
        {
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
