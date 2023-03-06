using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EntranceControlWeb.Data;


namespace EntranceControlWeb.Models
{
    public class VisitorViewModel
    {
        public List<Visitor> Visitors { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public int Idvisitor { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string Fio { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string MobilePhone { get; set; }
        public List<AccessLevel> Levels { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public int IdLevel { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string TitleLevel { get; set; }
        public List<Pass> Passes { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public int IdPass { get; set; }

    }
}
