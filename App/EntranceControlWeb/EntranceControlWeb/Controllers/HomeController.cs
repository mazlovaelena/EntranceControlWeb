using EntranceControlWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace EntranceControlWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private EntranceControlWebContext _context;

        public HomeController(ILogger<HomeController> logger, EntranceControlWebContext context)
        {
            _logger = logger;
            _context = context;
        }

        #region  ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ТУРНИКЕТЫ"
        //Отображение страницы
        public IActionResult Doors(DoorViewModel door)
        {
            door.Doors = _context.Doors.ToList();
            door.Rooms = _context.Rooms.ToList();
            return View(door);
        }
        //Удаление записи
        public IActionResult DelDoor(int id)
        {
            var data = _context.Doors.FirstOrDefault(x => x.IdDoor == id);
            if (data != null)
            {
                _context.Doors.Remove(data);
            }

            return RedirectToAction(nameof(Doors));

        }
        //Редактирование записи
        [HttpPost]
        public IActionResult DoorEdit(DoorViewModel door)
        {
            var edit = _context.Doors.FirstOrDefault(x => x.IdDoor == door.IdDoor);

            edit.IdDoor = door.IdDoor;
            edit.TitleDoor = door.TitleDoor;
            edit.IdRoom = door.IdRoom;

            _context.SaveChanges();
            return RedirectToAction(nameof(Doors));
        }
        public IActionResult DoorEdit(int id)
        {
            var viewmodel = new DoorViewModel();
            var view = _context.Doors.FirstOrDefault(x => x.IdDoor == id);
            if (view != null)
            {
                viewmodel.IdDoor = view.IdDoor;
                viewmodel.TitleDoor = view.TitleDoor;
                viewmodel.IdRoom = view.IdRoom;

            }
            return Json(viewmodel);
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "УРОВНИ ДОСТУПА"
        //Отображение сраницы
        public IActionResult Levels(AccessLevelViewModel acclev, AccessStatusViewModel accstat)
        {
            acclev.Levels = _context.AccessLevels.ToList();
            accstat.Statuses = _context.AccessStatuses.ToList();

            return View(acclev);
        }
        //Удаление записи
        [HttpPost]
        public IActionResult DelLevel(AccessLevelViewModel acclev)
        {
            var data = _context.AccessLevels.FirstOrDefault(x => x.IdLevel == acclev.IdLevel);

            //data.IdLevel = acclev.IdLevel;
            //data.TitleLevel = acclev.TitleLevel;

            _context.AccessLevels.Remove(data);

            return RedirectToAction(nameof(Levels));
        }
        public IActionResult DelLevel(int id)
        {
            var viewmodel = new AccessLevelViewModel();
            var data = _context.AccessLevels.FirstOrDefault(x => x.IdLevel == id);
            if (data != null)
            {
                viewmodel.IdLevel = data.IdLevel;
                viewmodel.TitleLevel = data.TitleLevel;

            }

            return Json(viewmodel);

        }
        //Редактирование записи
        [HttpPost]
        public IActionResult LevelEdit(AccessLevelViewModel acclev)
        {
            var edit = _context.AccessLevels.FirstOrDefault(x => x.IdLevel == acclev.IdLevel);

            edit.IdLevel = acclev.IdLevel;
            edit.TitleLevel = acclev.TitleLevel;

            _context.SaveChanges();
            return RedirectToAction(nameof(Levels));
        }
        public IActionResult LevelEdit(int id)
        {
            var viewmodel = new AccessLevelViewModel();
            var view = _context.AccessLevels.FirstOrDefault(x => x.IdLevel == id);
            if (view != null)
            {
                viewmodel.IdLevel = view.IdLevel;
                viewmodel.TitleLevel = view.TitleLevel;

            }
            return Json(viewmodel);
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ОТДЕЛЫ"
        //Отображение страницы
        public IActionResult Offices(OfficeViewModel off)
        {
            off.Offices = _context.Offices.ToList();
            return View(off);
        }
        //Удаление записи
        public IActionResult DelOffice(int id)
        {
            var data = _context.Offices.FirstOrDefault(x => x.IdOffice == id);
            if (data != null)
            {
                _context.Offices.Remove(data);
            }

            return RedirectToAction(nameof(Offices));

        }
        //Редактирование записи
        [HttpPost]
        public IActionResult OfficeEdit(OfficeViewModel off)
        {
            var edit = _context.Offices.FirstOrDefault(x => x.IdOffice == off.IdOffice);

            edit.IdOffice = off.IdOffice;
            edit.TitleOffice = off.TitleOffice;

            _context.SaveChanges();
            return RedirectToAction(nameof(Offices));
        }
        public IActionResult OfficeEdit(int id)
        {
            var viewmodel = new OfficeViewModel();
            var view = _context.Offices.FirstOrDefault(x => x.IdOffice == id);
            if (view != null)
            {
                viewmodel.IdOffice = view.IdOffice;
                viewmodel.TitleOffice = view.TitleOffice;

            }
            return Json(viewmodel);
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ДОЛЖНОСТИ"
        //Отображение страницы
        public IActionResult Positions(PositionViewModel pos)
        {
            pos.Positions = _context.Positions.ToList();
            return View(pos);
        }
        //Удаление записи
        public IActionResult DelPos(int id)
        {
            var data = _context.Positions.FirstOrDefault(x => x.IdPost == id);
            if (data != null)
            {
                _context.Positions.Remove(data);
            }

            return RedirectToAction(nameof(Positions));

        }
        //Редактирование записи
        [HttpPost]
        public IActionResult PosEdit(PositionViewModel pos)
        {
            var edit = _context.Positions.FirstOrDefault(x => x.IdPost == pos.IdPost);

            edit.IdPost = pos.IdPost;
            edit.TitlePost = pos.TitlePost;

            _context.SaveChanges();
            return RedirectToAction(nameof(Positions));
        }
        public IActionResult PosEdit(int id)
        {
            var viewmodel = new PositionViewModel();
            var view = _context.Positions.FirstOrDefault(x => x.IdPost == id);
            if (view != null)
            {
                viewmodel.IdPost = view.IdPost;
                viewmodel.TitlePost = view.TitlePost;

            }
            return Json(viewmodel);
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ПОМЕЩЕНИЯ"

        //Отображение данных
        public IActionResult Rooms(RoomViewModel room)
        {
            room.Rooms = _context.Rooms.ToList();
            room.Levels = _context.AccessLevels.ToList();
            return View(room);
        }
        //Удаление записи
        public IActionResult DelRoom(int id)
        {
            var data = _context.Rooms.FirstOrDefault(x => x.IdRoom == id);
            if (data != null)
            {
                _context.Rooms.Remove(data);
            }

            return RedirectToAction(nameof(Rooms));

        }
        //Редактирование записи
        [HttpPost]
        public IActionResult RoomEdit(RoomViewModel room)
        {
            var edit = _context.Rooms.FirstOrDefault(x => x.IdRoom == room.IdRoom);

            edit.IdRoom = room.IdRoom;
            edit.TitleRoom = room.TitleRoom;
            edit.IdLevel = room.IdLevel;

            _context.SaveChanges();
            return RedirectToAction(nameof(Rooms));
        }
        public IActionResult RoomEdit(int id)
        {
            var viewmodel = new RoomViewModel();
            var view = _context.Rooms.FirstOrDefault(x => x.IdRoom == id);
            if (view != null)
            {
                viewmodel.IdRoom = view.IdRoom;
                viewmodel.TitleRoom = view.TitleRoom;
                viewmodel.IdLevel = view.IdLevel;

            }
            return Json(viewmodel);
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "РАСПРЕДЕЛЕНИЕ ПО ОТДЕЛАМ"

        //Отображение страницы
        public IActionResult SortByOff(SortingByOfficeViewModel sort)
        {
            sort.Sortings = _context.SortingByOffices.ToList();
            sort.Staffs = _context.staff.ToList();
            sort.Positions = _context.Positions.ToList();
            sort.Offices = _context.Offices.ToList();
            return View(sort);
        }
        //Удаление записи
        public IActionResult DelSort(int id)
        {
            var data = _context.SortingByOffices.FirstOrDefault(x => x.IdItem == id);
            if (data != null)
            {
                _context.SortingByOffices.Remove(data);
            }

            return RedirectToAction(nameof(SortByOff));

        }
        //Редактирование записи
        [HttpPost]
        public IActionResult SortEdit(SortingByOfficeViewModel sort)
        {
            var edit = _context.SortingByOffices.FirstOrDefault(x => x.IdItem == sort.IdItem);

            edit.IdItem = sort.IdItem;
            edit.TimeBegin = sort.TimeBegin;
            edit.TimeEnd = sort.TimeEnd;
            edit.WorkPhone = sort.WorkPhone;
            edit.IdStaff = sort.IdStaff;
            edit.IdPost = sort.IdPost;
            edit.IdOffice = sort.IdOffice;

            _context.SaveChanges();
            return RedirectToAction(nameof(SortByOff));
        }
        public IActionResult SortEdit(SortingByOfficeViewModel sort, int id)
        {
            var view = _context.SortingByOffices.FirstOrDefault(x => x.IdItem == id);
            if (id != 0)
            {
                var edit = _context.SortingByOffices.FirstOrDefault(x => x.IdItem == id);
                sort.IdItem = edit.IdItem;
                sort.TimeBegin = edit.TimeBegin;
                sort.TimeEnd = edit.TimeEnd;
                sort.WorkPhone = edit.WorkPhone;
                sort.IdStaff = edit.IdStaff;
                sort.IdPost = edit.IdPost;
                sort.IdOffice = edit.IdOffice;
                return Json(sort);
            }
            return View(sort);
        }

        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "СОТРУДНИКИ"
        //Отображение данных
        public IActionResult Staff(StaffViewModel staff)
        {
            staff.Staffs = _context.staff.ToList();
            staff.Levels = _context.AccessLevels.ToList();
            staff.Positions = _context.Positions.ToList();
            staff.Offices = _context.Offices.ToList();
            return View(staff);
        }

        //Редактирование данных
        [HttpPost]
        public IActionResult StaffEdit (StaffViewModel staff)
        {
            var edit = _context.staff.FirstOrDefault(x => x.IdStaff == staff.IdStaff);
            edit.IdStaff = staff.IdStaff;
            edit.Surname = staff.Surname;
            edit.Name = staff.Name;
            edit.LastName = staff.LastName;
            edit.Birthday = staff.Birthday;
            edit.CorpEmail = staff.CorpEmail;
            edit.MobPhone = staff.MobPhone;
            edit.Image = staff.Image;
            edit.IdLevel = staff.IdLevel;

            _context.SaveChanges();

            return RedirectToAction(nameof(Staff));
        }
        public IActionResult StaffEdit(StaffViewModel staff, int id)
        {
            var view = _context.staff.FirstOrDefault(x => x.IdStaff == id);
            if(id != 0)
            {
                var edit = _context.staff.FirstOrDefault(x => x.IdStaff == id);

                staff.IdStaff = edit.IdStaff;
                staff.Surname = edit.Surname;
                staff.Name = edit.Name;
                staff.LastName = edit.LastName;
                staff.Birthday = edit.Birthday;
                staff.CorpEmail = edit.CorpEmail;
                staff.MobPhone = edit.MobPhone;
                staff.Image = edit.Image;
                staff.IdLevel = edit.IdLevel;
            }
            return View(staff);
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ДОСТУП"
        //Отображение данных
        public IActionResult Entrance(EntranceViewModel entr)
        {
            entr.Entrances = _context.Entrances.ToList();
            entr.Staffs = _context.staff.ToList();
            entr.Rooms = _context.Rooms.ToList();
            entr.Doors = _context.Doors.ToList();
            entr.Statuses = _context.AccessStatuses.ToList();
            return View(entr);
        }
        public IActionResult ViewStaff(int id)
        {
            var viewmodel = new StaffViewModel();
            var view = _context.staff.FirstOrDefault(x => x.IdStaff == id);
            if(view != null)
            {
                viewmodel.IdStaff = view.IdStaff;
                viewmodel.Surname = view.Surname;
                viewmodel.Name = view.Name;
                viewmodel.LastName = view.LastName;
                viewmodel.Birthday = view.Birthday;
                viewmodel.CorpEmail = view.CorpEmail;
                viewmodel.MobPhone = view.MobPhone;
                viewmodel.Image = view.Image;                      
                
            }
            return Json(viewmodel);
        }

        #endregion


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int code)
        {
            if (code == 404)
            {
                return View("Error");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
