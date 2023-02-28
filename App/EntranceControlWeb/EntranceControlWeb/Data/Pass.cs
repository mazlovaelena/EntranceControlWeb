using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class Pass
    {
        public Pass()
        {
            Entrances = new HashSet<Entrance>();
            Visitors = new HashSet<Visitor>();
            staffs = new HashSet<staff>();
        }

        public int IdPass { get; set; }
        public int IdLong { get; set; }
        public int IdActiv { get; set; }

        public virtual ActivityStatus IdActivs { get; set; }
        public virtual LastingStatus IdLongs { get; set; }
        public virtual ICollection<Entrance> Entrances { get; set; }
        public virtual ICollection<Visitor> Visitors { get; set; }
        public virtual ICollection<staff> staffs { get; set; }
    }
}
