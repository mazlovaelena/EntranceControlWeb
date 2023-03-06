using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Data
{
    public partial class ActivityStatus
    {
        public ActivityStatus()
        {
            Passes = new HashSet<Pass>();
        }

        public int IdActiv { get; set; }
        public string TitleActiv { get; set; }

        public virtual ICollection<Pass> Passes { get; set; }
    }
}
