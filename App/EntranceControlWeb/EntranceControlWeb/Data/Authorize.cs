using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class Authorize
    {
        public DateTime DateAuth { get; set; }
        public int IdUser { get; set; }

        public virtual User IdUserNavigation { get; set; }
    }
}
