using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntranceControlWeb.Data;

namespace EntranceControlWeb.Models
{
    public class AccessStatusViewModel
    {
        public List<AccessStatus> Statuses { get; set; }
        public int IdStatus { get; set; }
        public string TitleStatus { get; set; }
    }
}
