using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using System;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Data.Repositories
{
    class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(AlejandriaContext context) : base(context)
        {
        }

        public async Task<Users> CreateUser(string userId, string password, string role, Guid? authorId)
        {
            Devon4NetLogger.Debug($"Create User method from repository UserRepository with value : {userId}");
            if(authorId == null) return await Create(new Users { UserId = userId, Password = password, UserRole = role }).ConfigureAwait(false);
            return await Create(new Users { UserId = userId, Password = password, UserRole = role, AuthorId = authorId }).ConfigureAwait(false);
        }
    }
}
