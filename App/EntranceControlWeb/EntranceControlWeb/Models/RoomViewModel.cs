using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EntranceControlWeb.Data;


#nullable disable

namespace EntranceControlWeb.Models
{
    public class RoomViewModel
    {
        public List<Room> Rooms { get; set; }
        public int IdRoom { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string TitleRoom { get; set; }

        public List<AccessLevel> Levels { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public int IdLevel { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public string TitleLevel { get; set; }

    }
}
