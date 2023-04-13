using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public class ChartViewModel
    {
        public List<ChartItemViewModel> Chart { get; set; }  
        public List<ChartItemViewModel> Name { get; set; }
        public IEnumerable<SelectListItem> StaffSelect { get; set; }

        public List<Data.Entrance> Entrances { get; set; }

        public int IdPass { get; set; }
    }
    public class ChartItemViewModel
    {
        public int ID { get; set; }       
        public int Count { get; set; }

        public int Pass { get; set; }
        public int Sum { get; set; }
        public int Day { get; set; }
        

    }
}
