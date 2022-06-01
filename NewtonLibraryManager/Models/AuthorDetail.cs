using System;
using System.Collections.Generic;

namespace NewtonLibraryManager.Models
{
    public partial class AuthorDetail
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int ProductId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Product Product { get; set; }
    }
}
