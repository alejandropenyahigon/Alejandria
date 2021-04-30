using System;
using System.Threading.Tasks;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;

namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Service
{
    /// <summary>
    /// IBookService
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// GetBook
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        //Task<IEnumerable<BookDto>> GetBook(Expression<Func<Book, bool>> predicate = null);

        /// <summary>
        /// GetBookById
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public Task<BookDto> GetBookByTitle(string title);

        /// <summary>
        /// CreateBook
        /// </summary>
        /// <param name="title"></param>
        /// <param name="summary"></param>
        /// <param name="genere"></param>
        /// <returns></returns>
        public Task<BookDto> CreateBook(BookDto bookDto);

        /// <summary>
        /// DeleteBookById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Guid> DeleteBookById(Guid id);

        /// <summary>
        /// ModifyBookById
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="summary"></param>
        /// <param name="genere"></param>
        /// <returns></returns>
        Task<BookDto> ModifyBookById(Guid id, BookDto bookDto);
    }
}