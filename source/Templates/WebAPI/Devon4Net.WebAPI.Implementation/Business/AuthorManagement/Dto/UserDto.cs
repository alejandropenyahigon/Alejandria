using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto
{
    /// <summary>
    /// Class UserDto
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// the UserId
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// the Password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// the UserRole
        /// </summary>
        [Required]
        public string UserRole { get; set; }

        /// <summary>
        /// the AuthorId
        /// </summary>
        [Required]
        public string AuthorId { get; set; }
    }
}
