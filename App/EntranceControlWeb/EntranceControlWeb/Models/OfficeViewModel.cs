using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EntranceControlWeb.Data;


#nullable disable

namespace EntranceControlWeb.Models
{
    //Модель представления таблицы "Отделы"
    public class OfficeViewModel
    {
        public List<Office> Offices { get; set; }
        public int IdOffice { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public string TitleOffice { get; set; }
    }
}
