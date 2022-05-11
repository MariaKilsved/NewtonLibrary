using System;
using System.Collections.Generic;

namespace NewtonLibraryManager.Models
{
    public partial class ReservationDetail
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? ReservationDate { get; set; }
        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
