using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class UserViewModel
    {
        public List<User> Users { get; set; }
        public int IdUser { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
    }
}
