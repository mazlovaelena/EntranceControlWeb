using EntranceControlWeb.Models;
using EntranceControlWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public IActionResult Doors(DoorViewModel door, string sort, string Search, int? IdRoom, bool Hide)
        {
            door.RoomSelect = new SelectList(_context.Rooms, "IdRoom", "TitleRoom");
           
            ViewBag.RoomSort = String.IsNullOrEmpty(sort) ? "Room dsc" : "Room";
            ViewBag.DoorSort = String.IsNullOrEmpty(sort) ? "Door dsc" : " ";

            var find = from s in _context.Doors select s;

            if (!String.IsNullOrEmpty(Search))
            {
                find = find.Where(s => s.TitleDoor.ToUpper().Contains(Search.ToUpper()));
               
            }
            if(IdRoom != 0 && IdRoom != null)
            {
                find = find.Where(s => s.IdRoom == IdRoom);
            }

            switch (sort)
            {
                case "Room":
                    find = find.OrderBy(s => s.IdRooms.TitleRoom);
                    break;

                case "Room dsc":
                    find = find.OrderByDescending(s => s.IdRooms.TitleRoom);
                    break;

                default:
                    find = find.OrderBy(s => s.TitleDoor);
                    break;

                case "Door dsc":
                    find = find.OrderByDescending(s => s.TitleDoor);
                    break;
            }         
            
            if(Hide == true)
            {
                door.Doors = find.ToList();
                door.Rooms = _context.Rooms.ToList();

                return View(door);
            }

            door.Doors = find.Where(x=>x.Hidden == false).ToList();
            door.Rooms = _context.Rooms.ToList();


            if(door.Doors == null)
            {
                ViewBag.Message = "Результатов не найдено";
            }

            return View(door);
        }
        public IActionResult ClearDoor()
        {
            return RedirectToAction(nameof(Doors));
        }
        //Удаление записи
        public IActionResult DelDoor(int id)
        {
            var door = new DoorViewModel();
            var hide = _context.Doors.FirstOrDefault(x => x.IdDoor == id);
            if(door != null)
            {
                hide.Hidden = true;
            }            

            _context.SaveChanges();
            return RedirectToAction(nameof(Doors));

        }
        //Восстановление записи
        public IActionResult DoorShow(int id)
        {
            var door = new DoorViewModel();
            var hide = _context.Doors.FirstOrDefault(x => x.IdDoor == id);
            if (door != null)
            {
                hide.Hidden = false;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Doors));

        }
        //Редактирование записи
        [HttpPost]
        [Authorize]
        public IActionResult DoorEdit(DoorViewModel door)
        {
            var edit = _context.Doors.FirstOrDefault(x => x.IdDoor == door.IdDoor);

            edit.IdDoor = door.IdDoor;
            edit.TitleDoor = door.TitleDoor;
            edit.IdRoom = door.IdRoom;
            edit.Hidden = false;

            _context.SaveChanges();
            return RedirectToAction(nameof(Doors));
        }
        [Authorize]
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
        [Authorize]
        public IActionResult CreateDoor(DoorViewModel door)
        {
            var create = new Door { TitleDoor = door.TitleDoor, IdRoom = door.IdRoom, Hidden = false };

            _context.Doors.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Doors));

        }
        [Authorize]
        public IActionResult CreateDoor()
        {
            var door = new DoorViewModel();
            door.RoomSelect = new SelectList(_context.Rooms, "IdRoom", "TitleRoom");

            door.Doors = _context.Doors.ToList();
            door.Rooms = _context.Rooms.ToList();
            return View(door);
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ОТДЕЛЫ"
        //Отображение страницы
        [Authorize]
        public IActionResult Offices(OfficeViewModel off, string sort, string Search, bool Hide)
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
                    find = find.OrderBy(s => s.TitleOffice);
                    break;

                case "Off dsc":
                    find = find.OrderByDescending(s => s.TitleOffice);
                    break;
            }

            if(Hide == true)
            {
                off.Offices = find.ToList();

                return View(off);
            }

            off.Offices = find.Where(x=>x.Hidden == false).ToList();

            if(off.Offices == null)
            {
                ViewBag.Message = "Результатов не найдено";
            }

            return View(off);
        }
        //Удаление записи
        public IActionResult DelOffice(int id)
        {
            var off = new OfficeViewModel();
            var data = _context.Offices.FirstOrDefault(x => x.IdOffice == id);
           
            if (off != null)
            {
                data.Hidden = true;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Offices));

        }
        //Восстановление записи
        public IActionResult OfficeShow(int id)
        {
            var off = new OfficeViewModel();
            var data = _context.Offices.FirstOrDefault(x => x.IdOffice == id);

            if (off != null)
            {
                data.Hidden = false;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Offices));

        }
        //Редактирование записи
        [HttpPost]
        [Authorize]
        public IActionResult OfficeEdit(OfficeViewModel off)
        {
            var edit = _context.Offices.FirstOrDefault(x => x.IdOffice == off.IdOffice);

            edit.IdOffice = off.IdOffice;
            edit.TitleOffice = off.TitleOffice;
            edit.Hidden = false;

            _context.SaveChanges();
            return RedirectToAction(nameof(Offices));
        }
        [Authorize]
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
        [Authorize]
        public IActionResult CreateOffice(OfficeViewModel off)
        {
            var create = new Office { TitleOffice = off.TitleOffice, Hidden = false };

            _context.Offices.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Offices));
        }
        [Authorize]
        public IActionResult CreateOffice()
        {
            return View();
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ДОЛЖНОСТИ"
        //Отображение страницы
        [Authorize]
        public IActionResult Positions(PositionViewModel pos, string sort, string Search, bool Hide)
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
                    find = find.OrderBy(s => s.TitlePost);
                    break;

                case "Post dsc":
                    find = find.OrderByDescending(s => s.TitlePost);
                    break;
            }

            if(Hide == true)
            {
                pos.Positions = find.ToList();
                return View(pos);
            }

            pos.Positions = find.Where(x=>x.Hidden == false).ToList();

            if(pos.Positions == null)
            {
                ViewBag.Message = "Результатов не найдено";
            }

            return View(pos);
        }
        //Удаление записи
        public IActionResult DelPos(int id)
        {
            var data = _context.Positions.FirstOrDefault(x => x.IdPost == id);
            var pos = new PositionViewModel();
            if (pos != null)
            {
                data.Hidden = true;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Positions));

        }
        //Восстановление записи
        public IActionResult PosShow(int id)
        {
            var data = _context.Positions.FirstOrDefault(x => x.IdPost == id);
            var pos = new PositionViewModel();
            if (pos != null)
            {
                data.Hidden = false;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Positions));

        }
        //Редактирование записи
        [HttpPost]
        [Authorize]
        public IActionResult PosEdit(PositionViewModel pos)
        {
            var edit = _context.Positions.FirstOrDefault(x => x.IdPost == pos.IdPost);

            edit.IdPost = pos.IdPost;
            edit.TitlePost = pos.TitlePost;
            edit.Hidden = false;

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
        [Authorize]
        public IActionResult CreatePost(Position pos, int id)
        {
            var create = new Position { TitlePost = pos.TitlePost, Hidden = false };

            _context.Positions.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Positions));
        }
        [Authorize]
        public IActionResult CreatePost()
        {
            return View();
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ПОМЕЩЕНИЯ"

        //Отображение данных
        [Authorize]
        public IActionResult Rooms(RoomViewModel room, string sort, string Search, int? IdLevel, bool Hide)
        {
            room.LevelSelect = new SelectList(_context.AccessLevels, "IdLevel", "TitleLevel");

            ViewBag.RoomSort = sort == "Room" ? "Room dsc" : "Room";
            ViewBag.LevelSort = sort == "Level" ? "Level dsc" : "Level";
            var find = from s in _context.Rooms select s;

            if (!String.IsNullOrEmpty(Search))
            {
                find = find.Where(s => s.TitleRoom.ToUpper().Contains(Search.ToUpper()));
            }

            if (IdLevel != 0 && IdLevel != null)
            {
                find = find.Where(s => s.IdLevel == IdLevel);
            }

            switch (sort)
            {
                case "Room":
                    find = find.OrderBy(s => s.TitleRoom);
                    break;

                case "Room dsc":
                    find = find.OrderByDescending(s => s.TitleRoom);
                    break;

                case "Level":
                    find = find.OrderBy(s => s.IdLevel);
                    break;

                case "Level dsc":
                    find = find.OrderByDescending(s => s.IdLevel);
                    break;
            }

            if(Hide == true)
            {
                room.Rooms = find.ToList();
                room.Levels = _context.AccessLevels.ToList();
                return View(room);
            }

            room.Rooms = find.Where(x=>x.Hidden == false).ToList();
            room.Levels = _context.AccessLevels.ToList();

            if(room.Rooms == null)
            {
                ViewBag.Message = "Результатов не найдено";
            }

            return View(room);
        }

        public IActionResult ClearRoom()
        {
            return RedirectToAction(nameof(Rooms));
        }

        //Удаление записи
        public IActionResult DelRoom(int id)
        {
            var data = _context.Rooms.FirstOrDefault(x => x.IdRoom == id);
            var room = new RoomViewModel();

            if (room != null)
            {
                data.Hidden = true;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Rooms));

        }
        //Восстановление записи
        public IActionResult RoomShow(int id)
        {
            var data = _context.Rooms.FirstOrDefault(x => x.IdRoom == id);
            var room = new RoomViewModel();

            if (room != null)
            {
                data.Hidden = false;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Rooms));

        }
        //Редактирование записи
        [HttpPost]
        [Authorize]
        public IActionResult RoomEdit(RoomViewModel room)
        {
            var edit = _context.Rooms.FirstOrDefault(x => x.IdRoom == room.IdRoom);

            edit.IdRoom = room.IdRoom;
            edit.TitleRoom = room.TitleRoom;
            edit.IdLevel = room.IdLevel;
            edit.Hidden = false;

            _context.SaveChanges();
            return RedirectToAction(nameof(Rooms));
        }
        [Authorize]
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
        [Authorize]
        public IActionResult CreateRoom(RoomViewModel room)
        {
            var create = new Room { TitleRoom = room.TitleRoom, IdLevel = room.IdLevel, Hidden = false };

            _context.Rooms.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Rooms));

        }
        [Authorize]
        public IActionResult CreateRoom()
        {
            var room = new RoomViewModel();
            room.LevelSelect = new SelectList(_context.AccessLevels, "IdLevel", "TitleLevel");

            room.Rooms = _context.Rooms.ToList();
            room.Levels = _context.AccessLevels.ToList();
            return View(room);
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "РАСПРЕДЕЛЕНИЕ ПО ОТДЕЛАМ"

        //Отображение страницы
        [Authorize]
        public IActionResult SortByOff(SortingByOfficeViewModel sort, string order, string Search1, string Search2, int? IdStaff, int? IdPost, int? IdOffice, bool Hide)
        {
            sort.StaffSelect = new SelectList(_context.staff, "IdStaff", "Surname");
            sort.PostSelect = new SelectList(_context.Positions, "IdPost", "TitlePost");
            sort.OfficeSelect = new SelectList(_context.Offices, "IdOffice", "TitleOffice");

            ViewBag.SortStaff = order == "Staff" ? "Staff dsc" : "Staff";
            ViewBag.SortPost = order == "Post" ? "Post dsc" : "Post";
            ViewBag.SortOffice = order == "Office" ? "Office dsc" : "Office";         

            var find = from s in _context.SortingByOffices select s;

            if (!String.IsNullOrEmpty(Search1))
            {
                find = find.Where(s => s.TimeBegin.ToString().ToUpper().Contains(Search1.ToUpper()));
            }

            if (!String.IsNullOrEmpty(Search2))
            {
                find = find.Where(s => s.TimeEnd.ToString().ToUpper().Contains(Search2.ToUpper()));
            }

            if (IdStaff != 0 && IdStaff != null)
            {
                find = find.Where(s => s.IdStaff == IdStaff);
            }

            if(IdPost != 0 && IdPost != null)
            {
                find = find.Where(s => s.IdPost == IdPost);
            }

            if (IdOffice != 0 && IdOffice != null)
            {
                find = find.Where(s => s.IdOffice == IdOffice);
            }

            switch (order)
            {
                case "Staff":
                    find = find.OrderBy(s => s.IdStaffs.Surname);
                    break;

                case "Staff dsc":
                    find = find.OrderByDescending(s => s.IdStaffs.Surname);
                    break;

                case "Post":
                    find = find.OrderBy(s => s.IdPosts.TitlePost);
                    break;

                case "Post dsc":
                    find = find.OrderByDescending(s => s.IdPosts.TitlePost);
                    break;

                case "Office":
                    find = find.OrderBy(s => s.IdOffices.TitleOffice);
                    break;

                case "Office dsc":
                    find = find.OrderByDescending(s => s.IdOffices.TitleOffice);
                    break;
               
            }

            if(Hide == true)
            {
                sort.Sortings = find.ToList();
                sort.Staffs = _context.staff.ToList();
                sort.Positions = _context.Positions.ToList();
                sort.Offices = _context.Offices.ToList();

                return View(sort);
            }

            sort.Sortings = find.Where(x=>x.Hidden == false).ToList();
            sort.Staffs = _context.staff.ToList();
            sort.Positions = _context.Positions.ToList();
            sort.Offices = _context.Offices.ToList();

            if(sort.Sortings == null)
            {
                ViewBag.Message = "Результатов не найдено";
            }

            return View(sort);
        }

        public IActionResult ClearSort()
        {
            return RedirectToAction(nameof(SortByOff));
        }

        //Удаление записи
        public IActionResult DelSort(int id)
        {
            var data = _context.SortingByOffices.FirstOrDefault(x => x.IdItem == id);
            var sort = new SortingByOffice();

            if (sort != null)
            {
                data.Hidden = true;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(SortByOff));

        }
        //Восстановление записи
        public IActionResult SortShow(int id)
        {
            var data = _context.SortingByOffices.FirstOrDefault(x => x.IdItem == id);
            var sort = new SortingByOffice();

            if (sort != null)
            {
                data.Hidden = false;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(SortByOff));

        }
        //Редактирование записи
        [HttpPost]
        [Authorize]
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
            edit.Hidden = false;

            _context.SaveChanges();
            return RedirectToAction(nameof(SortByOff));
        }
        [Authorize]
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
        [Authorize]
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
                Hidden = false
            };

            _context.SortingByOffices.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(SortByOff));
        }
        [Authorize]
        public IActionResult CreateSort()
        {
            var sort = new SortingByOfficeViewModel();
            sort.StaffSelect = new SelectList(_context.staff, "IdStaff", "Surname");
            sort.PostSelect = new SelectList(_context.Positions, "IdPost", "TitlePost");
            sort.OfficeSelect = new SelectList(_context.Offices, "IdOffice", "TitleOffice");

            sort.Sortings = _context.SortingByOffices.ToList();
            sort.Staffs = _context.staff.ToList();
            sort.Positions = _context.Positions.ToList();
            sort.Offices = _context.Offices.ToList();

            return View(sort);
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "СОТРУДНИКИ"
        //Отображение данных
        [Authorize]
        public IActionResult Staff(StaffViewModel staff, string Search, bool Hide)
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

            if(Hide == true)
            {
                staff.Passes = _context.Passes.ToList();
                staff.Lastings = _context.LastingStatuses.ToList();
                staff.Activities = _context.ActivityStatuses.ToList();
                staff.Levels = _context.AccessLevels.ToList();
                staff.Staffs = find.ToList();

                return View(staff);
            }

            staff.Passes = _context.Passes.ToList();
            staff.Lastings = _context.LastingStatuses.ToList();
            staff.Activities = _context.ActivityStatuses.ToList();
            staff.Levels = _context.AccessLevels.ToList();
            staff.Staffs = find.Where(x=>x.Hidden == false).ToList(); 
            
            if(staff.Staffs == null)
            {
                ViewBag.Message = "Результатов не найдено";
            }

            return View(staff);
        }

        //Редактирование данных
        [HttpPost]
        [Authorize]
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
            edit.Hidden = false;

            _context.SaveChanges();

            return RedirectToAction(nameof(Staff));
        }
        [Authorize]
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
        [Authorize]
        public IActionResult PassEditStaff(PassesViewModel pass)
        {
            var edit = _context.Passes.FirstOrDefault(x => x.IdPass == pass.IdPass);

            edit.IdPass = pass.IdPass;
            edit.IdActiv = pass.IdActiv;
            edit.IdLong = pass.IdLong;
            edit.IdLevel = pass.IdLevel;
            edit.Hidden = false;

            _context.SaveChanges();
            return RedirectToAction(nameof(Staff));
        }
        [Authorize]
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
            var staff = new StaffViewModel();

            if (staff != null)
            {
                data.Hidden = true;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Staff));

        }
        //Восстановление записи
        public IActionResult StaffShow(int id)
        {
            var data = _context.staff.FirstOrDefault(x => x.IdStaff == id);
            var staff = new StaffViewModel();

            if (staff != null)
            {
                data.Hidden = false;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Staff));

        }
        //Создание записи
        [HttpPost]
        [Authorize]
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
                Hidden = false

            };

            _context.staff.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Staff));
        }
        [Authorize]
        public IActionResult CreateStaff()
        {
            var staff = new StaffViewModel();

            staff.Staffs = _context.staff.ToList();           

            return View(staff);
        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ДОСТУП"
        //Отображение данных
        [Authorize]
        public IActionResult Entrance(EntranceViewModel entr, string sort, DateTime? Search1, DateTime? Search2, int? IdDoor, int? IdPass, int? IdRoom, int? IdStatus)
        {
            entr.DoorSelect = new SelectList(_context.Doors, "IdDoor", "TitleDoor");
            entr.PassSelect = new SelectList(_context.Passes, "IdPass", "IdPass");
            entr.RoomSelect = new SelectList(_context.Rooms, "IdRoom", "TitleRoom");
            entr.StatSelect = new SelectList(_context.AccessStatuses, "IdStatus", "TitleStatus");

            ViewBag.DateEntrSort = sort == "DateEntr" ? "DateEntr dsc" : "DateEntr";
            ViewBag.DoorSort = String.IsNullOrEmpty(sort) ? "Door desc" : "Door";
            ViewBag.RoomSort = String.IsNullOrEmpty(sort) ? "Room desc" : "Room";            

            var find = from s in _context.Entrances select s;

            if (Search1 != null)
            {
                find = find.Where(s => s.DateEntr > Search1);
            }
            if (Search2 != null)
            {
                find = find.Where(s => s.DateExit < Search2);
            }       

            if (IdRoom != 0 && IdRoom != null)
            {
                find = find.Where(s => s.IdRoom == IdRoom);
            }
            if (IdPass != 0 && IdPass != null)
            {
                find = find.Where(s => s.IdPass == IdPass);
            }
            if (IdDoor != 0 && IdDoor != null)
            {
                find = find.Where(s => s.IdDoor == IdDoor);
            }
            if (IdStatus != 0 && IdStatus != null)
            {
                find = find.Where(s => s.IdStatus == IdStatus);
            }

            switch (sort)
            {
                case "DateEntr":
                    find = find.OrderBy(s => s.DateEntr);
                    break;

                case "DateEntr dsc":
                    find = find.OrderByDescending(s => s.DateEntr);
                    break;

                case "Door desc":
                    find = find.OrderByDescending(s => s.IdDoors.TitleDoor);
                    break;

                case "Door":
                    find = find.OrderBy(s => s.IdDoors.TitleDoor);
                    break;

                case "Room":
                    find = find.OrderBy(s => s.IdRooms.TitleRoom);
                    break;

                case "Room desc":
                    find = find.OrderByDescending(s => s.IdRooms.TitleRoom);
                    break;
            }

            entr.Entrances = find.ToList();
            entr.Passes = _context.Passes.ToList();               
            entr.Rooms = _context.Rooms.ToList();
            entr.Doors = _context.Doors.ToList();
            entr.Statuses = _context.AccessStatuses.ToList();

            if (entr.Entrances == null)
            {
                ViewBag.Message = "Результатов не найдено";
            }

            return View(entr);
        }
        [Authorize]
        public IActionResult ClearEntr()
        {
            return RedirectToAction(nameof(Entrance));
        }
        [Authorize]
        public IActionResult ViewStaff(int id)
        {
            var view = new EntranceViewModel();
            var db = _context.staff.FirstOrDefault(x => x.IdPass == id);           
            if(db != null)
            {
                view.IdPass = db.IdPass;
                view.Surname = db.Surname;
                view.Name = db.Name;
                view.LastName = db.LastName;
                view.Image = db.Image;
            }
            
            return Json(view);
                
        }
        [Authorize]
        public IActionResult ViewVisit (int id)
        {
            var view = new EntranceViewModel();          
            var vis = _context.Visitors.FirstOrDefault(x => x.IdPass == id);            
            if (vis != null)
            {
                view.IdPassVis = vis.IdPass;
                view.SurnameVis = vis.Surname;
                view.NameVis = vis.Name;
                view.LastNameVis = vis.LastName;
            }
            return Json(view);

        }
        #endregion

        #region ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ПОСЕТИТЕЛИ"

        //Отображение страницы
        [Authorize]
        public IActionResult Visitors(VisitorViewModel vis, string sort, string Search, bool Hide)
        {
            vis.ActivSelect = new SelectList(_context.ActivityStatuses, "IdActiv", "TitleActiv");
            vis.LongSelect = new SelectList(_context.LastingStatuses, "IdLong", "TitleLong");
            vis.LevelSelect = new SelectList(_context.AccessLevels, "IdLevel", "TitleLevel");

            ViewBag.PassSort = sort == "Pass" ? "Pass dsc" : "Pass";
            var find = from s in _context.Visitors select s;

            if (!String.IsNullOrEmpty(Search))
            {
                find = find.Where(s => s.Surname.ToUpper().Contains(Search.ToUpper())
                                       || s.Name.ToUpper().Contains(Search.ToUpper())
                                       || s.LastName.ToUpper().Contains(Search.ToUpper()));
            }

            switch (sort)
            {
                case "Pass":
                    find = find.OrderBy(s => s.IdPass);
                    break;

                case "Pass dsc":
                    find = find.OrderByDescending(s => s.IdPass);
                    break;
            }

            if(Hide == true)
            {
                vis.Visitors = find.ToList();
                vis.Passes = _context.Passes.ToList();
                vis.Lastings = _context.LastingStatuses.ToList();
                vis.Activities = _context.ActivityStatuses.ToList();
                vis.Levels = _context.AccessLevels.ToList();

                return View(vis);
            }

            vis.Visitors = find.Where(x=>x.Hidden == false).ToList();
            vis.Passes = _context.Passes.ToList();
            vis.Lastings = _context.LastingStatuses.ToList();
            vis.Activities = _context.ActivityStatuses.ToList();
            vis.Levels = _context.AccessLevels.ToList();

            if(vis.Visitors == null)
            {
                ViewBag.Message = "Результатов не найдено";
            }
            return View(vis);
        }

        //Редактирование записи
        [HttpPost]
        [Authorize]
        public IActionResult VisEdit(VisitorViewModel vis)
        {
            var edit = _context.Visitors.FirstOrDefault(x => x.Idvisitor == vis.Idvisitor);

            edit.Idvisitor = vis.Idvisitor;
            edit.Surname = vis.Surname;
            edit.Name = vis.Name;
            edit.LastName = vis.LastName;
            edit.MobilePhone = vis.MobilePhone;            
            edit.IdPass = vis.IdPass;
            edit.Hidden = false;

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
                vis.Surname = edit.Surname;
                vis.Name = edit.Name;
                vis.LastName = edit.LastName;
                vis.MobilePhone = edit.MobilePhone;                
                vis.IdPass = edit.IdPass;
            }
            return Json(vis);
        }

        //Удаление записи
        public IActionResult DelVis(int id)
        {
            var vis = _context.Visitors.FirstOrDefault(x => x.Idvisitor == id);
            var view = new VisitorViewModel();
            if (view != null)
            {
                vis.Hidden = true;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Visitors));
        }
        //Восстановление записи
        public IActionResult VisShow(int id)
        {
            var vis = _context.Visitors.FirstOrDefault(x => x.Idvisitor == id);
            var view = new VisitorViewModel();
            if (view != null)
            {
                vis.Hidden = false;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Visitors));
        }
        //Создание записи
        [HttpPost]
        [Authorize]
        public IActionResult CreateVisit(VisitorViewModel vis)
        {
            var create = new Visitor { Surname = vis.Surname, Name = vis.Name, LastName = vis.LastName, MobilePhone = vis.MobilePhone, IdPass = vis.IdPass, Hidden = false };

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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public IActionResult Passes(PassesViewModel pass, string Search, int? IdActiv, int? IdLong, int? IdLevel, bool Hide)
        {
            var find = from s in _context.Passes select s;

            if (!String.IsNullOrEmpty(Search))
            {
                find = find.Where(s => s.IdPass.ToString().ToUpper().Contains(Search.ToUpper()));
            }

            if (IdActiv != 0 && IdActiv != null)
            {
                find = find.Where(s => s.IdActiv == IdActiv);
            }

            if (IdLong != 0 && IdLong != null)
            {
                find = find.Where(s => s.IdLong == IdLong);
            }

            if (IdLevel != 0 && IdLevel != null)
            {
                find = find.Where(s => s.IdLevel == IdLevel);
            }

            pass.ActivSelect = new SelectList(_context.ActivityStatuses, "IdActiv", "TitleActiv");
            pass.LongSelect = new SelectList(_context.LastingStatuses, "IdLong", "TitleLong");
            pass.LevelSelect = new SelectList(_context.AccessLevels, "IdLevel", "TitleLevel");

            if(Hide == true)
            {
                pass.Passes = find.ToList();
                pass.Lastings = _context.LastingStatuses.ToList();
                pass.Activities = _context.ActivityStatuses.ToList();
                pass.Levels = _context.AccessLevels.ToList();

                return View(pass);
            }

            pass.Passes = find.Where(x=>x.Hidden == false).ToList();
            pass.Lastings = _context.LastingStatuses.ToList();
            pass.Activities = _context.ActivityStatuses.ToList();
            pass.Levels = _context.AccessLevels.ToList();

            if(pass.Passes == null)
            {
                ViewBag.Message = "Результатов не найдено";
            }

            return View(pass);
        }

        public IActionResult ClearPass()
        {
            return RedirectToAction(nameof(Passes));
        }

        //Создание записи
        [HttpPost]
        [Authorize]
        public IActionResult CreatePass(PassesViewModel pass)
        {
            var create = new Pass { IdPass = pass.IdPass, IdLong = pass.IdLong, IdActiv = pass.IdActiv, IdLevel = pass.IdLevel, Hidden = false };

            _context.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Passes));

        }
        [Authorize]
        public IActionResult CreatePass()
        {
            var pass = new PassesViewModel();
            pass.ActivSelect = new SelectList(_context.ActivityStatuses, "IdActiv", "TitleActiv");
            pass.LongSelect = new SelectList(_context.LastingStatuses, "IdLong", "TitleLong");
            pass.LevelSelect = new SelectList(_context.AccessLevels, "IdLevel", "TitleLevel");

            pass.Activities = _context.ActivityStatuses.ToList();
            pass.Lastings = _context.LastingStatuses.ToList();
            pass.Levels = _context.AccessLevels.ToList();
            pass.Passes = _context.Passes.ToList();

            return View(pass);

        }
        //Редактирование записи
        [HttpPost]
        [Authorize]
        public IActionResult PassEdit(PassesViewModel pass)
        {
            var edit = _context.Passes.FirstOrDefault(x => x.IdPass == pass.IdPass);

            edit.IdPass = pass.IdPass;
            edit.IdActiv = pass.IdActiv;
            edit.IdLong = pass.IdLong;
            edit.IdLevel = pass.IdLevel;
            edit.Hidden = false;

            _context.SaveChanges();
            return RedirectToAction(nameof(Passes));
        }
        [Authorize]
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
            var data = new PassesViewModel();

            if (data != null)
            {
                pass.Hidden = true;
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Passes));
        }
        //Восстановление записи
        public IActionResult PassShow(int id)
        {
            var pass = _context.Passes.FirstOrDefault(x => x.IdPass == id);
            var data = new PassesViewModel();

            if (data != null)
            {
                pass.Hidden = false;
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Passes));
        }
        #endregion

        #region ДЕЙСТВИЯ СО СПРАВОЧНЫМИ ТАБЛИЦАМИ
        [Authorize]
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
            var lev = new DictionaryViewModel();

            if (lev != null)
            {
                data.Hidden = true;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }

        //Редактирование записи таблицы "Уровни доступа"
        [HttpPost]
        [Authorize]
        public IActionResult LevelEdit(DictionaryViewModel acclev)
        {
            var edit = _context.AccessLevels.FirstOrDefault(x => x.IdLevel == acclev.IdLevel);

            edit.IdLevel = acclev.IdLevel;
            edit.TitleLevel = acclev.TitleLevel;
            edit.Hidden = false;

            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }
        [Authorize]
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
        [Authorize]
        public IActionResult CreateLevel(DictionaryViewModel lev)
        {
            var create = new AccessLevel { TitleLevel = lev.TitleLevel, Hidden = false };

            _context.AccessLevels.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }
        [Authorize]
        public IActionResult CreateLevel()
        {
            return View();
        }

        //Удаление записи таблицы "Статус доступа"
        public IActionResult DelStatus(int id)
        {
            var data = _context.AccessStatuses.FirstOrDefault(x => x.IdStatus == id);
            var stat = new DictionaryViewModel();          

            if (stat != null)
            {
                data.Hidden = true;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }

        //Редактирование записи таблицы "Статус доступа"
        [HttpPost]
        [Authorize]
        public IActionResult StatusEdit(DictionaryViewModel accstat)
        {
            var edit = _context.AccessStatuses.FirstOrDefault(x => x.IdStatus == accstat.IdStatus);

            edit.IdStatus = accstat.IdStatus;
            edit.TitleStatus = accstat.TitleStatus;
            edit.Hidden = false;

            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }
        [Authorize]
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
        [Authorize]
        public IActionResult CreateStatus(DictionaryViewModel stat)
        {
            var create = new AccessStatus { TitleStatus = stat.TitleStatus, Hidden = false };

            _context.AccessStatuses.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }
        [Authorize]
        public IActionResult CreateStatus()
        {
            return View();
        }

        //Удаление записи таблицы "Статус Активности"
        public IActionResult DelActiv(int id)
        {
            var data = _context.ActivityStatuses.FirstOrDefault(x => x.IdActiv == id);
            var act = new DictionaryViewModel();

            if (act != null)
            {
                data.Hidden = true;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }

        //Редактирование записи таблицы "Статус Активности"
        [HttpPost]
        [Authorize]
        public IActionResult ActivEdit(DictionaryViewModel act)
        {
            var edit = _context.ActivityStatuses.FirstOrDefault(x => x.IdActiv == act.IdActiv);

            edit.IdActiv = act.IdActiv;
            edit.TitleActiv = act.TitleActiv;
            edit.Hidden = false;

            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }
        [Authorize]
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
        [Authorize]
        public IActionResult CreateActiv(DictionaryViewModel act)
        {
            var create = new ActivityStatus { TitleActiv = act.TitleActiv, Hidden = false };

            _context.ActivityStatuses.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }
        [Authorize]
        public IActionResult CreateActiv()
        {
            return View();
        }

        //Удаление записи таблицы "Статус длительности"
        public IActionResult DelLong(int id)
        {
            var data = _context.LastingStatuses.FirstOrDefault(x => x.IdLong == id);
            var lon = new DictionaryViewModel();

            if (lon != null)
            {
                data.Hidden = false;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }

        //Редактирование записи таблицы "Статус длительности"
        [HttpPost]
        [Authorize]
        public IActionResult LongEdit(DictionaryViewModel last)
        {
            var edit = _context.LastingStatuses.FirstOrDefault(x => x.IdLong == last.IdLong);

            edit.IdLong = last.IdLong;
            edit.TitleLong = last.TitleLong;
            edit.Hidden = false;

            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }
        [Authorize]
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
        [Authorize]
        public IActionResult CreateLong(DictionaryViewModel last)
        {
            var create = new LastingStatus { TitleLong = last.TitleLong, Hidden = false };

            _context.LastingStatuses.Add(create);
            _context.SaveChanges();
            return RedirectToAction(nameof(Dictionary));
        }
        [Authorize]
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
