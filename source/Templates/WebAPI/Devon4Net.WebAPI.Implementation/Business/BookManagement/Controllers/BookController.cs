using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Controllers
{
    /// <summary>
    /// Books controller
    /// </summary>
    [ApiController]
    [Route("v1/bookmanagement")]
    [EnableCors("CorsPolicy")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bookService"></param>
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Gets the book with the title specified if there is one
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("booksbytitle")]
        [ProducesResponseType(typeof(List<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBook([FromQuery] string title)
        {
            Devon4NetLogger.Debug("Executing GetBook from controller BookController");
            return Ok(await _bookService.GetBookByTitle(title).ConfigureAwait(false));
        }
        
        /// <summary>
        /// Creates a book
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("createbook")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateBook(BookDto bookDto)
        {
            Devon4NetLogger.Debug("Executing GetBook from controller BookController");
            var result = await _bookService.CreateBook(bookDto).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Deletes the book provided the id
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("deletebook")]
        //[ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteBook([Required] Guid bookId)
        {
            Devon4NetLogger.Debug("Executing DeleteBook from controller BookController");
            return Ok(await _bookService.DeleteBookById(bookId).ConfigureAwait(false));
        }

        /// <summary>
        /// Modifies the done status of the book provided the data of the book
        /// In this sample, all the data fields are mandatory
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("modifybook")]
        //[ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ModifyBookById(Guid id, BookDto bookDto)
        {
            Devon4NetLogger.Debug("Executing ModifyBookById from controller BookController");
            if (id == null || id == Guid.Empty)
            {
                return BadRequest("The id of the book must be provided and different from 0");
            }
            return Ok(await _bookService.ModifyBookById(id, bookDto).ConfigureAwait(false));
        }

    }
}