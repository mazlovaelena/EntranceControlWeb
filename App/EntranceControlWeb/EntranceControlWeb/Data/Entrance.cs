using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Data
{
    public partial class Entrance
    {
        public int IdRecord { get; set; }
        public DateTime DateEntr { get; set; }
        public DateTime DateExit { get; set; }
        public int IdPass { get; set; }
        public int IdRoom { get; set; }
        public int IdDoor { get; set; }
        public int IdStatus { get; set; }

        public virtual Door IdDoors { get; set; }
        public virtual Pass IdPasses { get; set; }
        public virtual Room IdRooms { get; set; }
        public virtual AccessStatus IdStatuses { get; set; }
    }
}
