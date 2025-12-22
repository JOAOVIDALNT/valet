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

        /// <summary>
        /// Gets the timestamp indicating when the entity was created (in UTC).
        /// </summary>
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets the timestamp indicating when the entity was last updated (in UTC).
        /// </summary>
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// Updates the <see cref="UpdatedAt"/> timestamp to the current UTC time.
        /// Used to mark the entity as modified.
        /// </summary>
        protected void Touch()
        {
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
