using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    /// <summary>
    /// AuthorBookRepository interface
    /// </summary>
    public interface IAuthorBookRepository : IRepository<AuthorBook>
    {
        /// <summary>
        /// Creates an AuthorBook entity
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        Task<AuthorBook> Create(Guid authorId, Guid bookId, DateTime now, int validity);
    }
}
