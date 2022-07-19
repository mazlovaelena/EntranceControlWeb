using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class Office
    {
        public Office()
        {
            SortingByOffices = new HashSet<SortingByOffice>();
        }

        public int IdOffice { get; set; }
        public string TitleOffice { get; set; }

        public virtual ICollection<SortingByOffice> SortingByOffices { get; set; }
    }
}
