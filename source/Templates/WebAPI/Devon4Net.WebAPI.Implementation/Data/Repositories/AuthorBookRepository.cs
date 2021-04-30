using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Data.Repositories
{
    /// <summary>
    /// Repository implementation for the AuthorBook
    /// </summary>
    public class AuthorBookRepository : Repository<AuthorBook>, IAuthorBookRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public AuthorBookRepository(AlejandriaContext context) : base(context)
        {
        }

        /// <summary>
        /// Creates an AuthorBook entity
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public Task<AuthorBook> Create(Guid authorId, Guid bookId, DateTime now, DateTime validityDate)
        {
            Devon4NetLogger.Debug($"Create AuthorBook method from repository AuthorBookRepository with value : AuthorId = {authorId} y BookId= {bookId}");
            return Create(new AuthorBook { AuthorId = authorId, BookId = bookId, PublishDate = now, ValidityDate = validityDate });
        }

        public async Task<Guid> DeleteAuthorBookByBookID(Guid id)
        {
            Devon4NetLogger.Debug($"DeleteAuthorBookByBookID method from repository AuthorBookRepository with value : BookId = {id}");
            var deleted = await Delete(x => x.BookId == id).ConfigureAwait(false);
            if (deleted)
            {
                return id;
            }

            throw new ApplicationException($"The AuthorBook with BookId {id} has not been deleted.");
        }

        public async Task<bool> DeleteAuthorBooksFromList(IList<AuthorBook> authorBooks)
        {
            Devon4NetLogger.Debug($"DeleteAuthorBookFromList method from AuthorBookRepository");

            foreach(AuthorBook a in authorBooks)
            {
                var itemDeleted = await Delete(x => x.Id == a.Id).ConfigureAwait(false);
                if (!itemDeleted)
                {
                    Devon4NetLogger.Debug($"The AuthorBook entitie with value : {a.Id} could not be removed");
                    return false;
                }
            }
            return true;
        }
    }
}
