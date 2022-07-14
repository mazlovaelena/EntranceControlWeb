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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        public IActionResult Doors()
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
