using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Service
{
    /// <summary>
    /// IAuthorService
    /// </summary>
    public interface IAuthorService
    {
        /// <summary>
        /// GetAuthor
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        //Task<IEnumerable<BookDto>> GetBook(Expression<Func<Book, bool>> predicate = null);

        /// <summary>
        /// GetAuthorById
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public Task<AuthorDto> GetAuthorByName(string name);

        /// <summary>
        /// CreateAuthor
        /// </summary>
        /// <param name="title"></param>
        /// <param name="summary"></param>
        /// <param name="genere"></param>
        /// <returns></returns>
        public Task<AuthorDto> CreateAuthor(AuthorDto authorDto);

        /// <summary>
        /// DeleteAuthorById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Guid> DeleteAuthorById(Guid id);

        /// <summary>
        /// ModifyAuthorById
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="summary"></param>
        /// <param name="genere"></param>
        /// <returns></returns>
        public Task<Author> ModifyAuthorById(Guid id, string name, string surname, string email, int phone);

        
        /// <summary>
        /// PublishBook
        /// </summary>
        /// <param name="title"></param>
        /// <param name="summary"></param>
        /// <param name="genere"></param>
        /// <returns>Book Published</returns>
        public Task<BookDto> PublishBook(Guid authorId, BookDto bookDto);
        

        /// <summary>
        /// Lists all Authors
        /// </summary>
        /// <returns>AuthorListDto</returns>
        public Task<IEnumerable<AuthorDto>> GetAllAuthors();

        public Task<AuthorDto> GetAuthorById(Guid id);

        /// <summary>
        /// Creates a new User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <param name="authorDto"></param>
        /// <returns></returns>
        public Task<UserDto> CreateUser(string userId, string password, string role, AuthorDto authorDto);

        /// <summary>
        /// Returns if there is a User with the same credentials in the DataBase
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<UserDto> UserLogin(LoginDto loginDto);
    }
}