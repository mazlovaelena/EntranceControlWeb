using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class AccessLevel
    {
        public AccessLevel()
        {
            Rooms = new HashSet<Room>();
            staff = new HashSet<staff>();
        }

        public int IdLevel { get; set; }
        public string TitleLevel { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<staff> staff { get; set; }
    }
}
