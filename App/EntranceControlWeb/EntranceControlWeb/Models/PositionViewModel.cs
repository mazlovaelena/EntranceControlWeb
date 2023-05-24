using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EntranceControlWeb.Data;


#nullable disable

namespace EntranceControlWeb.Models
{
    //Модель представления таблицы "Должности"
    public class PositionViewModel
    {
        public List<Position> Positions { get; set; }
        public int IdPost { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public string TitlePost { get; set; }
    }
}
