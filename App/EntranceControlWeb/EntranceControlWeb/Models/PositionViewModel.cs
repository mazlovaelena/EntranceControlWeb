using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public class PositionViewModel
    {
        public List<Position> Positions { get; set; }
        public int IdPost { get; set; }
        public string TitlePost { get; set; }
    }
}
