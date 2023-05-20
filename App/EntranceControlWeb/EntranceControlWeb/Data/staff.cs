﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Data
{
    public partial class staff
    {
        public staff()
        {
            SortingByOffices = new HashSet<SortingByOffice>();
            Users = new HashSet<User>();
        }

        public int IdStaff { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string CorpEmail { get; set; }
        public string MobPhone { get; set; }
        public string Image { get; set; }
        public int IdPass { get; set; }
        public bool Hidden { get; set; }

        public virtual Pass IdPasses { get; set; }
        public virtual ICollection<SortingByOffice> SortingByOffices { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
