using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Data
{
    public partial class Room
    {
        public Room()
        {
            Doors = new HashSet<Door>();
            Entrances = new HashSet<Entrance>();
        }

        public int IdRoom { get; set; }
        public string TitleRoom { get; set; }
        public int IdLevel { get; set; }

        public virtual AccessLevel IdLevels{ get; set; }
        public virtual ICollection<Door> Doors { get; set; }
        public virtual ICollection<Entrance> Entrances { get; set; }
    }
}
