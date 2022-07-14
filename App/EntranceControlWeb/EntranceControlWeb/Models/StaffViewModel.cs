using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class StaffViewModel
    {
        public List<staff> Staffs { get; set; }
        public int IdStaff { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string CorpEmail { get; set; }
        public string MobPhone { get; set; }
        public string Image { get; set; }

        public List<AccessLevel> Levels { get; set; }
        public int IdLevel { get; set; }
        public string TitleLevel { get; set; }
       
       
    }
}
