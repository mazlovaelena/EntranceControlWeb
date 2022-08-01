using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using EntranceControlWeb.Models;

namespace EntranceControlWeb.Controllers
{
    public class CabinetController : Controller
    {
        private EntranceControlWebContext _context;

        public CabinetController (EntranceControlWebContext context)
        {
            
            _context = context;
        }

        public IActionResult UserCab()
        {
            return View();
        }
        public IActionResult AdminCab()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Chart()
        {
            return View();
        }
    }
}
