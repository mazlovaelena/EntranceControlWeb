using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using EntranceControlWeb.Models;
using EntranceControlWeb.Data;
using Microsoft.AspNetCore.Authorization;
using OfficeOpenXml;
using System.IO;
using LicenseContext = OfficeOpenXml.LicenseContext;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EntranceControlWeb.Controllers
{
    public class CabinetController : Controller
    {
        private EntranceControlWebContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public CabinetController(EntranceControlWebContext context, IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            _context = context;
        }

        [Authorize]
        public IActionResult UserCab()
        {
            return View();
        }
        [Authorize]
        public IActionResult AdminCab(EntranceViewModel entr)
        {
            entr.Entrances = _context.Entrances.ToList();
            entr.Passes = _context.Passes.ToList();
            entr.Rooms = _context.Rooms.ToList();
            entr.Doors = _context.Doors.ToList();
            entr.Statuses = _context.AccessStatuses.ToList();
            return View(entr);
        }
        [Authorize]
        public IActionResult SysAdminCab(EntranceViewModel entr)
        {
            entr.Entrances = _context.Entrances.ToList();
            entr.Passes = _context.Passes.ToList();
            entr.Rooms = _context.Rooms.ToList();
            entr.Doors = _context.Doors.ToList();
            entr.Statuses = _context.AccessStatuses.ToList();
            return View(entr);
        }
        #region ПРОСМОТР ПОЛЬЗОВАТЕЛЕЙ САЙТА
        [Authorize]       
        public IActionResult Users(AuthorizeViewModel auth, string Search)
        {
            var find = from s in _context.Authorizes select s;

            if (!String.IsNullOrEmpty(Search))
            {
                find = find.Where(s => s.IdUsers.Email.ToUpper().Contains(Search.ToUpper()));

                auth.Authorizes = _context.Authorizes.ToList();
                auth.Users = _context.Users.ToList();

                if (User.IsInRole(UserRole.Admin.ToString()))
                {
                    auth.Authorizes = find
                         .Where(x => x.IdUsers.UserRole == UserRole.User).ToList();
                }
                else
                {
                    auth.Authorizes = find.ToList();
                }                
               
                return View(auth);
            }
            //else if (!String.IsNullOrEmpty(Search) && find != null)
            //{
            //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //    FileStream fs = new("UserReport.xls", FileMode.Create);

            //    using (ExcelPackage Ep = new(fs))
            //    {
            //        var Sheet1 = Ep.Workbook.Worksheets.Add("Посетители сайта");
            //        Sheet1.Cells["A1"].Value = "ID_Запись";
            //        Sheet1.Cells["B1"].Value = "ДатаАвторизации";
            //        Sheet1.Cells["C1"].Value = "Пользователь";

            //        var row1 = 2;
            //        foreach (var entrance in find)
            //        {
            //            _context.Users.ToList();
            //            Sheet1.Cells[string.Format("A{0}", row1)].Value = entrance.IdItem;
            //            Sheet1.Cells[string.Format("B{0}", row1)].Value = entrance.DateAuth.ToString();
            //            Sheet1.Cells[string.Format("C{0}", row1)].Value = entrance.IdUsers.Email;
            //            row1++;
            //        }
            //        Sheet1.Cells["A:AZ"].AutoFitColumns();

            //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //        var xlFile = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UserReport.xls"));

            //        Ep.Save();
            //        fs.Close();

            //    }
            //    string file_path = Path.Combine(_appEnvironment.ContentRootPath, "UserReport.xls");
            //    //Тип файла -content - type
            //    string file_type = "application/octet-stream";
            //    //Имя файла -необязательно
            //    string file_name = "UserReport.xls";
            //    return PhysicalFile(file_path, file_type, file_name);
            //}

            auth.Authorizes = _context.Authorizes.ToList();
            auth.Users = _context.Users.ToList();

            if (User.IsInRole(UserRole.Admin.ToString()))
            {
                auth.Authorizes = _context.Authorizes
                     .Where(x => x.IdUsers.UserRole == UserRole.User).ToList();
            }
            else
            {
                auth.Authorizes = _context.Authorizes.ToList();
            }

            return View(auth);
        }

        [Authorize]
        [HttpPost]
        public IActionResult UserReport()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(UserRole.Admin.ToString()))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    FileStream fs = new("UserReport.xls", FileMode.Create);

                    using (ExcelPackage Ep = new(fs))
                    {
                        var Sheet1 = Ep.Workbook.Worksheets.Add("Посетители сайта");
                        Sheet1.Cells["A1"].Value = "ID_Запись";
                        Sheet1.Cells["B1"].Value = "ДатаАвторизации";
                        Sheet1.Cells["C1"].Value = "Пользователь";

                        var row1 = 2;
                        foreach (var entrance in _context.Authorizes
                        .Where(x => x.IdUsers.UserRole == UserRole.User).ToList())
                        {
                            _context.Users.ToList();
                            Sheet1.Cells[string.Format("A{0}", row1)].Value = entrance.IdItem;
                            Sheet1.Cells[string.Format("B{0}", row1)].Value = entrance.DateAuth.ToString();
                            Sheet1.Cells[string.Format("C{0}", row1)].Value = entrance.IdUsers.Email;
                            row1++;
                        }
                        Sheet1.Cells["A:AZ"].AutoFitColumns();

                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        var xlFile = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UserReport.xls"));

                        Ep.Save();
                        fs.Close();

                    }

                }
                else
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    FileStream fs = new("UserReport.xls", FileMode.Create);

                    using (ExcelPackage Ep = new(fs))
                    {
                        var Sheet1 = Ep.Workbook.Worksheets.Add("Посетители сайта");
                        Sheet1.Cells["A1"].Value = "ID_Запись";
                        Sheet1.Cells["B1"].Value = "ДатаАвторизации";
                        Sheet1.Cells["C1"].Value = "Пользователь";

                        var row1 = 2;
                        foreach (var entrance in _context.Authorizes.ToList())
                        {
                            _context.Users.ToList();
                            Sheet1.Cells[string.Format("A{0}", row1)].Value = entrance.IdItem;
                            Sheet1.Cells[string.Format("B{0}", row1)].Value = entrance.DateAuth.ToString();
                            Sheet1.Cells[string.Format("C{0}", row1)].Value = entrance.IdUsers.Email;
                            row1++;
                        }
                        Sheet1.Cells["A:AZ"].AutoFitColumns();

                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        var xlFile = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UserReport.xls"));

                        Ep.Save();
                        fs.Close();

                    }

                }
            }
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "UserReport.xls");
            //Тип файла -content - type
            string file_type = "application/octet-stream";
            //Имя файла -необязательно
            string file_name = "UserReport.xls";
            return PhysicalFile(file_path, file_type, file_name);

        }
        #endregion

        #region ПРОСМОТР ГРАФИКА
        [Authorize]
        public IActionResult Chart(ChartViewModel model, int IdPass)
        {
            model.StaffSelect = new SelectList(_context.staff, "IdPass", "Surname");

            var find = from s in _context.Entrances select s;

            if(IdPass != 0)
            {
                find = find.Where(s => s.IdPass.ToString().Contains(IdPass.ToString().ToUpper()));

                model.Entrances = find.ToList();

                var chart = find
                    .GroupBy(x => x.DateEntr.Day)
                    .Select(t => new ChartItemViewModel { ID = t.Key, Count = t.Count() })
                    .ToList();

                var data = _context.Entrances
                   .Where(s => s.IdPasses.IdLong == 2)
                   .GroupBy(p => p.IdPass)
                   .Select(g => new ChartItemViewModel { Pass = g.Key, Sum = g.Count() })
                   .ToList();

                model.Chart = chart;
                model.Name = data;

                return View(model);                    
            }

            var number = _context.Entrances
            .GroupBy(p => p.DateEntr.Day)
            .Select(g => new ChartItemViewModel { ID = g.Key, Count = g.Count() })
            .ToList();

            var name = _context.Entrances
                .Where(s=>s.IdPasses.IdLong == 2)
                .GroupBy(p => p.IdPass)                
                .Select(g => new ChartItemViewModel { Pass = g.Key, Sum = g.Count() })                
                .ToList();

            model.Chart = number;
            model.Name = name;

            return View(model);
        }
        #endregion

        #region УЧЕТ РАБОЧЕГО ВРЕМЕНИ
        public IActionResult WorkTime(Entrance vm, SortingByOffice sm)
        {
            //var vm = new EntranceViewModel();
            //var sm = new SortingByOfficeViewModel();
            //var timefact = Convert.ToDouble(vm.DateExit.TimeOfDay) - Convert.ToDouble(vm.DateEntr.TimeOfDay);
                        
            //sm.Sortings = _context.SortingByOffices.ToList();
            var wm = new WorkTimeCalc();

            var calc = _context.Entrances
                .Where(s=>s.IdPasses.IdLong == 2)
                .GroupBy(x => x.IdPass)
                .Select(y => new WorkTimeCalc
                {
                    ID = y.Key,
                    TimeEntr = vm.DateEntr.TimeOfDay.ToString(),
                    TimeExt = vm.DateExit.TimeOfDay.ToString(),
                    //TimeBeg = (string)sm.TimeBegin.ToString().Where(r => sm.IdStaff == wm.ID),
                    //TimeEnd = (string)sm.TimeEnd.ToString().Where(r => sm.IdStaff == wm.ID)

                })
                .ToList();

            var mod = new WorkTimeViewModel
            {
                WorkTime = calc
            };

            return View(mod);
        }
        #endregion

        #region РАБОТА С ОТЧЕТНЫМ ФАЙЛОМ
        public IActionResult EntranceReport()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult EntranceReport(EntranceViewModel model)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileStream fs = new FileStream("EntranceReport.xls", FileMode.Create);
            using (ExcelPackage Ep = new ExcelPackage(fs))
            {
                var Sheet1 = Ep.Workbook.Worksheets.Add("Доступ");
                Sheet1.Cells["A1"].Value = "ID_Запись";
                Sheet1.Cells["B1"].Value = "Дата_Входа";
                Sheet1.Cells["C1"].Value = "Дата_Выхода";
                Sheet1.Cells["D1"].Value = "Помещение";
                Sheet1.Cells["E1"].Value = "№ пропуска";
                Sheet1.Cells["F1"].Value = "ТипТурникета";
                Sheet1.Cells["G1"].Value = "СтатусВхода";

                var row1 = 2;
                foreach (var entrance in _context.Entrances.ToList())
                {
                    _context.staff.ToList();
                    _context.Rooms.ToList();
                    _context.Doors.ToList();
                    _context.AccessStatuses.ToList();
                    Sheet1.Cells[string.Format("A{0}", row1)].Value = entrance.IdRecord;
                    Sheet1.Cells[string.Format("B{0}", row1)].Value = entrance.DateEntr.ToString();
                    Sheet1.Cells[string.Format("C{0}", row1)].Value = entrance.DateExit.ToString();
                    Sheet1.Cells[string.Format("D{0}", row1)].Value = entrance.IdRooms.TitleRoom;
                    Sheet1.Cells[string.Format("E{0}", row1)].Value = entrance.IdPass;
                    Sheet1.Cells[string.Format("F{0}", row1)].Value = entrance.IdDoors.TitleDoor;
                    Sheet1.Cells[string.Format("G{0}", row1)].Value = entrance.IdStatuses.TitleStatus;
                    row1++;
                }
                Sheet1.Cells["A:AZ"].AutoFitColumns();

                var Sheet2 = Ep.Workbook.Worksheets.Add("Сотрудники");
                Sheet2.Cells["A1"].Value = "ID_Сотрудника";
                Sheet2.Cells["B1"].Value = "ФамилияСотрудника";
                Sheet2.Cells["C1"].Value = "ИмяСотрудника";
                Sheet2.Cells["D1"].Value = "ОтчетсвоСотрудника";
                Sheet2.Cells["E1"].Value = "ДатаРождения";
                Sheet2.Cells["F1"].Value = "КорпоративнаяПочта";
                Sheet2.Cells["G1"].Value = "ТелефонМобильный";
                Sheet2.Cells["H1"].Value = "Изображение";
                Sheet2.Cells["I1"].Value = "№ пропуска";

                var row2 = 2;
                foreach (var entrance in _context.staff.ToList())
                {
                    _context.AccessLevels.ToList();
                    Sheet2.Cells[string.Format("A{0}", row2)].Value = entrance.IdStaff;
                    Sheet2.Cells[string.Format("B{0}", row2)].Value = entrance.Surname;
                    Sheet2.Cells[string.Format("C{0}", row2)].Value = entrance.Name;
                    Sheet2.Cells[string.Format("D{0}", row2)].Value = entrance.LastName;
                    Sheet2.Cells[string.Format("E{0}", row2)].Value = entrance.Birthday.ToString();
                    Sheet2.Cells[string.Format("F{0}", row2)].Value = entrance.CorpEmail;
                    Sheet2.Cells[string.Format("G{0}", row2)].Value = entrance.MobPhone;
                    Sheet2.Cells[string.Format("H{0}", row2)].Value = entrance.Image;
                    Sheet2.Cells[string.Format("I{0}", row2)].Value = entrance.IdPass;

                    row2++;
                }
                Sheet2.Cells["A:AZ"].AutoFitColumns();

                var Sheet3 = Ep.Workbook.Worksheets.Add("Должности");
                Sheet3.Cells["A1"].Value = "ID_Должности";
                Sheet3.Cells["B1"].Value = "НаименованиеДолжности";

                var row3 = 2;
                foreach (var entrance in _context.Positions.ToList())
                {
                    Sheet3.Cells[string.Format("A{0}", row3)].Value = entrance.IdPost;
                    Sheet3.Cells[string.Format("B{0}", row3)].Value = entrance.TitlePost;
                    row3++;
                }
                Sheet3.Cells["A:AZ"].AutoFitColumns();

                var Sheet4 = Ep.Workbook.Worksheets.Add("Отделы");
                Sheet4.Cells["A1"].Value = "ID_Отдела";
                Sheet4.Cells["B1"].Value = "НаименованиеОтдела";

                var row4 = 2;
                foreach (var entrance in _context.Offices.ToList())
                {
                    Sheet4.Cells[string.Format("A{0}", row4)].Value = entrance.IdOffice;
                    Sheet4.Cells[string.Format("B{0}", row4)].Value = entrance.TitleOffice;
                    row4++;
                }
                Sheet4.Cells["A:AZ"].AutoFitColumns();

                var Sheet5 = Ep.Workbook.Worksheets.Add("РаспределениеПоОтделам");
                Sheet5.Cells["A1"].Value = "ID_Запись";
                Sheet5.Cells["B1"].Value = "Сотрудник";
                Sheet5.Cells["C1"].Value = "Отдел";
                Sheet5.Cells["D1"].Value = "Должности";
                Sheet5.Cells["E1"].Value = "ВремяНачалаРаботы";
                Sheet5.Cells["F1"].Value = "ВремяЗавершенияРаботы";
                Sheet5.Cells["G1"].Value = "ТелефонРабочий";

                var row5 = 2;
                foreach (var entrance in _context.SortingByOffices.ToList())
                {
                    _context.staff.ToList();
                    _context.Offices.ToList();
                    _context.Positions.ToList();
                    Sheet5.Cells[string.Format("A{0}", row5)].Value = entrance.IdItem;
                    Sheet5.Cells[string.Format("B{0}", row5)].Value = entrance.IdStaffs.Surname;
                    Sheet5.Cells[string.Format("C{0}", row5)].Value = entrance.IdOffices.TitleOffice;
                    Sheet5.Cells[string.Format("D{0}", row5)].Value = entrance.IdPosts.TitlePost;
                    Sheet5.Cells[string.Format("E{0}", row5)].Value = entrance.TimeBegin.ToString();
                    Sheet5.Cells[string.Format("F{0}", row5)].Value = entrance.TimeEnd.ToString();
                    Sheet5.Cells[string.Format("G{0}", row5)].Value = entrance.WorkPhone;
                    row5++;
                }
                Sheet5.Cells["A:AZ"].AutoFitColumns();

                var Sheet6 = Ep.Workbook.Worksheets.Add("Помещения");
                Sheet6.Cells["A1"].Value = "ID_Помещения";
                Sheet6.Cells["B1"].Value = "НаименованиеПомещения";
                Sheet6.Cells["C1"].Value = "УровеньДоступа";

                var row6 = 2;
                foreach (var entrance in _context.Rooms.ToList())
                {
                    _context.AccessLevels.ToList();
                    Sheet6.Cells[string.Format("A{0}", row6)].Value = entrance.IdRoom;
                    Sheet6.Cells[string.Format("B{0}", row6)].Value = entrance.TitleRoom;
                    Sheet6.Cells[string.Format("C{0}", row6)].Value = entrance.IdLevels.TitleLevel;
                    row6++;
                }
                Sheet6.Cells["A:AZ"].AutoFitColumns();

                var Sheet7 = Ep.Workbook.Worksheets.Add("ТипыТурникетов");
                Sheet7.Cells["A1"].Value = "ID_ТипТурникета";
                Sheet7.Cells["B1"].Value = "НаименованиеТипаТурникета";
                Sheet7.Cells["C1"].Value = "Помещение";

                var row7 = 2;
                foreach (var entrance in _context.Doors.ToList())
                {
                    _context.Rooms.ToList();
                    Sheet7.Cells[string.Format("A{0}", row7)].Value = entrance.IdDoor;
                    Sheet7.Cells[string.Format("B{0}", row7)].Value = entrance.TitleDoor;
                    Sheet7.Cells[string.Format("C{0}", row7)].Value = entrance.IdRooms.TitleRoom;
                    row7++;
                }
                Sheet7.Cells["A:AZ"].AutoFitColumns();

                var Sheet8 = Ep.Workbook.Worksheets.Add("СтатусДоступа");
                Sheet8.Cells["A1"].Value = "ID_Статус";
                Sheet8.Cells["B1"].Value = "НаименованиеСтатус";

                var row8 = 2;
                foreach (var entrance in _context.AccessStatuses.ToList())
                {
                    Sheet8.Cells[string.Format("A{0}", row8)].Value = entrance.IdStatus;
                    Sheet8.Cells[string.Format("B{0}", row8)].Value = entrance.TitleStatus;
                    row8++;
                }
                Sheet8.Cells["A:AZ"].AutoFitColumns();

                var Sheet9 = Ep.Workbook.Worksheets.Add("УровеньДоступа");
                Sheet9.Cells["A1"].Value = "ID_Уровень";
                Sheet9.Cells["B1"].Value = "НаименованиеУровень";

                var row9 = 2;
                foreach (var entrance in _context.AccessLevels.ToList())
                {
                    Sheet9.Cells[string.Format("A{0}", row9)].Value = entrance.IdLevel;
                    Sheet9.Cells[string.Format("B{0}", row9)].Value = entrance.TitleLevel;
                    row9++;
                }
                Sheet9.Cells["A:AZ"].AutoFitColumns();

                var Sheet10 = Ep.Workbook.Worksheets.Add("Пропуска");
                Sheet10.Cells["A1"].Value = "№ пропуска";
                Sheet10.Cells["B1"].Value = "СтатусДлительности";
                Sheet10.Cells["C1"].Value = "СтатусАктивности";
                Sheet10.Cells["D1"].Value = "УровеньДоступа";


                var row10 = 2;
                foreach (var entrance in _context.Passes.ToList())
                {
                    _context.LastingStatuses.ToList();
                    _context.AccessLevels.ToList();
                    _context.ActivityStatuses.ToList();
                    Sheet10.Cells[string.Format("A{0}", row10)].Value = entrance.IdPass;
                    Sheet10.Cells[string.Format("B{0}", row10)].Value = entrance.IdLongs.TitleLong;
                    Sheet10.Cells[string.Format("C{0}", row10)].Value = entrance.IdActivs.TitleActiv;
                    Sheet10.Cells[string.Format("D{0}", row10)].Value = entrance.IdLevels.TitleLevel;

                    row10++;
                }
                Sheet10.Cells["A:AZ"].AutoFitColumns();

                var Sheet11 = Ep.Workbook.Worksheets.Add("Посетители");
                Sheet11.Cells["A1"].Value = "IDПосетитель";
                Sheet11.Cells["B1"].Value = "Фамилия";
                Sheet11.Cells["C1"].Value = "Имя";
                Sheet11.Cells["D1"].Value = "Отчество";
                Sheet11.Cells["E1"].Value = "МобильныйТелефон";
                Sheet11.Cells["F1"].Value = "№ пропуска";


                var row11 = 2;
                foreach (var entrance in _context.Visitors.ToList())
                {
                    Sheet11.Cells[string.Format("A{0}", row11)].Value = entrance.Idvisitor;
                    Sheet11.Cells[string.Format("B{0}", row11)].Value = entrance.Surname;
                    Sheet11.Cells[string.Format("C{0}", row11)].Value = entrance.Name;
                    Sheet11.Cells[string.Format("D{0}", row11)].Value = entrance.LastName;
                    Sheet11.Cells[string.Format("E{0}", row11)].Value = entrance.MobilePhone;
                    Sheet11.Cells[string.Format("F{0}", row11)].Value = entrance.IdPass;

                    row11++;
                }
                Sheet11.Cells["A:AZ"].AutoFitColumns();

                var Sheet12 = Ep.Workbook.Worksheets.Add("СтатусАктивности");
                Sheet12.Cells["A1"].Value = "IDСтатус";
                Sheet12.Cells["B1"].Value = "НаименованиеСтатус";

                var row12 = 2;
                foreach (var entrance in _context.ActivityStatuses.ToList())
                {
                    Sheet12.Cells[string.Format("A{0}", row12)].Value = entrance.IdActiv;
                    Sheet12.Cells[string.Format("B{0}", row12)].Value = entrance.TitleActiv;

                    row12++;
                }
                Sheet12.Cells["A:AZ"].AutoFitColumns();

                var Sheet13 = Ep.Workbook.Worksheets.Add("СтатусДлительности");
                Sheet13.Cells["A1"].Value = "IDСтатус";
                Sheet13.Cells["B1"].Value = "НаименованиеСтатус";

                var row13 = 2;
                foreach (var entrance in _context.LastingStatuses.ToList())
                {
                    Sheet13.Cells[string.Format("A{0}", row13)].Value = entrance.IdLong;
                    Sheet13.Cells[string.Format("B{0}", row13)].Value = entrance.TitleLong;

                    row13++;
                }
                Sheet13.Cells["A:AZ"].AutoFitColumns();

                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var xlFile = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EntranceReport.xls"));

                Ep.Save();
                fs.Close();
            }
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "EntranceReport.xls");

            string file_type = "application/octet-stream";

            string file_name = "EntranceReport.xls";
            return PhysicalFile(file_path, file_type, file_name);
        }

        [HttpPost]
        public IActionResult EntranceReportFilter(bool Entrance, bool Staff, bool Visitors, bool Passes, bool Post, bool Offices, bool SortByOff, bool Rooms, bool Doors, bool Levels, bool Statuses, bool Activ, bool Long)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileStream fs = new FileStream("EntranceReport.xls", FileMode.Create);
            using (ExcelPackage Ep = new ExcelPackage(fs))
            {
                if(Entrance == true)
                {
                    var Sheet1 = Ep.Workbook.Worksheets.Add("Доступ");
                    Sheet1.Cells["A1"].Value = "ID_Запись";
                    Sheet1.Cells["B1"].Value = "Дата_Входа";
                    Sheet1.Cells["C1"].Value = "Дата_Выхода";
                    Sheet1.Cells["D1"].Value = "Помещение";
                    Sheet1.Cells["E1"].Value = "№ пропуска";
                    Sheet1.Cells["F1"].Value = "ТипТурникета";
                    Sheet1.Cells["G1"].Value = "СтатусВхода";

                    var row1 = 2;
                    foreach (var entrance in _context.Entrances.ToList())
                    {
                        _context.staff.ToList();
                        _context.Rooms.ToList();
                        _context.Doors.ToList();
                        _context.AccessStatuses.ToList();
                        Sheet1.Cells[string.Format("A{0}", row1)].Value = entrance.IdRecord;
                        Sheet1.Cells[string.Format("B{0}", row1)].Value = entrance.DateEntr.ToString();
                        Sheet1.Cells[string.Format("C{0}", row1)].Value = entrance.DateExit.ToString();
                        Sheet1.Cells[string.Format("D{0}", row1)].Value = entrance.IdRooms.TitleRoom;
                        Sheet1.Cells[string.Format("E{0}", row1)].Value = entrance.IdPass;
                        Sheet1.Cells[string.Format("F{0}", row1)].Value = entrance.IdDoors.TitleDoor;
                        Sheet1.Cells[string.Format("G{0}", row1)].Value = entrance.IdStatuses.TitleStatus;
                        row1++;
                    }
                    Sheet1.Cells["A:AZ"].AutoFitColumns();
                }

                if(Staff == true)
                {
                    var Sheet2 = Ep.Workbook.Worksheets.Add("Сотрудники");
                    Sheet2.Cells["A1"].Value = "ID_Сотрудника";
                    Sheet2.Cells["B1"].Value = "ФамилияСотрудника";
                    Sheet2.Cells["C1"].Value = "ИмяСотрудника";
                    Sheet2.Cells["D1"].Value = "ОтчетсвоСотрудника";
                    Sheet2.Cells["E1"].Value = "ДатаРождения";
                    Sheet2.Cells["F1"].Value = "КорпоративнаяПочта";
                    Sheet2.Cells["G1"].Value = "ТелефонМобильный";
                    Sheet2.Cells["H1"].Value = "Изображение";
                    Sheet2.Cells["I1"].Value = "№ пропуска";

                    var row2 = 2;
                    foreach (var entrance in _context.staff.ToList())
                    {
                        _context.AccessLevels.ToList();
                        Sheet2.Cells[string.Format("A{0}", row2)].Value = entrance.IdStaff;
                        Sheet2.Cells[string.Format("B{0}", row2)].Value = entrance.Surname;
                        Sheet2.Cells[string.Format("C{0}", row2)].Value = entrance.Name;
                        Sheet2.Cells[string.Format("D{0}", row2)].Value = entrance.LastName;
                        Sheet2.Cells[string.Format("E{0}", row2)].Value = entrance.Birthday.ToString();
                        Sheet2.Cells[string.Format("F{0}", row2)].Value = entrance.CorpEmail;
                        Sheet2.Cells[string.Format("G{0}", row2)].Value = entrance.MobPhone;
                        Sheet2.Cells[string.Format("H{0}", row2)].Value = entrance.Image;
                        Sheet2.Cells[string.Format("I{0}", row2)].Value = entrance.IdPass;

                        row2++;
                    }
                    Sheet2.Cells["A:AZ"].AutoFitColumns();
                }

                if(Visitors == true)
                {
                    var Sheet11 = Ep.Workbook.Worksheets.Add("Посетители");
                    Sheet11.Cells["A1"].Value = "IDПосетитель";
                    Sheet11.Cells["B1"].Value = "Фамилия";
                    Sheet11.Cells["C1"].Value = "Имя";
                    Sheet11.Cells["D1"].Value = "Отчество";
                    Sheet11.Cells["E1"].Value = "МобильныйТелефон";
                    Sheet11.Cells["F1"].Value = "№ пропуска";

                    var row11 = 2;
                    foreach (var entrance in _context.Visitors.ToList())
                    {
                        Sheet11.Cells[string.Format("A{0}", row11)].Value = entrance.Idvisitor;
                        Sheet11.Cells[string.Format("B{0}", row11)].Value = entrance.Surname;
                        Sheet11.Cells[string.Format("C{0}", row11)].Value = entrance.Name;
                        Sheet11.Cells[string.Format("D{0}", row11)].Value = entrance.LastName;
                        Sheet11.Cells[string.Format("E{0}", row11)].Value = entrance.MobilePhone;
                        Sheet11.Cells[string.Format("F{0}", row11)].Value = entrance.IdPass;

                        row11++;
                    }
                    Sheet11.Cells["A:AZ"].AutoFitColumns();
                }

                if(Passes == true)
                {
                    var Sheet10 = Ep.Workbook.Worksheets.Add("Пропуска");
                    Sheet10.Cells["A1"].Value = "№ пропуска";
                    Sheet10.Cells["B1"].Value = "СтатусДлительности";
                    Sheet10.Cells["C1"].Value = "СтатусАктивности";
                    Sheet10.Cells["D1"].Value = "УровеньДоступа";


                    var row10 = 2;
                    foreach (var entrance in _context.Passes.ToList())
                    {
                        _context.LastingStatuses.ToList();
                        _context.AccessLevels.ToList();
                        _context.ActivityStatuses.ToList();
                        Sheet10.Cells[string.Format("A{0}", row10)].Value = entrance.IdPass;
                        Sheet10.Cells[string.Format("B{0}", row10)].Value = entrance.IdLongs.TitleLong;
                        Sheet10.Cells[string.Format("C{0}", row10)].Value = entrance.IdActivs.TitleActiv;
                        Sheet10.Cells[string.Format("D{0}", row10)].Value = entrance.IdLevels.TitleLevel;

                        row10++;
                    }
                    Sheet10.Cells["A:AZ"].AutoFitColumns();
                }

                if(Post == true)
                {
                    var Sheet3 = Ep.Workbook.Worksheets.Add("Должности");
                    Sheet3.Cells["A1"].Value = "ID_Должности";
                    Sheet3.Cells["B1"].Value = "НаименованиеДолжности";

                    var row3 = 2;
                    foreach (var entrance in _context.Positions.ToList())
                    {
                        Sheet3.Cells[string.Format("A{0}", row3)].Value = entrance.IdPost;
                        Sheet3.Cells[string.Format("B{0}", row3)].Value = entrance.TitlePost;
                        row3++;
                    }
                    Sheet3.Cells["A:AZ"].AutoFitColumns();
                }

                if(Offices == true)
                {
                    var Sheet4 = Ep.Workbook.Worksheets.Add("Отделы");
                    Sheet4.Cells["A1"].Value = "ID_Отдела";
                    Sheet4.Cells["B1"].Value = "НаименованиеОтдела";

                    var row4 = 2;
                    foreach (var entrance in _context.Offices.ToList())
                    {
                        Sheet4.Cells[string.Format("A{0}", row4)].Value = entrance.IdOffice;
                        Sheet4.Cells[string.Format("B{0}", row4)].Value = entrance.TitleOffice;
                        row4++;
                    }
                    Sheet4.Cells["A:AZ"].AutoFitColumns();
                }

                if(SortByOff == true)
                {
                    var Sheet5 = Ep.Workbook.Worksheets.Add("РаспределениеПоОтделам");
                    Sheet5.Cells["A1"].Value = "ID_Запись";
                    Sheet5.Cells["B1"].Value = "Сотрудник";
                    Sheet5.Cells["C1"].Value = "Отдел";
                    Sheet5.Cells["D1"].Value = "Должности";
                    Sheet5.Cells["E1"].Value = "ВремяНачалаРаботы";
                    Sheet5.Cells["F1"].Value = "ВремяЗавершенияРаботы";
                    Sheet5.Cells["G1"].Value = "ТелефонРабочий";

                    var row5 = 2;
                    foreach (var entrance in _context.SortingByOffices.ToList())
                    {
                        _context.staff.ToList();
                        _context.Offices.ToList();
                        _context.Positions.ToList();
                        Sheet5.Cells[string.Format("A{0}", row5)].Value = entrance.IdItem;
                        Sheet5.Cells[string.Format("B{0}", row5)].Value = entrance.IdStaffs.Surname;
                        Sheet5.Cells[string.Format("C{0}", row5)].Value = entrance.IdOffices.TitleOffice;
                        Sheet5.Cells[string.Format("D{0}", row5)].Value = entrance.IdPosts.TitlePost;
                        Sheet5.Cells[string.Format("E{0}", row5)].Value = entrance.TimeBegin.ToString();
                        Sheet5.Cells[string.Format("F{0}", row5)].Value = entrance.TimeEnd.ToString();
                        Sheet5.Cells[string.Format("G{0}", row5)].Value = entrance.WorkPhone;
                        row5++;
                    }
                    Sheet5.Cells["A:AZ"].AutoFitColumns();

                }

                if(Rooms == true)
                {
                    var Sheet6 = Ep.Workbook.Worksheets.Add("Помещения");
                    Sheet6.Cells["A1"].Value = "ID_Помещения";
                    Sheet6.Cells["B1"].Value = "НаименованиеПомещения";
                    Sheet6.Cells["C1"].Value = "УровеньДоступа";

                    var row6 = 2;
                    foreach (var entrance in _context.Rooms.ToList())
                    {
                        _context.AccessLevels.ToList();
                        Sheet6.Cells[string.Format("A{0}", row6)].Value = entrance.IdRoom;
                        Sheet6.Cells[string.Format("B{0}", row6)].Value = entrance.TitleRoom;
                        Sheet6.Cells[string.Format("C{0}", row6)].Value = entrance.IdLevels.TitleLevel;
                        row6++;
                    }
                    Sheet6.Cells["A:AZ"].AutoFitColumns();
                }

                if(Doors == true)
                {
                    var Sheet7 = Ep.Workbook.Worksheets.Add("ТипыТурникетов");
                    Sheet7.Cells["A1"].Value = "ID_ТипТурникета";
                    Sheet7.Cells["B1"].Value = "НаименованиеТипаТурникета";
                    Sheet7.Cells["C1"].Value = "Помещение";

                    var row7 = 2;
                    foreach (var entrance in _context.Doors.ToList())
                    {
                        _context.Rooms.ToList();
                        Sheet7.Cells[string.Format("A{0}", row7)].Value = entrance.IdDoor;
                        Sheet7.Cells[string.Format("B{0}", row7)].Value = entrance.TitleDoor;
                        Sheet7.Cells[string.Format("C{0}", row7)].Value = entrance.IdRooms.TitleRoom;
                        row7++;
                    }
                    Sheet7.Cells["A:AZ"].AutoFitColumns();
                }

                if(Levels == true)
                {
                    var Sheet9 = Ep.Workbook.Worksheets.Add("УровеньДоступа");
                    Sheet9.Cells["A1"].Value = "ID_Уровень";
                    Sheet9.Cells["B1"].Value = "НаименованиеУровень";

                    var row9 = 2;
                    foreach (var entrance in _context.AccessLevels.ToList())
                    {
                        Sheet9.Cells[string.Format("A{0}", row9)].Value = entrance.IdLevel;
                        Sheet9.Cells[string.Format("B{0}", row9)].Value = entrance.TitleLevel;
                        row9++;
                    }
                    Sheet9.Cells["A:AZ"].AutoFitColumns();
                }

                if(Statuses == true)
                {
                    var Sheet8 = Ep.Workbook.Worksheets.Add("СтатусДоступа");
                    Sheet8.Cells["A1"].Value = "ID_Статус";
                    Sheet8.Cells["B1"].Value = "НаименованиеСтатус";

                    var row8 = 2;
                    foreach (var entrance in _context.AccessStatuses.ToList())
                    {
                        Sheet8.Cells[string.Format("A{0}", row8)].Value = entrance.IdStatus;
                        Sheet8.Cells[string.Format("B{0}", row8)].Value = entrance.TitleStatus;
                        row8++;
                    }
                    Sheet8.Cells["A:AZ"].AutoFitColumns();
                }

                if(Activ == true)
                {
                    var Sheet12 = Ep.Workbook.Worksheets.Add("СтатусАктивности");
                    Sheet12.Cells["A1"].Value = "IDСтатус";
                    Sheet12.Cells["B1"].Value = "НаименованиеСтатус";

                    var row12 = 2;
                    foreach (var entrance in _context.ActivityStatuses.ToList())
                    {
                        Sheet12.Cells[string.Format("A{0}", row12)].Value = entrance.IdActiv;
                        Sheet12.Cells[string.Format("B{0}", row12)].Value = entrance.TitleActiv;

                        row12++;
                    }
                    Sheet12.Cells["A:AZ"].AutoFitColumns();
                }

                if(Long == true)
                {
                    var Sheet13 = Ep.Workbook.Worksheets.Add("СтатусДлительности");
                    Sheet13.Cells["A1"].Value = "IDСтатус";
                    Sheet13.Cells["B1"].Value = "НаименованиеСтатус";

                    var row13 = 2;
                    foreach (var entrance in _context.LastingStatuses.ToList())
                    {
                        Sheet13.Cells[string.Format("A{0}", row13)].Value = entrance.IdLong;
                        Sheet13.Cells[string.Format("B{0}", row13)].Value = entrance.TitleLong;

                        row13++;
                    }
                    Sheet13.Cells["A:AZ"].AutoFitColumns();
                }

                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var xlFile = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EntranceReport.xls"));

                Ep.Save();
                fs.Close();
            }
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "EntranceReport.xls");

            string file_type = "application/octet-stream";

            string file_name = "EntranceReport.xls";
            return PhysicalFile(file_path, file_type, file_name);
        }
        #endregion
    }
}
