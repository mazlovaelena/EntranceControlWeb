using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class SortingByOfficeViewModel
    {
        public List<SortingByOffice> Sortings { get; set; }
        public int IdItem { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public TimeSpan TimeBegin { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public TimeSpan TimeEnd { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string WorkPhone { get; set; }

        public List<staff> Staffs { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public int IdStaff { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string CorpEmail { get; set; }
        public string MobPhone { get; set; }
        public string Image { get; set; }

        public List<Position> Positions { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public int IdPost { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public string TitlePost { get; set; }

        public List<Office> Offices { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public int IdOffice { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public string TitleOffice { get; set; }


    }
}
