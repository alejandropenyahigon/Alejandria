using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Devon4Net.WebAPI.Implementation.Domain.Entities
{
    public partial class AuthorBook
    {
        public Guid Id { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? ValidityDate { get; set; }
        public Guid AuthorId { get; set; }
        public Guid BookId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
