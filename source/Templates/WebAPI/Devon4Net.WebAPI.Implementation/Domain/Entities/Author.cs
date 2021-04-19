using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Devon4Net.WebAPI.Implementation.Domain.Entities
{
    public partial class Author
    {
        public Author()
        {
            AuthorBook = new HashSet<AuthorBook>();
        }

        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AuthorBook> AuthorBook { get; set; }
    }
}
