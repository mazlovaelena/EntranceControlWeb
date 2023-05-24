using EntranceControlWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntranceControlWeb.Models
{
    //Модель добавления нового пользователя системы
    public class NewUserViewModel
    {
        public List<User> Users { get; set; }
        public int IdUser { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordRetry { get; set; }
        public UserRole UserRole { get; set; }
        public List<staff> Staffs { get; set; }
        public IEnumerable<SelectListItem> StaffSelect { get; set; }
        public int IdStaff { get; set; }
    }
}
