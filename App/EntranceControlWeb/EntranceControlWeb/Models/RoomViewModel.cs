using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EntranceControlWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;


#nullable disable

namespace EntranceControlWeb.Models
{
    //Модель представления таблицы "Помещения"
    public class RoomViewModel
    {
        public List<Room> Rooms { get; set; }
        public int IdRoom { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string TitleRoom { get; set; }

        public List<AccessLevel> Levels { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public int IdLevel { get; set; }
        public IEnumerable<SelectListItem> LevelSelect { get; set; }


    }
}
