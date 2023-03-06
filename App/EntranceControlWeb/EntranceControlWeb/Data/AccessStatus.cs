using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Data
{
    public partial class AccessStatus
    {
        public AccessStatus()
        {
            Entrances = new HashSet<Entrance>();
        }

        public int IdStatus { get; set; }
        public string TitleStatus { get; set; }

        public virtual ICollection<Entrance> Entrances { get; set; }
    }
}
