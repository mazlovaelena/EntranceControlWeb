using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Data
{
    public partial class staff
    {
        public staff()
        {
            SortingByOffices = new HashSet<SortingByOffice>();
        }

        public int IdStaff { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string CorpEmail { get; set; }
        public string MobPhone { get; set; }
        public string Image { get; set; }
        public int IdLevel { get; set; }
        public int IdPass { get; set; }

        public virtual AccessLevel IdLevels { get; set; }
        public virtual ICollection<SortingByOffice> SortingByOffices { get; set; }
    }
}
