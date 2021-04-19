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
        public async Task<AuthorBook> Create(Guid authorId, Guid bookId, DateTime now, int validity)
        {
            Devon4NetLogger.Debug($"Create AuthorBook method from repository AuthorBookRepository with value : AuthorId = {authorId} y BookId= {bookId}");
            return await Create(new AuthorBook { AuthorId = authorId, BookId = bookId, PublishDate = now, ValidityDate = now.AddYears(validity) }).ConfigureAwait(false);
        }
    }
}
