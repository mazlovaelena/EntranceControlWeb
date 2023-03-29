using System;
using System.Collections.Generic;
using EntranceControlWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;


#nullable disable

namespace EntranceControlWeb.Models
{
    public class EntranceViewModel
    {
        public List<Entrance> Entrances { get; set; }
        public int IdRecord { get; set; }
        public DateTime DateEntr { get; set; }
        public DateTime DateExit { get; set; }

        public List<Pass> Passes { get; set; }
        public int IdPass { get; set; }
        public IEnumerable<SelectListItem> PassSelect { get; set; }
        

        public List<Room> Rooms { get; set; }
        public int IdRoom { get; set; }
        public string TitleRoom { get; set; }
        public IEnumerable<SelectListItem> RoomSelect { get; set; }


        public List<Door> Doors { get; set; }
        public int IdDoor { get; set; }
        public string TitleDoor { get; set; }
        public IEnumerable<SelectListItem> DoorSelect { get; set; }

        public List<AccessStatus> Statuses { get; set; }
        public int IdStatus { get; set; }
        public string TitleStatus { get; set; }
        public IEnumerable<SelectListItem> StatSelect { get; set; }

    }
}
