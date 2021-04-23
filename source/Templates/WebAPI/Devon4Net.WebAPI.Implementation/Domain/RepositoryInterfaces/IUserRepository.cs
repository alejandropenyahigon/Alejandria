using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    interface IUserRepository
    {
        public Task<Users> CreateUser(string userId, string password, string role, Guid? authorId);
    }
}
