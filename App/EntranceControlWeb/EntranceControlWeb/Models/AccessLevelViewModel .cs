using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntranceControlWeb.Data;

namespace EntranceControlWeb.Models
{
    public class AccessLevelViewModel
    {
        public List<AccessLevel> Levels { get; set; }
        public int IdLevel { get; set; }
        public string TitleLevel { get; set; }
    }
}
