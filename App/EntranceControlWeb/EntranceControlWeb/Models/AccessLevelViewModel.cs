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
    }
}
