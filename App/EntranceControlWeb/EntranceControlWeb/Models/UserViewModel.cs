using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EntranceControlWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;


#nullable disable

namespace EntranceControlWeb.Models
{
    public partial class UserViewModel
    {

        public List<User> Users { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public string Password { get; set; }       
       
    }
}
