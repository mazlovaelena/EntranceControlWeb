using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EntranceControlWeb.Data;


#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class StaffViewModel
    {
        public List<staff> Staffs { get; set; }
        public int IdStaff { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string CorpEmail { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string MobPhone { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string Image { get; set; }
        
        public int IdPass { get; set; }

    }
}
