using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntranceControlWeb.Data;


namespace EntranceControlWeb.Models
{
    public class AuthorizeViewModel
    {
        public List<Authorize> Authorizes { get; set; }
        public int IdItem { get; set; }
        public DateTime DateAuth { get; set; }
        public List<User> Users { get; set; }
        public int IdUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public List<staff> Staffs { get; set; }

    }
}
