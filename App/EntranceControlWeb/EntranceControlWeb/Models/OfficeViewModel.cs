using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Models
{
    public class OfficeViewModel
    {
        public List<Office> Offices { get; set; }
        public int IdOffice { get; set; }
        public string TitleOffice { get; set; }
    }
}
