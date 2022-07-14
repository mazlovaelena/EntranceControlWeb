using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class Authorize
    {
        public DateTime DateAuth { get; set; }
        [Key]
        public int IdUser { get; set; }

        public virtual User IdUserNavigation { get; set; }
    }
}
