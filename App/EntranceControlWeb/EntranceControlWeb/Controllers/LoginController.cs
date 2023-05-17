using Microsoft.AspNetCore.Mvc;
using EntranceControlWeb.Models;
using EntranceControlWeb.Data;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntranceControlWeb.Controllers
{
    public class LoginController : Controller
    {
        private EntranceControlWebContext _context;
        public LoginController(EntranceControlWebContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> HomePage(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Поля заполнены некорректно";
                return View(model);
            }

            var user = _context.Users.FirstOrDefault(x => x.Email == model.Email);
            if (user == null)
            {
                ViewBag.Message = "Пользователь не зарегистрирован в системе";
                return View(model);
            }

            var pass = _context.Users.FirstOrDefault(x => x.Password == model.Password);
            if (pass==null)
            {
                ViewBag.Message = "Неверный пароль";
                return View(model);
            }

            model.Users = _context.Users.ToList();

            await Authenticate(user);

            if (_context.Users.Any(x => x.IdUser == user.IdUser))
            {
                var DateTimeAuth = new Authorize { DateAuth = DateTime.Now, IdUser = user.IdUser };

                _context.Authorizes.Add(DateTimeAuth);
                _context.SaveChanges();
            }
            return RedirectToAction("Entrance", "Home");

        }
        public IActionResult HomePage()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(HomePage));
        }

        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim("IdUser", user.IdUser.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.UserRole.ToString())
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
