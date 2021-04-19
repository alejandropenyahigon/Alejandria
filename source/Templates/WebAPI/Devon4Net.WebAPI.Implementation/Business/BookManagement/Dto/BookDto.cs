using System.ComponentModel.DataAnnotations;

namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto
{
    /// <summary>
    /// Book definition
    /// </summary>
    public class BookDto
    {
        /// <summary>
        /// the Title
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// the Summary
        /// </summary>
        [Required]
        public string Summary { get; set; }

        /// <summary>
        /// the Genere
        /// </summary>
        [Required]
        public string Genere { get; set; }

    }
}
