using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EntranceControlWeb.Data;

namespace EntranceControlWeb.Models
{
    public class PassesViewModel
    {
        public List<Pass> Passes { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public int IdPass { get; set; }
        public List<LastingStatus> Lastings { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public int IdLong { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public string TitleLong { get; set; }
        public List<ActivityStatus> Activities { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public int IdActiv { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public string TitleActiv { get; set; }
    }
}
