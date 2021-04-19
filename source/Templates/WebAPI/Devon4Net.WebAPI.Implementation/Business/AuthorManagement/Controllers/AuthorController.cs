using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Devon4Net.Infrastructure.JWT.Common.Const;
using Devon4Net.Infrastructure.JWT.Handlers;
using Devon4Net.Infrastructure.Log;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Service;
using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Business.UserManagement.Dto;
using Microsoft.AspNetCore.Authorization;
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
        private IJwtHandler JwtHandler { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authorService"></param>
        public AuthorController(IAuthorService authorService, IJwtHandler jwtHandler)
        {
            _authorService = authorService;
            JwtHandler = jwtHandler;
        }

        [HttpPost]
        [HttpOptions]
        [AllowAnonymous]
        [Route("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login(string user, string password)
        {
            Devon4NetLogger.Debug("Executing Login from controller AuthorController");

            var token = JwtHandler.CreateClientToken(new List<Claim>
            {
                new Claim(ClaimTypes.Role, AuthConst.AlejandriaAuthor),
                new Claim(ClaimTypes.Name, user),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            });

            return Ok(new LoginResponse { Token = token });
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
        [HttpOptions]
        [Authorize(AuthenticationSchemes = AuthConst.AuthenticationScheme, Roles = AuthConst.AlejandriaAuthor)]
        [Route("publish")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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