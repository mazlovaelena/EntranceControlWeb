using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class Authorize
    {
        public int IdItem { get; set; }
        public DateTime DateAuth { get; set; }        
        public int IdUser { get; set; }      
        public virtual User IdUsers { get; set; }
    }
}
