using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class UserViewModel
    {
        public List<User> Users { get; set; }
        public int IdUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
    }
}
