using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Data
{
    public partial class Pass
    {
        public Pass()
        {
            Entrances = new HashSet<Entrance>();
            Visitors = new HashSet<Visitor>();
            staff = new HashSet<staff>();
        }

        public int IdPass { get; set; }
        public int IdLong { get; set; }
        public int IdActiv { get; set; }
        public int IdLevel { get; set; }
        public bool Hidden { get; set; }

        public virtual ActivityStatus IdActivs { get; set; }
        public virtual AccessLevel IdLevels { get; set; }
        public virtual LastingStatus IdLongs { get; set; }
        public virtual ICollection<Entrance> Entrances { get; set; }
        public virtual ICollection<Visitor> Visitors { get; set; }
        public virtual ICollection<staff> staff { get; set; }
    }
}
