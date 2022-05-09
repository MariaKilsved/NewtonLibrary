using System;
using System.Collections.Generic;

namespace NewtonLibraryManager.Models
{
    public partial class User
    {
        public User()
        {
            LendingDetails = new HashSet<LendingDetail>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public bool? IsAdmin { get; set; }
        public string Password { get; set; }

        public virtual ICollection<LendingDetail> LendingDetails { get; set; }
    }
}
