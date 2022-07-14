using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class Entrance
    {
        public int IdRecord { get; set; }
        public DateTime DateEntr { get; set; }
        public DateTime DateExit { get; set; }
        public int IdStaff { get; set; }
        public int IdRoom { get; set; }
        public int IdDoor { get; set; }
        public int IdStatus { get; set; }

        public virtual Door IdDoorNavigation { get; set; }
        public virtual Room IdRoomNavigation { get; set; }
        public virtual staff IdStaffNavigation { get; set; }
        public virtual AccessStatus IdStatusNavigation { get; set; }
    }
}
