using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Data
{
    public partial class SortingByOffice
    {
        public int IdItem { get; set; }
        public TimeSpan TimeBegin { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public string WorkPhone { get; set; }
        public int IdStaff { get; set; }
        public int IdPost { get; set; }
        public int IdOffice { get; set; }
        public bool Hidden { get; set; }

        public virtual Office IdOffices { get; set; }
        public virtual Position IdPosts { get; set; }
        public virtual staff IdStaffs { get; set; }
    }
}
