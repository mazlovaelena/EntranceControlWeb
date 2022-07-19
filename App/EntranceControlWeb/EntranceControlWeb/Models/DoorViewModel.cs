using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntranceControlWeb.Models
{
    public class DoorViewModel
    {
        public List<Door> Doors { get; set; }
        public int IdDoor { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public string TitleDoor { get; set; }

        public List<Room> Rooms { get; set; }
        public int IdRoom { get; set; }
        public string TitleRoom { get; set; }
    }
}
