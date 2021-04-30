using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Devon4Net.Domain.UnitOfWork.Service;
using Devon4Net.Domain.UnitOfWork.UnitOfWork;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Converters;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Service;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Converters;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using Devon4Net.WebAPI.Implementation.Options;
using Microsoft.Extensions.Options;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using System.Net.Http;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using System.Linq;

namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Service
{
    /// <summary>
    /// Book service implementation
    /// </summary>
    public class AuthorService : Service<AlejandriaContext>, IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorBookRepository _authorBookRepository;
        private readonly IUserRepository _userBookRepository;
        private readonly AlejandriaOptions _alejandriaOptions;
        private readonly IHttpClientHandler _httpClientHandler;

        /// <summary>
        /// Constructor
        /// </summary>
        // Se Ejecuta cada vez que se accede a un endpoint.
        public AuthorService(IUnitOfWork<AlejandriaContext> uoW, IOptions<AlejandriaOptions> alejandriaOptions, IHttpClientHandler httpClientHandler) : base(uoW)
        {
            _authorRepository = uoW.Repository<IAuthorRepository>();
            _bookRepository = uoW.Repository<IBookRepository>();
            _authorBookRepository = uoW.Repository<IAuthorBookRepository>();
            _userBookRepository = uoW.Repository<IUserRepository>();
            _alejandriaOptions = alejandriaOptions.Value;
            _httpClientHandler = httpClientHandler;
        }

        /// <summary>
        /// Creates an author
        /// </summary>
        /// <param name="authorDto"></param>
        /// <returns></returns>
        public async Task<AuthorDto> CreateAuthor(AuthorDto authorDto)
        {
            Devon4NetLogger.Debug($"CreateAuthor method from service AuthorService with values : Name = {authorDto.Name}, Surname = {authorDto.Surname}, Email = {authorDto.Email}, Phone = {authorDto.Phone}");
            return AuthorConverter.ModelToDto(await _authorRepository.Create(authorDto).ConfigureAwait(false));
        }

        /// <summary>
        /// Deletes the author with the id provided
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteAuthorById(Guid id)
        {
            Devon4NetLogger.Debug("DeleteAuthorById method from service AuthorService");
            var author = await _authorRepository.GetFirstOrDefault(a => a.Id == id).ConfigureAwait(false);
            if (author == null)
            {
                throw new Exception($"The author with id : {id} has not been found in the DataBase");
            }

            var authorBooks = await _authorBookRepository.Get(x => x.AuthorId == id).ConfigureAwait(false);
            var books = await GetBooksByAuthorBooks(authorBooks).ConfigureAwait(false);

            var authorBooksDeleted = await _authorBookRepository.DeleteAuthorBooksFromList(authorBooks).ConfigureAwait(false);
            if (!authorBooksDeleted)
            {
                throw new Exception($"Author with id: {id} was found but authorbooks related with it could not be deleted");
            }

            var booksDeleted = await _bookRepository.DeleteBooksFromList(books).ConfigureAwait(false);
            if (!booksDeleted)
            {
                throw new Exception($"Author with id: {id} was found but books published by it could not be deleted");
            }

            return await _authorRepository.DeleteAuthorById(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the Books related with the AuthorBooks provided
        /// </summary>
        /// <param name="authorBooks"></param>
        /// <returns></returns>
        private async Task<IList<Book>> GetBooksByAuthorBooks(IList<AuthorBook> authorBooks)
        {
            var books = new List<Book>();
            foreach (AuthorBook a in authorBooks)
            {
                books.Add(await _bookRepository.GetBookById(a.BookId).ConfigureAwait(false));
            }
            return books;
        }

        /// <summary>
        /// Returns all the authors
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AuthorDto>> GetAllAuthors()
        {
            Devon4NetLogger.Debug(" GetAllAuthors method from service AuthorService");
            var authors = await _authorRepository.GetAllAuthors().ConfigureAwait(false);
            
            return authors.Select(AuthorConverter.ModelToDto);
        }

        /// <summary>
        /// Gets the author with the name provided
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<AuthorDto> GetAuthorByName(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Modifies the author with the id provided
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Task<Author> ModifyAuthorById(Guid id, string name, string surname, string email, int phone)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Publishes a Book and AuthorBook with linked with the author provided
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        public async Task<BookDto> PublishBook(Guid authorId, BookDto bookDto)
        {
            Devon4NetLogger.Debug($"Publish method from service AuthorService with values : Title = {bookDto.Title}, Summary = {bookDto.Summary}, Genere = {bookDto.Genere}");

            if (bookDto == null || bookDto.Title == null || bookDto.Summary == null || bookDto.Genere == null)
            {
                throw new ArgumentException("A field or more should be filled");
            }

            var transaction = await UoW.BeginTransaction().ConfigureAwait(false);
            try
            {
                var newBookDto = await _httpClientHandler.Send<BookDto>(HttpMethod.Post, "Books", "v1/bookmanagement/createbook", bookDto, MediaType.ApplicationJson, null, true, true).ConfigureAwait(false);
                var newBook = await _bookRepository.GetBookByBookDto(newBookDto).ConfigureAwait(false);
                var authorBook = await _authorBookRepository.Create(authorId, newBook.Id, DateTime.Now, DateTime.Now.AddYears(_alejandriaOptions.Validity)).ConfigureAwait(false);

                await UoW.Commit(transaction).ConfigureAwait(false);
                return BookConverter.ModelToDto(newBook);
            }
            catch
            {
                await UoW.Rollback(transaction).ConfigureAwait(false);
                throw new Exception("A problem has occured while executing the method PublishBook from class AuthorService");
            }
        }

        /// <summary>
        /// Creates an User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <param name="authorDto"></param>
        /// <returns></returns>
        public async Task<UserDto> CreateUser(string userId, string password, string role, AuthorDto authorDto = null)
        {
            Devon4NetLogger.Debug($"Executing method CreateUser from class AuthorService with values : userId = {userId}, password = {password}, role = {role}");

            if (authorDto == null) return UserConverter.ModelToDto(await _userBookRepository.CreateUser(userId, password, role, null).ConfigureAwait(false));

            var transaction = await UoW.BeginTransaction().ConfigureAwait(false);
            try
            {
                var newAuthor = await _authorRepository.Create(authorDto).ConfigureAwait(false);
                var newUserDto = UserConverter.ModelToDto(await _userBookRepository.CreateUser(userId, password, role, newAuthor.Id).ConfigureAwait(false));
                await UoW.Commit(transaction).ConfigureAwait(false);
                return newUserDto;
            }
            catch
            {
                await UoW.Rollback(transaction).ConfigureAwait(false);
                throw new Exception("An error ocurred while creating the new User");
            }
        }

        /// <summary>
        /// Returns the User information that matches with the Credentials provided
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public async Task<UserDto> UserLogin(LoginDto loginDto)
        {
            Devon4NetLogger.Debug($"Executing method LoginUser from class AuthorService with values : UserId = {loginDto.UserId} and Password = {loginDto.Password}");
            var userInDb = await _userBookRepository.GetUserByCredentials(loginDto.UserId, loginDto.Password).ConfigureAwait(false);
            if (userInDb == null) 
            {
                return null;
            }
            return UserConverter.ModelToDto(userInDb);
        }
    }
}
