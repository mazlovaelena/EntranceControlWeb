using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class AccessLevel
    {
        public AccessLevel()
        {
            Visitors = new HashSet<Visitor>();
            staffs = new HashSet<staff>();
        }

        public int IdLevel { get; set; }
        public string TitleLevel { get; set; }

        public virtual Room Rooms { get; set; }
        public virtual ICollection<Visitor> Visitors { get; set; }
        public virtual ICollection<staff> staffs { get; set; }
    }
}
