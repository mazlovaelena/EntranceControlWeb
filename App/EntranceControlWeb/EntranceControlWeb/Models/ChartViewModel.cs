using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public class ChartViewModel
    {
        public List<ChartItemViewModel> Chart { get; set; }             
    }
    public class ChartItemViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
