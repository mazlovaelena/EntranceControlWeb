using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EntranceControlWeb.Data;


namespace EntranceControlWeb.Models
{
    //Модель представления таблицы "Турникеты"
    public class DoorViewModel
    {
        public List<Door> Doors { get; set; }
        public int IdDoor { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string TitleDoor { get; set; }

        public List<Room> Rooms { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public int IdRoom { get; set; }
        [Required(ErrorMessage = "Выберите значение")]
        public string TitleRoom { get; set; }
    }
}
