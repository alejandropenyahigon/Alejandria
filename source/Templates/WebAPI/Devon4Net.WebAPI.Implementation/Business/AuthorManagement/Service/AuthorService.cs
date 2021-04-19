﻿using System;
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
using Devon4Net.Infrastructure.Common.Options.CircuitBreaker;
using Devon4Net.Infrastructure.CircuitBreaker;
using Devon4Net.Infrastructure.CircuitBreaker.Handler;
using System.Net.Http;
using Devon4Net.Infrastructure.CircuitBreaker.Common.Enums;
using Microsoft.AspNetCore.Mvc;

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
            _alejandriaOptions = alejandriaOptions.Value;
            _httpClientHandler = httpClientHandler;
        }

        public async Task<AuthorDto> CreateAuthor(AuthorDto authorDto)
        {
            Devon4NetLogger.Debug($"CreateAuthor method from service AuthorService with values : Name = {authorDto.Name}, Surname = {authorDto.Surname}, Email = {authorDto.Email}, Phone = {authorDto.Phone}");
            return AuthorConverter.ModelToDto(await _authorRepository.Create(authorDto).ConfigureAwait(false));
        }

        public async Task<Guid> DeleteAuthorById(Guid id)
        {
            Devon4NetLogger.Debug("DeleteAuthorById method from service AuthorService");
            var author = await _authorRepository.GetFirstOrDefault(a => a.Id == id).ConfigureAwait(false);

            if (author == null)
            {
                throw new Exception($"The author with id : {id} has not been found in the DataBase");
            }

            return await _authorRepository.DeleteAuthorById(id).ConfigureAwait(false);
        }

        public async Task<IList<AuthorDto>> GetAllAuthors()
        {
            Devon4NetLogger.Debug(" GetAllAuthors method from service AuthorService");
            var authors = await _authorRepository.GetAllAuthors().ConfigureAwait(false);
            var result = new List<AuthorDto>();

            foreach (Author i in authors)
            {
                result.Add(AuthorConverter.ModelToDto(i));
            }

            return result;
        }

        public Task<AuthorDto> GetAuthorByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Author> ModifyAuthorById(Guid id, string name, string surname, string email, int phone)
        {
            throw new NotImplementedException();
        }


        public async Task<BookDto> PublishBook(Guid authorId, BookDto bookDto)
        {
            Devon4NetLogger.Debug($"Publish method from service AuthorService with values : Title = {bookDto.Title}, Summary = {bookDto.Summary}, Genere = {bookDto.Genere}");

            if (bookDto == null || bookDto.Title == null || bookDto.Summary == null || bookDto.Genere == null)
            {
                throw new ArgumentException("A field or more should be filled");
            }

            var newBookDto = await _httpClientHandler.Send<BookDto>(HttpMethod.Post, "Books", "/v1/bookmanagement/createbook", bookDto, MediaType.ApplicationJson).ConfigureAwait(false);
            var newBook = await _bookRepository.GetFirstOrDefault(x => x.Title == bookDto.Title && x.Summary == bookDto.Summary && x.Genere == bookDto.Genere).ConfigureAwait(false);
            var authorBook = await _authorBookRepository.Create(authorId, newBook.Id, DateTime.Now, _alejandriaOptions.Validity).ConfigureAwait(false);

            return BookConverter.ModelToDto(newBook);
        }
    }
}
