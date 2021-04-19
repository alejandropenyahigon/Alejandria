﻿using Devon4Net.Domain.UnitOfWork.Repository;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Devon4Net.WebAPI.Implementation.Domain.RepositoryInterfaces
{
    /// <summary>
    /// BookRepository interface
    /// </summary>
    public interface IBookRepository : IRepository<Book>
    {
        /// <summary>
        /// Create
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
    }
}