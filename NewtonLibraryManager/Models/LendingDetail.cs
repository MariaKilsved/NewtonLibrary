using System;
using System.Collections.Generic;

namespace NewtonLibraryManager.Models
{
    public partial class LendingDetail
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? Isbn { get; set; }
        public DateTime? BorrowedFrom { get; set; }
        public DateTime? BorrowedTo { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool? IsReservation { get; set; }

        public virtual Book? IsbnNavigation { get; set; }
        public virtual User? User { get; set; }
    }
}
