namespace valet.lib.Core.Domain.Entities
{
    /// <summary>
    /// Represents a base entity with common properties for identification and tracking.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets the unique identifier for the entity.
        /// </summary>
        public Guid Id { get; private set; } = Guid.NewGuid();
    }
}
