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
        /// Checks if a role with the specified name exists.
        /// </summary>
        /// <param name="name">The name of the role to check.</param>
        /// <returns>
        /// <c>true</c> if a role with the given name exists; otherwise, <c>false</c>.
        /// </returns>
        bool RoleExists(string name);
    }
}
