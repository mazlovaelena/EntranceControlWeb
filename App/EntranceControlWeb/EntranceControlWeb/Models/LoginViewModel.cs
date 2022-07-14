using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntranceControlWeb.Models
{
    public class LoginViewModel
    {
        public List<User> Users { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Введите Email-адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
