using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Data
{
    public partial class Position
    {
        public Position()
        {
            SortingByOffices = new HashSet<SortingByOffice>();
        }

        public int IdPost { get; set; }
        public string TitlePost { get; set; }
        public bool Hidden { get; set; }

        public virtual ICollection<SortingByOffice> SortingByOffices { get; set; }
    }
}
