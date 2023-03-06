using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Data
{
    public partial class Visitor
    {
        public int Idvisitor { get; set; }
        public string Fio { get; set; }
        public string MobilePhone { get; set; }
        public int IdLevel { get; set; }
        public int IdPass { get; set; }

        public virtual AccessLevel IdLevels { get; set; }
        public virtual Pass IdPasses { get; set; }
    }
}
