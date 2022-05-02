using System;
using System.Collections.Generic;

namespace NewtonLibraryManager.Models
{
    public partial class Author
    {
        public Author()
        {
            AuthorDetails = new HashSet<AuthorDetail>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public virtual ICollection<AuthorDetail> AuthorDetails { get; set; }
    }
}
