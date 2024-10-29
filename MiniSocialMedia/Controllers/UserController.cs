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
        // GET: UserController/List
        public IActionResult List()
        {
            var allUsers = _context.Users.ToList();
            return View(allUsers);
        }

        // GET: UserController/Add
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

        [HttpPost]
        public IActionResult Del(string login)
        {
            try
            {
                var userInDb = _context.Users.FirstOrDefault(u => u.Login == login);
                _context.Users.Remove(userInDb);
                _context.SaveChanges();
                return RedirectToAction(nameof(List));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(List));
            }

        }
    }
}
