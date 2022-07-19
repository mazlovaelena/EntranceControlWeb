using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntranceControlWeb.Models
{
    public class AccessLevelViewModel
    {
        public List<AccessLevel> Levels { get; set; }
        public int IdLevel { get; set; }
        public string TitleLevel { get; set; }

        public List<staff> Staffs { get; set; }
        public int IdStaff { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string CorpEmail { get; set; }
        public string MobPhone { get; set; }
        public string Image { get; set; }
    }
}
