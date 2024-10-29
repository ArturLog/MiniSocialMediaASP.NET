using System;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniSocialMedia.Data;
using MiniSocialMedia.Models;

namespace MiniSocialMedia.Controllers
{
    public class UserController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public IActionResult List()
        {
            var allUsers = _context.Users.ToList();
            return View(allUsers);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPost(string login)
        {
            if (!string.IsNullOrWhiteSpace(login) && _context.Users.FirstOrDefault(u=> u.Login == login) == null)
            {
                _context.Users.Add(new User(login));
                _context.SaveChanges();
                return RedirectToAction(nameof(List));
            }
            ModelState.AddModelError("", "Login użytkownika jest wymagany i musi być unikalny.");
            return RedirectToAction(nameof(Add));

        }

        [HttpGet]
        [Route("User/Del/{login}")]
        public IActionResult Del(string login)
        {
            var userInDb = _context.Users.FirstOrDefault(u => u.Login == login);
            if (userInDb is not null)
            {
                _context.Users.Remove(userInDb);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(List));
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
                _context.Users.Add(new User(RandomString(6)));
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
