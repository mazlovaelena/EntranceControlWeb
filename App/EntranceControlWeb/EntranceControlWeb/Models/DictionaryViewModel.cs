using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntranceControlWeb.Data;

namespace EntranceControlWeb.Models
{
    //Модель представления таблиц "Статусы доступа", "Статусы Активности", "Статусы длительности", "Уровни доступа"
    public class DictionaryViewModel
    {
        public List<AccessLevel> Levels { get; set; }
        public int IdLevel { get; set; }
        public string TitleLevel { get; set; }

        public List<AccessStatus> Statuses { get; set; }
        public int IdStatus { get; set; }
        public string TitleStatus { get; set; }

        public List<ActivityStatus> Activities { get; set; }
        public int IdActiv { get; set; }
        public string TitleActiv { get; set; }

        public List<LastingStatus> Lastings { get; set; }
        public int IdLong { get; set; }
        public string TitleLong { get; set; }
    }
}
