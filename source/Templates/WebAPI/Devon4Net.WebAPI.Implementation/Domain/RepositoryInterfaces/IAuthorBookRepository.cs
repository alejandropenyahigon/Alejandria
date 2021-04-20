using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Collections.Generic;
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
        Task<AuthorBook> Create(Guid authorId, Guid bookId, DateTime now, DateTime validityDate);

        /// <summary>
        /// Deletes AuthorBook with BookId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Guid> DeleteAuthorBookByBookID(Guid id);

        /// <summary>
        /// Deletes all AuthorBooks in a List<AuthorBook> from de DB
        /// </summary>
        /// <param name="authorBooks"></param>
        /// <returns></returns>
        Task<bool> DeleteAuthorBooksFromList(IList<AuthorBook> authorBooks);
    }
}
