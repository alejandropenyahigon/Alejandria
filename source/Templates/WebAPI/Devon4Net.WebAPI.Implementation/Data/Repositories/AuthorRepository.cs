﻿using Devon4Net.Domain.UnitOfWork.Repository;
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
        /// Creates the TODO
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
        /// Gets all the TODO
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            Devon4NetLogger.Debug("GetAllAuthors method from TodoRepository AuthorService");
            return await Get().ConfigureAwait(false);
        }        
    }
}
