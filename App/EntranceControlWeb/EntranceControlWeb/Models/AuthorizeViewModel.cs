using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntranceControlWeb.Models
{
    public class AuthorizeViewModel
    {
        public List<Authorize> Authorizes { get; set; }
        public DateTime DateAuth { get; set; }
        public List<User> Users { get; set; }
        public int IdUser { get; set; }


    }
}
