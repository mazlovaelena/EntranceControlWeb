using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class SortingByOfficeViewModel
    {
        public List<SortingByOffice> Sortings { get; set; }
        public int IdItem { get; set; }
        public TimeSpan TimeBegin { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public string WorkPhone { get; set; }

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


    }
}
