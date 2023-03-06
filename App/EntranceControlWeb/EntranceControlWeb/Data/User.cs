using System;
using System.Collections.Generic;
using EntranceControlWeb.Data;
using EntranceControlWeb.Models;

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

        public virtual ICollection<Authorize> Authorizes { get; set; }
    }
}
