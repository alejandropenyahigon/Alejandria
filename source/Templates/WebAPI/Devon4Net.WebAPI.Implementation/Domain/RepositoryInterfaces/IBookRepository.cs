using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    /// <summary>
    /// BookRepository interface
    /// </summary>
    public interface IBookRepository : IRepository<Book>
    {
        /// <summary>
        /// Creates a Book
        /// </summary>
        /// <param name="title"></param>
        /// <param name="summary"></param>
        /// <param name="genere"></param>
        /// <returns></returns>
        Task<Book> Create(BookDto bookDto);

        /// <summary>
        /// DeleteBookById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Guid> DeleteBookById(Guid id);

        /// <summary>
        /// GetBookById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Book> GetBookById(Guid id);

        /// <summary>
        /// GetBookByTitle
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        Task<Book> GetBookByTitle(string title);

        /// <summary>
        /// Deletes all Books in a List<Book> from de DB
        /// </summary>
        /// <param name="books"></param>
        /// <returns></returns>
        Task<bool> DeleteBooksFromList(IList<Book> books);

        /// <summary>
        /// Get Book by BookDto
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        Task<Book> GetBookByBookDto(BookDto bookDto);
    }
}
