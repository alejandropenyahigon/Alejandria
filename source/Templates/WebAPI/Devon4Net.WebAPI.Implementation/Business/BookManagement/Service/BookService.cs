using System;
using System.Threading.Tasks;
using Devon4Net.Domain.UnitOfWork.Service;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Converters;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using Devon4Net.WebAPI.Implementation.Options;
using Microsoft.Extensions.Options;

namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Service
{
    /// <summary>
    /// Book service implementation
    /// </summary>
    public class BookService : Service<AlejandriaContext>, IBookService
	{
        private readonly IBookRepository _bookRepository;
        private readonly AlejandriaOptions _alejandriaOptions;

        /// <summary>
		/// Constructor
		/// </summary>
        // Se Ejecuta cada vez que se accede a un endpoint.
		public BookService(IUnitOfWork<AlejandriaContext> uoW, IOptions<AlejandriaOptions> alejandriaOptions) : base(uoW)
        {
            _bookRepository = uoW.Repository<IBookRepository>();
            _alejandriaOptions = alejandriaOptions.Value;

        }

        /// <summary>
        /// Creates the Book
        /// </summary>
        /// <param name="title"></param>
        /// <param name="summary"></param>
        /// <param name="genere"></param>
        /// <returns></returns>
        public async Task<BookDto> CreateBook(BookDto bookDto)
        {
            Devon4NetLogger.Debug($"CreateBook method from service BookService with values : Title = {bookDto.Title}, Summary = {bookDto.Summary}, Genere = {bookDto.Genere}");
            var result = await _bookRepository.Create(bookDto).ConfigureAwait(false);
            var validity =_alejandriaOptions.Validity;
            return BookConverter.ModelToDto(result);
        }

        /// <summary>
        /// Deletes the Book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteBookById(Guid id)
        {
            Devon4NetLogger.Debug($"DeleteBookById method from service BookService with value: {id}");
            return await _bookRepository.DeleteBookById(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the Book by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<BookDto> GetBookByTitle(string title)
        {
            Devon4NetLogger.Debug($"GetBookByTitle method from service BookService with value : {title}");
            var result = await _bookRepository.GetBookByTitle(title).ConfigureAwait(false);
            return BookConverter.ModelToDto(result);
        }

        /// <summary>
        /// Modifies the Book by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="summary"></param>
        /// <param name="genere"></param>
        /// <returns></returns>
        public async Task<BookDto> ModifyBookById(Guid id, BookDto bookDto)
        {
            var book = await _bookRepository.GetFirstOrDefault(b => b.Id == id).ConfigureAwait(false);

            if(book == null)
            {
                throw new Exception("The book has not been found");
            }

            book.Title = bookDto.Title;
            book.Summary = bookDto.Summary;
            book.Genere = bookDto.Genere;

            return BookConverter.ModelToDto(await _bookRepository.Update(book).ConfigureAwait(false));
        }
    }
}

