using valet.lib.Auth.Domain.Entities;
using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Auth.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Defines repository operations specific to the <see cref="Role"/> entity,
    /// extending the generic <see cref="IRepository{T}"/> interface.
    /// </summary>
    public interface IRoleRepository : IRepository<Role>
    {
        /// <summary>
        /// Checks if a role with the specified name exists.
        /// </summary>
        /// <param name="name">The name of the role to check.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> that represents the asynchronous operation.
        /// The task result contains <c>true</c> if a role with the given name exists; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> RoleExistsAsync(string name);
        /// <summary>
        /// Retrieves a role with the specified name if it exists;
        /// otherwise, creates and persists a new role.
        /// </summary>
        /// <param name="name">
        /// The unique name of the role.
        /// </param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> that represents the asynchronous operation.
        /// The task result contains a <see cref="Role"/> entity tracked by the current context.
        /// </returns>
        /// <remarks>
        /// This method must be used when the returned role will participate
        /// in write operations. It guarantees that the entity is tracked.
        /// </remarks>
        Task<Role> EnsureRoleExistsAsync(string name);
    }
}
