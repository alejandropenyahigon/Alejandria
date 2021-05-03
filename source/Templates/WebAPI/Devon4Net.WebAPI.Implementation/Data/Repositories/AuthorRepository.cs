using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Database;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Data.Repositories
{
    /// <summary>
    /// Repository implementation for the Author
    /// </summary>
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public AuthorRepository(AlejandriaContext context) : base(context)
        {           
        }

        /// <summary>
        /// Creates the Author
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Task<Author> Create(AuthorDto authorDto)
        {
            Devon4NetLogger.Debug($"Create Todo method from repository AuthorRepository with value : {authorDto.Name}");
            return Create(new Author { Name = authorDto.Name, Surname = authorDto.Surname, Email = authorDto.Email, Phone = authorDto.Phone });
        }

        /// <summary>
        /// Deletes the author with the id provided
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteAuthorById(Guid id)
        {
            Devon4NetLogger.Debug($"DeletetodoById method from repository AuthorRepository with value : {id}");
            var deleted = await Delete(x => x.Id == id).ConfigureAwait(false);

            if (deleted)
            {
                return id;
            }

            throw new ApplicationException($"The Todo entity {id} has not been deleted.");
        }

        /// <summary>
        /// Gets all the Authors
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            Devon4NetLogger.Debug("GetAllAuthors method from TodoRepository AuthorService");
            var includeList = new List<string>();
            includeList.Add("AuthorBook.Book");
            includeList.Add("Users");
            var result = await Get(includeList, x => x.Name.Equals("Miguel")).ConfigureAwait(false);
            return result;
        }
    }
}
