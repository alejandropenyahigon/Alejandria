using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Devon4Net.WebAPI.Implementation.Domain.Entities
{
    public partial class Users
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public Guid? AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}
