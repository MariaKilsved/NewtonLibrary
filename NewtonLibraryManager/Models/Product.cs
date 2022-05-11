using System;
using System.Collections.Generic;

namespace NewtonLibraryManager.Models
{
    public partial class Product
    {
        public Product()
        {
            AuthorDetails = new HashSet<AuthorDetail>();
            LendingDetails = new HashSet<LendingDetail>();
            ReservationDetails = new HashSet<ReservationDetail>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? LanguageId { get; set; }
        public int? CategoryId { get; set; }
        public int? NrOfCopies { get; set; }
        public decimal? Dewey { get; set; }
        public string Description { get; set; }
        public string Isbn { get; set; }
        public int? ProductType { get; set; }

        public virtual ICollection<AuthorDetail> AuthorDetails { get; set; }
        public virtual ICollection<LendingDetail> LendingDetails { get; set; }
        public virtual ICollection<ReservationDetail> ReservationDetails { get; set; }
    }
}
