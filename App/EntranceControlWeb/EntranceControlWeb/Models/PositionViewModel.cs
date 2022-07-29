using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EntranceControlWeb.Models
{
    public class PositionViewModel
    {
        public List<Position> Positions { get; set; }
        public int IdPost { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public string TitlePost { get; set; }
    }
}
