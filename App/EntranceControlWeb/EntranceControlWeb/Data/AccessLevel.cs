using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Data
{
    public partial class AccessLevel
    {
        public AccessLevel()
        {
            Passes = new HashSet<Pass>();
            Rooms = new HashSet<Room>();
        }

        public int IdLevel { get; set; }
        public string TitleLevel { get; set; }

        public virtual ICollection<Pass> Passes { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
