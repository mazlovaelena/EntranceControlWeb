using System;
using System.Collections.Generic;

#nullable disable

namespace EntranceControlWeb.Data
{
    public partial class Visitor
    {
        public int Idvisitor { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public int IdPass { get; set; }

        public virtual Pass IdPasses { get; set; }
    }
}
