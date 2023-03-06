using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Data
{
    public partial class AccessLevel
    {
        public AccessLevel()
        {
            Rooms = new HashSet<Room>();
            Visitors = new HashSet<Visitor>();
            staff = new HashSet<staff>();
        }

        public int IdLevel { get; set; }
        public string TitleLevel { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Visitor> Visitors { get; set; }
        public virtual ICollection<staff> staff { get; set; }
    }
}
