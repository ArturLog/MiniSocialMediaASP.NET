using System;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniSocialMedia.Data;
using MiniSocialMedia.Models;

namespace MiniSocialMedia.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("Login/{login}")]
        public IActionResult Login()
        {
            return RedirectToAction(nameof(FriendsController.List));

        }
        public IActionResult Logout()
        {
            return RedirectToAction(nameof(HomeController.Index));
        }
        public IActionResult List()
        {
            var allUsers = _context.Users.ToList();
            return View(allUsers);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult AddUser(string login)
        {
            try
            {
                _context.Users.Add(new User
                {
                    Uuid = Guid.NewGuid(),
                    Login = login,
                    CreatedAt = DateTime.Now
                });
                _context.SaveChanges();

                return RedirectToAction(nameof(List));
            }
            catch
            {
                return RedirectToAction(nameof(List));
            }

        }

        [HttpGet]
        [Route("User/Del/{login}")]
        public IActionResult Del(string login)
        {
            try
            {
                var userInDb = _context.Users.FirstOrDefault(u => u.Login == login);
                if (userInDb is not null)
                {
                    _context.Users.Remove(userInDb);
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(List));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(List));
            }
        }

        [HttpPost]
        public IActionResult DelPost(string login)
        {
            try
            {
                var userInDb = _context.Users.FirstOrDefault(u => u.Login == login);
                if (userInDb is not null)
                {
                    _context.Users.Remove(userInDb);
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(List));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(List));
            }
        }
        public IActionResult Init()
        {
            for (int i = 0; i < 6; i++)
            {
                _context.Users.Add(new User
                {
                    Uuid = Guid.NewGuid(),
                    Login = RandomString(6),
                    CreatedAt = DateTime.Now
                });
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(List));
        }
        [NonAction]
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
