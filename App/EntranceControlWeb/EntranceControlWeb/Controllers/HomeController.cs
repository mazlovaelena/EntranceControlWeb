using EntranceControlWeb.Models;
using EntranceControlWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult Doors(DoorViewModel door, string sort)
        {
            door.RoomSelect = new SelectList(_context.Rooms, "IdRoom", "TitleRoom");
            ViewBag.RoomSort = sort == "Room" ? "Room dsc" : "Room";
            ViewBag.DoorSort = String.IsNullOrEmpty(sort) ? "Door dsc" : "Door";
            var find = from s in _context.Doors select s;
            switch (sort)
            {
                case "Room":
                    find = find.OrderBy(s => s.IdRoom);
                    break;

                case "Room dsc":
                    find = find.OrderByDescending(s => s.IdRoom);
                    break;

                case "Door":
                    find = find.OrderBy(s => s.TitleDoor);
                    break;

                case "Door dsc":
                    find = find.OrderByDescending(s => s.TitleDoor);
                    break;
            }

            door.Doors = find.ToList();
            door.Rooms = _context.Rooms.ToList();
            return View(door);
        }
        //Удаление записи
        public IActionResult DelDoor(int id)
        {
            var data = _context.Doors.FirstOrDefault(x => x.IdDoor == id);
            var entr = _context.Entrances.FirstOrDefault(x => x.IdDoor == id);

            if (entr != null)
            {
                _context.Entrances.Remove(entr);
                _context.Doors.Remove(data);
            }

            _context.SaveChanges();
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
        //Создание записи
        [HttpPost]
        public IActionResult CreateDoor(DoorViewModel door)
        {
            var create = new Door { TitleDoor = door.TitleDoor, IdRoom = door.IdRoom };

            _context.Doors.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Doors));

        }
        public IActionResult CreateDoor()
        {
            var door = new DoorViewModel();
            SelectList room = new SelectList(_context.Rooms, "IdRoom", "TitleRoom");
            ViewBag.Room = room;

            door.Doors = _context.Doors.ToList();
            door.Rooms = _context.Rooms.ToList();
            return View(door);
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ОТДЕЛЫ"
        //Отображение страницы
        public IActionResult Offices(OfficeViewModel off, string sort, string Search)
        {
            ViewBag.OffSort = String.IsNullOrEmpty(sort) ? "Off dsc" : "Off";
            var find = from s in _context.Offices select s;

            if (!String.IsNullOrEmpty(Search))
            {
                find = find.Where(s => s.TitleOffice.ToUpper().Contains(Search.ToUpper()));
            }

            switch (sort)
            {
                case "Off":
                    find = find.OrderBy(s => s.IdOffice);
                    break;

                case "Off dsc":
                    find = find.OrderByDescending(s => s.IdOffice);
                    break;
            }

            off.Offices = find.ToList();
            return View(off);
        }
        //Удаление записи
        public IActionResult DelOffice(int id)
        {
            var data = _context.Offices.FirstOrDefault(x => x.IdOffice == id);
            var office = _context.SortingByOffices.FirstOrDefault(x => x.IdOffice == id);

            if (data != null)
            {
                _context.SortingByOffices.Remove(office);
                _context.Offices.Remove(data);
            }

            _context.SaveChanges();
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
        //Создание записи
        [HttpPost]
        public IActionResult CreateOffice(OfficeViewModel off)
        {
            var create = new Office { TitleOffice = off.TitleOffice };

            _context.Offices.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Offices));
        }
        public IActionResult CreateOffice()
        {
            return View();
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ДОЛЖНОСТИ"
        //Отображение страницы
        public IActionResult Positions(PositionViewModel pos, string sort, string Search)
        {
            ViewBag.PostSort = String.IsNullOrEmpty(sort) ? "Post dsc" : "";
            var find = from s in _context.Positions select s;

            if (!String.IsNullOrEmpty(Search))
            {
                find = find.Where(s => s.TitlePost.ToUpper().Contains(Search.ToUpper()));
            }

            switch (sort)
            {
                default:
                    find = find.OrderBy(s => s.IdPost);
                    break;

                case "Post dsc":
                    find = find.OrderByDescending(s => s.IdPost);
                    break;
            }

            pos.Positions = find.ToList();
            return View(pos);
        }
        //Удаление записи
        public IActionResult DelPos(int id)
        {
            var data = _context.Positions.FirstOrDefault(x => x.IdPost == id);
            var office = _context.SortingByOffices.FirstOrDefault(x => x.IdPost == id);

            if (data != null)
            {
                _context.SortingByOffices.Remove(office);
                _context.Positions.Remove(data);
            }

            _context.SaveChanges();
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
        //Создание записи
        [HttpPost]
        public IActionResult CreatePost(Position pos, int id)
        {
            var create = new Position { TitlePost = pos.TitlePost };

            _context.Positions.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Positions));
        }
        public IActionResult CreatePost()
        {
            return View();
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ПОМЕЩЕНИЯ"

        //Отображение данных
        public IActionResult Rooms(RoomViewModel room, string sort)
        {
            room.LevelSelect = new SelectList(_context.AccessLevels, "IdLevel", "TitleLevel");

            ViewBag.RoomSort = sort == "Room" ? "Room dsc" : "Room";
            ViewBag.LevelSort = sort == "Level" ? "Level dsc" : "Level";
            var find = from s in _context.Rooms select s;
            switch (sort)
            {
                case "Room":
                    find = find.OrderBy(s => s.IdRoom);
                    break;

                case "Room dsc":
                    find = find.OrderByDescending(s => s.IdRoom);
                    break;

                case "Level":
                    find = find.OrderBy(s => s.IdLevel);
                    break;

                case "Level dsc":
                    find = find.OrderByDescending(s => s.IdLevel);
                    break;
            }

            room.Rooms = find.ToList();
            room.Levels = _context.AccessLevels.ToList();
            return View(room);
        }
        //Удаление записи
        public IActionResult DelRoom(int id)
        {
            var data = _context.Rooms.FirstOrDefault(x => x.IdRoom == id);
            var entr = _context.Entrances.FirstOrDefault(x => x.IdRoom == id);
            var door = _context.Doors.FirstOrDefault(x => x.IdRoom == id);

            if (data != null)
            {
                _context.Entrances.Remove(entr);
                _context.Doors.Remove(door);
                _context.Rooms.Remove(data);
            }

            _context.SaveChanges();
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
        //Создание записи
        [HttpPost]
        public IActionResult CreateRoom(RoomViewModel room)
        {
            var create = new Room { TitleRoom = room.TitleRoom, IdLevel = room.IdLevel };

            _context.Rooms.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Rooms));

        }
        public IActionResult CreateRoom()
        {
            var room = new RoomViewModel();
            SelectList lev = new SelectList(_context.AccessLevels, "IdLevel", "TitleLevel");
            ViewBag.Levels = lev;

            room.Rooms = _context.Rooms.ToList();
            room.Levels = _context.AccessLevels.ToList();
            return View(room);
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "РАСПРЕДЕЛЕНИЕ ПО ОТДЕЛАМ"

        //Отображение страницы
        public IActionResult SortByOff(SortingByOfficeViewModel sort, string order)
        {
            ViewBag.SortStaff = order == "Staff" ? "Staff dsc" : "Staff";
            ViewBag.SortPost = order == "Post" ? "Post dsc" : "Post";
            ViewBag.SortOffice = order == "Office" ? "Office dsc" : "Office";
            ViewBag.IdSort = order == "Id" ? "Id desc" : "Id";
            var find = from s in _context.SortingByOffices select s;
            switch (order)
            {
                case "Staff":
                    find = find.OrderBy(s => s.IdStaff);
                    break;

                case "Staff dsc":
                    find = find.OrderByDescending(s => s.IdStaff);
                    break;

                case "Post":
                    find = find.OrderBy(s => s.IdPost);
                    break;

                case "Post dsc":
                    find = find.OrderByDescending(s => s.IdPost);
                    break;

                case "Office":
                    find = find.OrderBy(s => s.IdOffice);
                    break;

                case "Office dsc":
                    find = find.OrderByDescending(s => s.IdOffice);
                    break;

                case "Id":
                    find = find.OrderBy(s => s.IdItem);
                    break;

                case "Id desc":
                    find = find.OrderByDescending(s => s.IdItem);
                    break;
            }

            sort.Sortings = find.ToList();
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
                _context.SaveChanges();
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
            sort.StaffSelect = new SelectList(_context.staff, "IdStaff", "Surname");
            sort.PostSelect = new SelectList(_context.Positions, "IdPost", "TitlePost");
            sort.OfficeSelect = new SelectList(_context.Offices, "IdOffice", "TitleOffice");

            sort.Sortings = _context.SortingByOffices.ToList();
            sort.Staffs = _context.staff.ToList();
            sort.Positions = _context.Positions.ToList();
            sort.Offices = _context.Offices.ToList();

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

            }
            return View(sort);
        }
        //Создание записи
        [HttpPost]
        public IActionResult CreateSort(SortingByOfficeViewModel sort)
        {
            var create = new SortingByOffice
            {
                TimeBegin = sort.TimeBegin,
                TimeEnd = sort.TimeEnd,
                WorkPhone = sort.WorkPhone,
                IdStaff = sort.IdStaff,
                IdPost = sort.IdPost,
                IdOffice = sort.IdOffice,
            };

            _context.SortingByOffices.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(SortByOff));
        }
        public IActionResult CreateSort()
        {
            var sort = new SortingByOfficeViewModel();
            SelectList staff = new SelectList(_context.staff, "IdStaff", "Surname");
            ViewBag.Staff = staff;

            SelectList pos = new SelectList(_context.Positions, "IdPost", "TitlePost");
            ViewBag.Position = pos;

            SelectList of = new SelectList(_context.Offices, "IdOffice", "TitleOffice");
            ViewBag.Office = of;

            sort.Sortings = _context.SortingByOffices.ToList();
            sort.Staffs = _context.staff.ToList();
            sort.Positions = _context.Positions.ToList();
            sort.Offices = _context.Offices.ToList();

            return View(sort);
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "СОТРУДНИКИ"
        //Отображение данных
        public IActionResult Staff(StaffViewModel staff, string Search)
        {
            var find = from s in _context.staff select s;

            if (!String.IsNullOrEmpty(Search))
            {
                find = find.Where(s => s.Surname.ToUpper().Contains(Search.ToUpper())
                                       || s.Name.ToUpper().Contains(Search.ToUpper())
                                       || s.LastName.ToUpper().Contains(Search.ToUpper()));
            }

            staff.ActivSelect = new SelectList(_context.ActivityStatuses, "IdActiv", "TitleActiv");
            staff.LongSelect = new SelectList(_context.LastingStatuses, "IdLong", "TitleLong");
            staff.LevelSelect = new SelectList(_context.AccessLevels, "IdLevel", "TitleLevel");

            staff.Passes = _context.Passes.ToList();
            staff.Lastings = _context.LastingStatuses.ToList();
            staff.Activities = _context.ActivityStatuses.ToList();
            staff.Levels = _context.AccessLevels.ToList();
            staff.Staffs = find.ToList();            

            return View(staff);
        }

        //Редактирование данных
        [HttpPost]
        public IActionResult StaffEdit(StaffViewModel staff)
        {
            var edit = _context.staff.FirstOrDefault(x => x.IdStaff == staff.IdStaff);

            staff.Staffs = _context.staff.ToList();
            
            edit.IdStaff = staff.IdStaff;
            edit.Surname = staff.Surname;
            edit.Name = staff.Name;
            edit.LastName = staff.LastName;
            edit.Birthday = staff.Birthday;
            edit.CorpEmail = staff.CorpEmail;
            edit.MobPhone = staff.MobPhone;
            edit.Image = staff.Image;            
            edit.IdPass = staff.IdPass;

            _context.SaveChanges();

            return RedirectToAction(nameof(Staff));
        }
        public IActionResult StaffEdit(StaffViewModel staff, int id)
        {
            staff.Staffs = _context.staff.ToList();
           

            var view = _context.staff.FirstOrDefault(x => x.IdStaff == id);
            if (id != 0)
            {
                var edit = _context.staff.FirstOrDefault(x => x.IdStaff == id);

                staff.Staffs = _context.staff.ToList();               

                staff.IdStaff = edit.IdStaff;
                staff.Surname = edit.Surname;
                staff.Name = edit.Name;
                staff.LastName = edit.LastName;
                staff.Birthday = edit.Birthday;
                staff.CorpEmail = edit.CorpEmail;
                staff.MobPhone = edit.MobPhone;
                staff.Image = edit.Image;               
                staff.IdPass = edit.IdPass;
            }
            return View(staff);
        }
        //Редактирование записи
        [HttpPost]
        public IActionResult PassEditStaff(PassesViewModel pass)
        {
            var edit = _context.Passes.FirstOrDefault(x => x.IdPass == pass.IdPass);

            edit.IdPass = pass.IdPass;
            edit.IdActiv = pass.IdActiv;
            edit.IdLong = pass.IdLong;
            edit.IdLevel = pass.IdLevel;

            _context.SaveChanges();
            return RedirectToAction(nameof(Staff));
        }
        public IActionResult PassEditStaff(int id)
        {
            var pass = new PassesViewModel();
            var edit = _context.Passes.FirstOrDefault(x => x.IdPass == id);
            if (edit != null)
            {
                pass.IdPass = edit.IdPass;
                pass.IdActiv = edit.IdActiv;
                pass.IdLong = edit.IdLong;
                pass.IdLevel = edit.IdLevel;
            }

            return Json(pass);
        }
        //Удаление записи
        public IActionResult DelStaff(int id)
        {
            var data = _context.staff.FirstOrDefault(x => x.IdStaff == id);
            var sort = _context.SortingByOffices.FirstOrDefault(x => x.IdStaff == id);

            if (data != null)
            {
                _context.SortingByOffices.Remove(sort);
                _context.staff.Remove(data);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Staff));

        }
        //Создание записи
        [HttpPost]
        public IActionResult CreateStaff(StaffViewModel staff)
        {
            var create = new staff
            {
                Surname = staff.Surname,
                Name = staff.Name,
                LastName = staff.LastName,
                Birthday = staff.Birthday,
                CorpEmail = staff.CorpEmail,
                MobPhone = staff.MobPhone,
                Image = staff.Image,                
                IdPass = staff.IdPass,

            };

            _context.staff.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Staff));
        }
        public IActionResult CreateStaff()
        {
            var staff = new StaffViewModel();

            staff.Staffs = _context.staff.ToList();           

            return View(staff);
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ДОСТУП"
        //Отображение данных
        public IActionResult Entrance(EntranceViewModel entr, string sort)
        {
            entr.DoorSelect = new SelectList(_context.Doors, "IdDoor", "TitleDoor");
            entr.PassSelect = new SelectList(_context.Passes, "IdPass", "IdPass");
            entr.RoomSelect = new SelectList(_context.Rooms, "IdRoom", "TitleRoom");
            entr.StatSelect = new SelectList(_context.AccessStatuses, "IdStatus", "TitleStatus");

            ViewBag.DateEntrSort = sort == "DateEntr" ? "DateEntr dsc" : "DateEntr";
            ViewBag.DoorSort = sort == "Door" ? "Door desc" : "Door";
            ViewBag.RoomSort = sort == "Room" ? "Room desc" : "Room";
            ViewBag.IdSort = sort == "Id" ? "Id desc" : "Id";
            var find = from s in _context.Entrances select s;
            switch (sort)
            {
                case "DateEntr":
                    find = find.OrderBy(s => s.DateEntr);
                    break;

                case "DateEntr dsc":
                    find = find.OrderByDescending(s => s.DateEntr);
                    break;

                case "Door desc":
                    find = find.OrderByDescending(s => s.IdDoor);
                    break;

                case "Door":
                    find = find.OrderBy(s => s.IdDoor);
                    break;

                case "Room":
                    find = find.OrderBy(s => s.IdRoom);
                    break;

                case "Room desc":
                    find = find.OrderByDescending(s => s.IdRoom);
                    break;

                case "Id":
                    find = find.OrderBy(s => s.IdRecord);                    
                    break;

                case "Id desc":
                    find = find.OrderByDescending(s => s.IdRecord);
                    break;

            }

            entr.Entrances = find.ToList();
            entr.Passes = _context.Passes.ToList();
            entr.Rooms = _context.Rooms.ToList();
            entr.Doors = _context.Doors.ToList();
            entr.Statuses = _context.AccessStatuses.ToList();
            return View(entr);
        }       
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ПОСЕТИТЕЛИ"

        //Отображение страницы
        public IActionResult Visitors(VisitorViewModel vis, string sort, string Search)
        {
            vis.ActivSelect = new SelectList(_context.ActivityStatuses, "IdActiv", "TitleActiv");
            vis.LongSelect = new SelectList(_context.LastingStatuses, "IdLong", "TitleLong");
            vis.LevelSelect = new SelectList(_context.AccessLevels, "IdLevel", "TitleLevel");

            ViewBag.VisSort = String.IsNullOrEmpty(sort) ? "Name dsc" : "Name";
            ViewBag.PassSort = sort == "Pass" ? "Pass dsc" : "Pass";
            var find = from s in _context.Visitors select s;

            if (!String.IsNullOrEmpty(Search))
            {
                find = find.Where(s => s.Fio.ToUpper().Contains(Search.ToUpper()));
            }

            switch (sort)
            {
                case "Name":
                    find = find.OrderBy(s => s.Fio);
                    break;

                case "Name dsc":
                    find = find.OrderByDescending(s => s.Fio);
                    break;

                case "Pass":
                    find = find.OrderBy(s => s.IdPass);
                    break;

                case "Pass dsc":
                    find = find.OrderByDescending(s => s.IdPass);
                    break;
            }

            vis.Visitors = find.ToList();
            vis.Passes = _context.Passes.ToList();
            vis.Lastings = _context.LastingStatuses.ToList();
            vis.Activities = _context.ActivityStatuses.ToList();
            vis.Levels = _context.AccessLevels.ToList();
            return View(vis);
        }

        //Редактирование записи
        [HttpPost]
        public IActionResult VisEdit(VisitorViewModel vis)
        {
            var edit = _context.Visitors.FirstOrDefault(x => x.Idvisitor == vis.Idvisitor);

            edit.Idvisitor = vis.Idvisitor;
            edit.Fio = vis.Fio;
            edit.MobilePhone = vis.MobilePhone;            
            edit.IdPass = vis.IdPass;            

            _context.SaveChanges();
            return RedirectToAction(nameof(Visitors));
        }
        public IActionResult VisEdit(int id)
        {
            var vis = new VisitorViewModel();           
            var edit = _context.Visitors.FirstOrDefault(x => x.Idvisitor == id);
            if (edit != null)
            {
                vis.Idvisitor = edit.Idvisitor;
                vis.Fio = edit.Fio;
                vis.MobilePhone = edit.MobilePhone;                
                vis.IdPass = edit.IdPass;
            }
            return Json(vis);
        }

        //Удаление записи
        public IActionResult DelVis(int id)
        {
            var vis = _context.Visitors.FirstOrDefault(x => x.Idvisitor == id);
            if (vis != null)
            {
                _context.Visitors.Remove(vis);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Visitors));
        }

        //Создание записи
        [HttpPost]
        public IActionResult CreateVisit(VisitorViewModel vis)
        {
            var create = new Visitor { Fio = vis.Fio, MobilePhone = vis.MobilePhone, IdPass = vis.IdPass };

            _context.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Visitors));
        }
        public IActionResult CreateVisit()
        {
            var vis = new VisitorViewModel();

            vis.Visitors = _context.Visitors.ToList();           
            vis.Passes = _context.Passes.ToList();
            return View(vis);

        }
        //Редактирование записи пропуска
        [HttpPost]
        public IActionResult PassEditVis(PassesViewModel pass)
        {
            var edit = _context.Passes.FirstOrDefault(x => x.IdPass == pass.IdPass);

            edit.IdPass = pass.IdPass;
            edit.IdActiv = pass.IdActiv;
            edit.IdLong = pass.IdLong;
            edit.IdLevel = pass.IdLevel;

            _context.SaveChanges();
            return RedirectToAction(nameof(Visitors));
        }
        public IActionResult PassEditVis(int id)
        {
            var pass = new PassesViewModel();
            var edit = _context.Passes.FirstOrDefault(x => x.IdPass == id);
            if (edit != null)
            {
                pass.IdPass = edit.IdPass;
                pass.IdActiv = edit.IdActiv;
                pass.IdLong = edit.IdLong;
                pass.IdLevel = edit.IdLevel;
            }

            return Json(pass);
        }

        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ПРОПУСКА"
        //Отображение страницы
        public IActionResult Passes(PassesViewModel pass)
        {
            pass.ActivSelect = new SelectList(_context.ActivityStatuses, "IdActiv", "TitleActiv");
            pass.LongSelect = new SelectList(_context.LastingStatuses, "IdLong", "TitleLong");
            pass.LevelSelect = new SelectList(_context.AccessLevels, "IdLevel", "TitleLevel");

            pass.Passes = _context.Passes.ToList();
            pass.Lastings = _context.LastingStatuses.ToList();
            pass.Activities = _context.ActivityStatuses.ToList();
            pass.Levels = _context.AccessLevels.ToList();

            return View(pass);
        }

        //Создание записи
        [HttpPost]
        public IActionResult CreatePass(PassesViewModel pass)
        {
            var create = new Pass { IdPass = pass.IdPass, IdLong = pass.IdLong, IdActiv = pass.IdActiv, IdLevel = pass.IdLevel };

            _context.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Passes));

        }
        public IActionResult CreatePass()
        {
            var pass = new PassesViewModel();
            SelectList last = new SelectList(_context.LastingStatuses, "IdLong", "TitleLong");
            ViewBag.Lasting = last;

            SelectList act = new SelectList(_context.ActivityStatuses, "IdActiv", "TitleActiv");
            ViewBag.Activ = act;

            SelectList lev = new SelectList(_context.AccessLevels, "IdLevel", "TitleLevel");
            ViewBag.Level = lev;

            pass.Activities = _context.ActivityStatuses.ToList();
            pass.Lastings = _context.LastingStatuses.ToList();
            pass.Levels = _context.AccessLevels.ToList();
            pass.Passes = _context.Passes.ToList();

            return View(pass);

        }
        //Редактирование записи
        [HttpPost]
        public IActionResult PassEdit(PassesViewModel pass)
        {
            var edit = _context.Passes.FirstOrDefault(x => x.IdPass == pass.IdPass);

            edit.IdPass = pass.IdPass;
            edit.IdActiv = pass.IdActiv;
            edit.IdLong = pass.IdLong;
            edit.IdLevel = pass.IdLevel;

            _context.SaveChanges();
            return RedirectToAction(nameof(Passes));
        }
        public IActionResult PassEdit(int id)
        {
            var pass = new PassesViewModel();
            var edit = _context.Passes.FirstOrDefault(x => x.IdPass == id);
            if(edit != null)
            {
                pass.IdPass = edit.IdPass;
                pass.IdActiv = edit.IdActiv;
                pass.IdLong = edit.IdLong;
                pass.IdLevel = edit.IdLevel;
            }

            return Json(pass);
        }
        //Удаление записи
        public IActionResult DelPass(int id)
        {
            var pass = _context.Passes.FirstOrDefault(x => x.IdPass == id);
            if (pass != null)
            {
                _context.Passes.Remove(pass);
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Passes));
        }

        #endregion

        #region ДЕЙСТВИЯ СО СТПРАВОЧНЫМИ ТАБЛИЦАМИ

        public IActionResult Dictionary(DictionaryViewModel dict)
        {
            dict.Levels = _context.AccessLevels.ToList();
            dict.Lastings = _context.LastingStatuses.ToList();
            dict.Statuses = _context.AccessStatuses.ToList();
            dict.Activities = _context.ActivityStatuses.ToList();

            return View(dict);
        }
        //Удаление записи таблицы "Уровни доступа"
        public IActionResult DelLevel(int id)
        {
            var data = _context.AccessLevels.FirstOrDefault(x => x.IdLevel == id);
            var room = _context.Rooms.FirstOrDefault(x => x.IdLevel == id);
            var pass = _context.Passes.FirstOrDefault(x => x.IdLevel == id);

            if (data != null)
            {
                _context.Passes.Remove(pass);
                _context.Rooms.Remove(room);
                _context.AccessLevels.Remove(data);

            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }

        //Редактирование записи таблицы "Уровни доступа"
        [HttpPost]
        public IActionResult LevelEdit(DictionaryViewModel acclev)
        {
            var edit = _context.AccessLevels.FirstOrDefault(x => x.IdLevel == acclev.IdLevel);

            edit.IdLevel = acclev.IdLevel;
            edit.TitleLevel = acclev.TitleLevel;

            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }
        public IActionResult LevelEdit(int id)
        {
            var viewmodel = new DictionaryViewModel();
            var view = _context.AccessLevels.FirstOrDefault(x => x.IdLevel == id);
            if (view != null)
            {
                viewmodel.IdLevel = view.IdLevel;
                viewmodel.TitleLevel = view.TitleLevel;

            }
            return Json(viewmodel);

        }
        //Создание записи таблицы "Уровни доступа"
        [HttpPost]
        public IActionResult CreateLevel(DictionaryViewModel lev)
        {
            var create = new AccessLevel { TitleLevel = lev.TitleLevel };

            _context.AccessLevels.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }
        public IActionResult CreateLevel()
        {
            return View();
        }

        //Удаление записи таблицы "Статус доступа"
        public IActionResult DelStatus(int id)
        {
            var data = _context.AccessStatuses.FirstOrDefault(x => x.IdStatus == id);
            var entr = _context.Entrances.FirstOrDefault(x => x.IdStatus == id);           

            if (data != null)
            {
                _context.Entrances.Remove(entr);
                _context.AccessStatuses.Remove(data);

            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }

        //Редактирование записи таблицы "Статус доступа"
        [HttpPost]
        public IActionResult StatusEdit(DictionaryViewModel accstat)
        {
            var edit = _context.AccessStatuses.FirstOrDefault(x => x.IdStatus == accstat.IdStatus);

            edit.IdStatus = accstat.IdStatus;
            edit.TitleStatus = accstat.TitleStatus;

            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }
        public IActionResult StatusEdit(int id)
        {
            var viewmodel = new DictionaryViewModel();
            var view = _context.AccessStatuses.FirstOrDefault(x => x.IdStatus == id);
            if (view != null)
            {
                viewmodel.IdStatus = view.IdStatus;
                viewmodel.TitleStatus = view.TitleStatus;

            }
            return Json(viewmodel);

        }
        //Создание записи таблицы "Статус доступа"
        [HttpPost]
        public IActionResult CreateStatus(DictionaryViewModel stat)
        {
            var create = new AccessStatus { TitleStatus = stat.TitleStatus };

            _context.AccessStatuses.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }
        public IActionResult CreateStatus()
        {
            return View();
        }

        //Удаление записи таблицы "Статус Активности"
        public IActionResult DelActiv(int id)
        {
            var data = _context.ActivityStatuses.FirstOrDefault(x => x.IdActiv == id);
            var pass = _context.Passes.FirstOrDefault(x => x.IdActiv == id);

            if (data != null)
            {
                _context.Passes.Remove(pass);
                _context.ActivityStatuses.Remove(data);

            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }

        //Редактирование записи таблицы "Статус Активности"
        [HttpPost]
        public IActionResult ActivEdit(DictionaryViewModel act)
        {
            var edit = _context.ActivityStatuses.FirstOrDefault(x => x.IdActiv == act.IdActiv);

            edit.IdActiv = act.IdActiv;
            edit.TitleActiv = act.TitleActiv;

            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }
        public IActionResult ActivEdit(int id)
        {
            var viewmodel = new DictionaryViewModel();
            var view = _context.ActivityStatuses.FirstOrDefault(x => x.IdActiv == id);
            if (view != null)
            {
                viewmodel.IdActiv = view.IdActiv;
                viewmodel.TitleActiv = view.TitleActiv;

            }
            return Json(viewmodel);

        }
        //Создание записи таблицы "Статус Активности"
        [HttpPost]
        public IActionResult CreateActiv(DictionaryViewModel act)
        {
            var create = new ActivityStatus { TitleActiv = act.TitleActiv };

            _context.ActivityStatuses.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }
        public IActionResult CreateActiv()
        {
            return View();
        }

        //Удаление записи таблицы "Статус длительности"
        public IActionResult DelLong(int id)
        {
            var data = _context.LastingStatuses.FirstOrDefault(x => x.IdLong == id);
            var pass = _context.Passes.FirstOrDefault(x => x.IdLong == id);

            if (data != null)
            {
                _context.Passes.Remove(pass);
                _context.LastingStatuses.Remove(data);

            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }

        //Редактирование записи таблицы "Статус длительности"
        [HttpPost]
        public IActionResult LongEdit(DictionaryViewModel last)
        {
            var edit = _context.LastingStatuses.FirstOrDefault(x => x.IdLong == last.IdLong);

            edit.IdLong = last.IdLong;
            edit.TitleLong = last.TitleLong;

            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }
        public IActionResult LongEdit(int id)
        {
            var viewmodel = new DictionaryViewModel();
            var view = _context.LastingStatuses.FirstOrDefault(x => x.IdLong == id);
            if (view != null)
            {
                viewmodel.IdLong = view.IdLong;
                viewmodel.TitleLong = view.TitleLong;

            }
            return Json(viewmodel);

        }
        //Создание записи таблицы "Статус длительности"
        [HttpPost]
        public IActionResult CreateLong(DictionaryViewModel last)
        {
            var create = new LastingStatus { TitleLong = last.TitleLong };

            _context.LastingStatuses.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }
        public IActionResult CreateLong()
        {
            return View();
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
