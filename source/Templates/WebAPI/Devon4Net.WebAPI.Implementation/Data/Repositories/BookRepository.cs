using Devon4Net.Domain.UnitOfWork.Pagination;
using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Data.Repositories
{
    /// <summary>
    /// Repository implementation for the Book
    /// </summary>
    public class BookRepository : Repository<Book>, IBookRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public BookRepository(AlejandriaContext context) : base(context)
        {
        }

        /// <summary>
        /// Create Book
        /// </summary>
        /// <param name="title"></param>
        /// <param name="summary"></param>
        /// <param name="genere"></param>
        /// <returns></returns>
        public Task<Book> Create(BookDto bookDto)
        {
            Devon4NetLogger.Debug($"SetTodo method from repository BookService with value: {bookDto.Title}");
            return Create(new Book {Title = bookDto.Title, Summary = bookDto.Summary, Genere = bookDto.Genere });
        }

        /// <summary>
        /// Deletes the book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteBookById(Guid id)
        {
            Devon4NetLogger.Debug($"DeleteBookById method from repository BookRepository with value : {id}");
            var deleted = await Delete(x => x.Id == id).ConfigureAwait(false);

            if (deleted)
            {
                return id;
            }

            throw new ApplicationException($"The Book entity {id} has not been deleted.");
        }

        /// <summary>
        /// Deletes all Books in a List<Book> from de DB
        /// </summary>
        /// <param name="books"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBooksFromList(IList<Book> books)
        {
            Devon4NetLogger.Debug($"DeleteBookFromList method from BookRepository");
            
            foreach(Book b in books)
            {
                var itemDeleted = await Delete(x => x.Id == b.Id).ConfigureAwait(false);
                //Maybe let the controller handle this
                if (!itemDeleted)
                {
                    Devon4NetLogger.Debug($"The book with value : {b.Title} could not be removed");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets the Book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Book> GetBookById(Guid id)
        {
            Devon4NetLogger.Debug($"GetBookById method from repository BookRepository with value : {id}");
            return GetFirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Gets the Book by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public Task<Book> GetBookByTitle(string title)
        {
            Devon4NetLogger.Debug($"GetBookByTitle method from repository BookRepository with value : {title}");
            return GetFirstOrDefault(x => x.Title == title);
        }

        public Task<Book> GetBookByBookDto(BookDto bookDto)
        {
            Devon4NetLogger.Debug($"GetBookByBookDto method from repository BookRepository with value : {bookDto.Title}");
            return GetFirstOrDefault(x => x.Title == bookDto.Title && x.Summary == bookDto.Summary && x.Genere == bookDto.Summary);
        }
    }
}
