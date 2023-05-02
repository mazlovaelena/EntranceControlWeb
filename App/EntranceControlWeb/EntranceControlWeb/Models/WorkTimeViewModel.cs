﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntranceControlWeb.Models
{
    public class WorkTimeViewModel
    {
        public List<WorkTimeCalc> WorkTime { get; set; }
    }
    public class WorkTimeCalc
    { 
        public int ID { get; set; }
        public string TimeWork { get; set; }
        public List<TimeSpan> Time { get; set; }
        //public string TimeExt { get; set; }
        //public double TimeFact { get; set; }
        //public string TimeBeg { get; set; }
        //public string TimeEnd { get; set; }

    }
}
