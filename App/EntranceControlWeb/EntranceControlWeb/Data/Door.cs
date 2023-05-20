using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Data
{
    public partial class Door
    {
        public Door()
        {
            Entrances = new HashSet<Entrance>();
        }

        public int IdDoor { get; set; }
        public string TitleDoor { get; set; }
        public int IdRoom { get; set; }
        public bool Hidden { get; set; }

        public virtual Room IdRooms { get; set; }
        public virtual ICollection<Entrance> Entrances { get; set; }
    }
}
