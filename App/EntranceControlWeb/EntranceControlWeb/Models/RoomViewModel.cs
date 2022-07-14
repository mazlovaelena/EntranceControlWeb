using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public class RoomViewModel
    {
        public List<Room> Rooms { get; set; }
        public int IdRoom { get; set; }
        public string TitleRoom { get; set; }

        public List<AccessLevel> Levels { get; set; }
        public int IdLevel { get; set; }

    }
}
