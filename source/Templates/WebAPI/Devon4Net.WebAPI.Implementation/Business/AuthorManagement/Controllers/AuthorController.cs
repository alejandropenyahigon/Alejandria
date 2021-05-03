using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Returns the User credentials that matches the Credentials provided
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
        /// Creates an Author
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status200OK)]
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
        ///Publishes a Book
        /// </summary>
        [HttpPost]
        [HttpOptions]
        //[Authorize(AuthenticationSchemes = AuthConst.AuthenticationScheme, Roles = AuthConst.AlejandriaAuthor)]
        [Route("publish")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
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
        /// Lists all the Authors
        /// </summary>
        [HttpGet]
        [Route("authors")]
        [ProducesResponseType(typeof(IEnumerable<AuthorDto>), StatusCodes.Status200OK)]
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
        /// Deletes an Author
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

        //TODO: Refactor Arguments
        /// <summary>
        /// Creates an User
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createuser")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateUser(string userId, string password, string userRole, AuthorDto author)
        {
            Devon4NetLogger.Debug($"Executing method CreateUser from class AuthorController with values : userId = {userId}, password = {password}, role = {userRole}");
            return Ok(await _authorService.CreateUser(userId, password, userRole, author).ConfigureAwait(false));
        }

        //TODO: Refactor JWT
        /// <summary>
        /// Returns the JWT for the User that matches the Credentials provided
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("userlogin")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UserLogin([FromBody] LoginDto loginDto)
        {
            Devon4NetLogger.Debug($"Executing method UserLogin from class AuthorController with values : UserId = {loginDto.UserId} and Password = {loginDto.Password}");
            var result = await _authorService.UserLogin(loginDto).ConfigureAwait(false);
            if (result == null) return NotFound("User not found, try to create a new account");

            var token = JwtHandler.CreateClientToken(new List<Claim>
            {
                new Claim(ClaimTypes.Role, result.UserRole),
                new Claim(ClaimTypes.NameIdentifier, result.UserId),
                new Claim("AuthorId", result.AuthorId)
            });

            return Ok(new LoginResponse { Token = token });
        }
    }
}