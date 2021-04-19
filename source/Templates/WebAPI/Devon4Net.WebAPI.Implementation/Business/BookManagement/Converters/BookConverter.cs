using Devon4Net.WebAPI.Implementation.Business.BookManagement.Dto;
using Devon4Net.WebAPI.Implementation.Domain.Entities;

namespace Devon4Net.WebAPI.Implementation.Business.BookManagement.Converters
{
    /// <summary>
    /// TodoConverter
    /// </summary>
    public static class BookConverter
    {
        /// <summary>
        /// ModelToDto TODO transformation
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static BookDto ModelToDto(Book item)
        {
            if (item == null) return new BookDto();

            return new BookDto
            {
                Title = item.Title,
                Summary = item.Summary,
                Genere = item.Genere
            };
        }
    }
}
