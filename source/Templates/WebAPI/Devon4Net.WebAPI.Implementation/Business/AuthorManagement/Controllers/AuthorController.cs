using System;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Service;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Controllers
{
    /// <summary>
    /// Author controller
    /// </summary>
    [ApiController]
    [Route("v1/authormanagement")]
    [EnableCors("CorsPolicy")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authorService"></param>
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// Creates an author
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateAuthor(AuthorDto authorDto)
        {
            Devon4NetLogger.Debug("Executing CreateAuthor from controller AuthorController");
            var result = await _authorService.CreateAuthor(authorDto).ConfigureAwait(false);
            return Ok(result);
        }

        
        ///<summary>
        ///Publishes a book
        /// </summary>
        [HttpPost]
        [Route("publish")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PublishBook(Guid authorId, BookDto bookDto)
        {
            Devon4NetLogger.Debug("Executing PublishBook from controller AuthorController");
            var result = await _authorService.PublishBook(authorId, bookDto).ConfigureAwait(false);
            return Ok(result);
        }
        

        /// <summary>
        /// Lists all the authors
        /// </summary>
        [HttpGet]
        [Route("authors")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllAuthors()
        {
            Devon4NetLogger.Debug("Executing GetAllAuthors from controller AuthorController");
            var result = await _authorService.GetAllAuthors().ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Deletes an author
        /// </summary>
        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAuthor(Guid id)
        {
            Devon4NetLogger.Debug("Executing DeleteAuthor from controller AuthorController");
            var result = await _authorService.DeleteAuthorById(id).ConfigureAwait(false);
            return Ok(result);
        }

    }
}