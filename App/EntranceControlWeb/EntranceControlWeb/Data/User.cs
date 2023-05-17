using EntranceControlWeb.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Data
{
    public partial class User
    {
        public User()
        {
            Authorizes = new HashSet<Authorize>();
        }

        public int IdUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
        public int IdStaff { get; set; }

        public virtual staff IdStaffs { get; set; }
        public virtual ICollection<Authorize> Authorizes { get; set; }
    }
}
