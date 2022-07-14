using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class SortingByOffice
    {
        public TimeSpan TimeBegin { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public string WorkPhone { get; set; }
        public int IdStaff { get; set; }
        public int IdPost { get; set; }
        public int IdOffice { get; set; }

        public virtual Office IdOfficeNavigation { get; set; }
        public virtual Position IdPostNavigation { get; set; }
        public virtual staff IdStaffNavigation { get; set; }
    }
}
