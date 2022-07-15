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

        //***ДЕЙСТВИЯ С ТАБЛИЦЕЙ "ТУРНИКЕТЫ"***
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
            if(data != null)
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
            edit.IdRooms.IdRoom = door.IdRoom;
            edit.IdRooms.TitleRoom = door.TitleRoom;

            _context.SaveChanges();
            return RedirectToAction(nameof(Doors));
        }

        public IActionResult DoorEdit(DoorViewModel door, int id)
        {
            var view = _context.Doors.FirstOrDefault(x => x.IdDoor == id);
            if(id != 0)
            {
                var edit = _context.Doors.FirstOrDefault(x => x.IdDoor == id);
                door.IdDoor = edit.IdDoor;
                door.TitleDoor = edit.TitleDoor;
                door.IdRoom = edit.IdRooms.IdRoom;
                door.TitleRoom = edit.IdRooms.TitleRoom;
                
            }
            return View(door);
        }



        public IActionResult StaffEdit()
        {
            return View();
        }

        public IActionResult Staff()
        {
            return View();
        }

        public IActionResult Positions()
        {
            return View();
        }

        public IActionResult Offices()
        {
            return View();
        }

        public IActionResult SortByOff()
        {
            return View();
        }

        public IActionResult SortEdit()
        {
            return View();
        }
        public IActionResult Entrance()
        {
            return View();
        }
        public IActionResult Rooms()
        {
            return View();
        }

        

        public IActionResult Levels()
        {
            return View();
        }

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
