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
using System.Security.Cryptography;
using System.Text;


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
        [ValidateAntiForgeryToken]        
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
          
            var password = HashSHA512(model.Password);
            var match = password == user.Password;

            if(match)
            {
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

            ViewBag.Message = "Неверный пароль";
            return View(model);

        }
        public IActionResult HomePage()
        {
            return View();
        }
        //О программе
        public IActionResult About()
        {
            return View();
        }

        // Функция хеширования пароля SHA512
        private string HashSHA512(string str)
        {
            using (var sha512 = SHA512.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(str);
                var hashBytes = sha512.ComputeHash(bytes);
                var hashString = Convert.ToBase64String(hashBytes);

                return hashString;
            }
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
