using EntranceControlWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntranceControlWeb.Models
{
    public class WorkTimeViewModel
    {
        public List<WorkTimeCalc> WorkTime { get; set; }
        public List<ResultTime> TimeCalc { get; set; }
        public List<PlanTime> PlanTimes { get; set; }
        public List<staff> Staffs { get; set; }
        public List<(ResultTime, PlanTime, PercentTime, staff)> ResultList { get; set; }
    }
    public class WorkTimeCalc
    { 
        public int ID { get; set; }        
        public List<TimeSpan> Time { get; set; }
        

    }
    public class ResultTime
    {
        public int ID { get; set; }
        public List<double> TimeW { get; set; }       
    }
    public class PlanTime
    {
        public int IDStaff { get; set; }
        public List<double> TimeP { get; set; }        
    }

    public class PercentTime
    {
        public int IDStaff { get; set; }
        public List<double> TimePercent { get; set; }

    }

}
