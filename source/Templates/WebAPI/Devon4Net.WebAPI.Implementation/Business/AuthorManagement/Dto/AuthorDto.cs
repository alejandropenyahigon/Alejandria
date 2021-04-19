using System.ComponentModel.DataAnnotations;

namespace Devon4Net.WebAPI.Implementation.Business.AuthorManagement.Dto
{
    /// <summary>
    /// Author definition
    /// </summary>
    public class AuthorDto
    {
        /// <summary>
        /// the Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// the Surname
        /// </summary>
        [Required]
        public string Surname { get; set; }

        /// <summary>
        /// the Email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// the Phone
        /// </summary>
        [Required]
        public int Phone { get; set; }
    }
}
