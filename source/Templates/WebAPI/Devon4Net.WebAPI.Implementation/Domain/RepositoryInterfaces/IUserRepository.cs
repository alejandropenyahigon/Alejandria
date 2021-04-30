using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    /// <summary>
    /// UserRepository interface
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <param name="authorId"></param>
        /// <returns></returns>
        public Task<Users> CreateUser(string userId, string password, string role, Guid? authorId);

        /// <summary>
        /// Gets a User from de database with the credentials in the arguments
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<Users> GetUserByCredentials(string userId, string password);
    }
}
