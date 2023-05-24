using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EntranceControlWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;


#nullable disable

namespace EntranceControlWeb.Models
{
    //Модель представления таблицы "Распределение по отделам"
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
        public IEnumerable<SelectListItem> StaffSelect { get; set; }
        public List<Position> Positions { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public int IdPost { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public IEnumerable<SelectListItem> PostSelect { get; set; }

        public List<Office> Offices { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public int IdOffice { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public IEnumerable<SelectListItem> OfficeSelect { get; set; }

    }
}
