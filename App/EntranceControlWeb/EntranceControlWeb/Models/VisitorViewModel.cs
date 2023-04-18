﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EntranceControlWeb.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EntranceControlWeb.Models
{
    public class VisitorViewModel
    {
        public List<Visitor> Visitors { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public int Idvisitor { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public string MobilePhone { get; set; }       
        public List<Pass> Passes { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        public int IdPass { get; set; }
        public List<LastingStatus> Lastings { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public int IdLong { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public string TitleLong { get; set; }
        public IEnumerable<SelectListItem> LongSelect { get; set; }
        public List<ActivityStatus> Activities { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public int IdActiv { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        public string TitleActiv { get; set; }
        public IEnumerable<SelectListItem> ActivSelect { get; set; }
        public List<AccessLevel> Levels { get; set; }

        [Required(ErrorMessage = "Выберите значение")]
        public int IdLevel { get; set; }

        [Required(ErrorMessage = "Выберите значение")]
        public string TitleLevel { get; set; }
        public IEnumerable<SelectListItem> LevelSelect { get; set; }
    }
}
