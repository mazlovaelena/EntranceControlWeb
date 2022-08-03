using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public class EntranceViewModel
    {
        public List<Entrance> Entrances { get; set; }
        public int IdRecord { get; set; }
        public DateTime DateEntr { get; set; }
        public DateTime DateExit { get; set; }

        public List<staff> Staffs { get; set; }
        public int IdStaff { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string CorpEmail { get; set; }
        public string MobPhone { get; set; }
        public string Image { get; set; }

        public List<Position> Positions { get; set; }
        public int IdPost { get; set; }
        public string TitlePost { get; set; }

        public List<Office> Offices { get; set; }
        public int IdOffice { get; set; }
        public string TitleOffice { get; set; }


        public List<AccessLevel> Levels { get; set; }
        public int IdLevel { get; set; }
        public string TitleLevel { get; set; }

        public List<Room> Rooms { get; set; }
        public int IdRoom { get; set; }
        public string TitleRoom { get; set; }

        public List<Door> Doors { get; set; }
        public int IdDoor { get; set; }
        public string TitleDoor { get; set; }

        public List<AccessStatus> Statuses { get; set; }
        public int IdStatus { get; set; }
        public string TitleStatus { get; set; }

        public int [] Months { get; set; }

    }
}
