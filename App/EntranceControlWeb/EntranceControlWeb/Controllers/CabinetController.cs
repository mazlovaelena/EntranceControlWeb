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
        public IActionResult SysAdminCab()
        {
            return View();
        }
        public IActionResult Users(AuthorizeViewModel auth)
        {
            auth.Authorizes = _context.Authorizes.ToList();
            auth.Users = _context.Users.ToList();

            return View(auth);
        }
        public IActionResult Chart(EntranceViewModel model)
        {
            model.Entrances = _context.Entrances.ToList();

            for(int i = 1; i < 32; i++)
            {
                int date = _context.Entrances.ToList().Count(x => x.DateEntr.Day.ToString().Contains((char)i));
                model.Months = new int[] { date };
                continue;
            }
            
            return View(model);
        }
    }
}
