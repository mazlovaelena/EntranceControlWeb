﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Data
{
    public partial class LastingStatus
    {
        public LastingStatus()
        {
            Passes = new HashSet<Pass>();
        }

        public int IdLong { get; set; }
        public string TitleLong { get; set; }
        public bool Hidden { get; set; }

        public virtual ICollection<Pass> Passes { get; set; }
    }
}
