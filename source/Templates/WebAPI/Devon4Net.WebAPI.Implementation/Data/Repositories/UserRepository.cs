using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using System;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Data.Repositories
{
    /// <summary>
    /// Repository implementation for the User
    /// </summary>
    public class UserRepository : Repository<Users>, IUserRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(AlejandriaContext context) : base(context)
        {
        }

        /// <summary>
        /// Creates a User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <param name="authorId"></param>
        /// <returns></returns>
        public async Task<Users> CreateUser(string userId, string password, string role, Guid? authorId)
        {
            Devon4NetLogger.Debug($"Create User method from repository UserRepository with value : {userId}");
            if(authorId == null) return await Create(new Users { UserId = userId, Password = password, UserRole = role }).ConfigureAwait(false);
            return await Create(new Users { UserId = userId, Password = password, UserRole = role, AuthorId = authorId }).ConfigureAwait(false);
        }
    }
}
