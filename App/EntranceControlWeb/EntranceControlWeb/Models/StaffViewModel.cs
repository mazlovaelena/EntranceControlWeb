using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EntranceControlWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;


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

        public List<Pass> Passes { get; set; }
        public int IdPass { get; set; }

        public List<LastingStatus> Lastings { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public int IdLong { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public string TitleLong { get; set; }
        public IEnumerable<SelectListItem> LongSelect { get; set; }
        public List<ActivityStatus> Activities { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public int IdActiv { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public string TitleActiv { get; set; }
        public IEnumerable<SelectListItem> ActivSelect { get; set; }
        public List<AccessLevel> Levels { get; set; }

        [Required(ErrorMessage = "Выберите значение")]
        public int IdLevel { get; set; }

        [Required(ErrorMessage = "Выберите значение")]
        public string TitleLevel { get; set; }
        public IEnumerable<SelectListItem> LevelSelect { get; set; }

    }
}
