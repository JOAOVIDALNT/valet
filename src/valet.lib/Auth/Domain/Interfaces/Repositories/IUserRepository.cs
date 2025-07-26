using valet.lib.Auth.Domain.Entities;
using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Auth.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Defines repository operations specific to the <see cref="User"/> entity,
    /// extending the generic <see cref="IRepository{T}"/> interface.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Checks if a user with the specified email exists.
        /// </summary>
        /// <param name="email">The email of the user to check.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> that represents the asynchronous operation.
        /// The task result contains <c>true</c> if a user with the given email exists; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> UserExists(string email);

        /// <summary>
        /// Checks if a user with the specified identifier exists.
        /// </summary>
        /// <param name="identifier">The unique identifier (GUID) of the user to check.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> that represents the asynchronous operation.
        /// The task result contains <c>true</c> if a user with the given identifier exists; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> UserExists(Guid identifier);

        /// <summary>
        /// Retrieves a user by email along with their associated roles.
        /// </summary>
        /// <param name="email">The email of the user to retrieve.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="User"/> including its roles.
        /// </returns>
        Task<User> GetUserWithRolesAsync(string email);
    }
}
