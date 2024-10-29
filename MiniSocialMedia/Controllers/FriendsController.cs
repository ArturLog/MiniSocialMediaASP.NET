using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSocialMedia.Data;
using MiniSocialMedia.Models;

namespace MiniSocialMedia.Controllers
{
    public class FriendsController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // GET: FriendsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FriendsController/List
        public ActionResult List()
        {
            return View(); //json z friendsami
        }

        // POST: FriendsController/Add/login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(string login)
        {
            try
            {
                _context.Users.Add(new User(login));
                _context.SaveChanges();

                return RedirectToAction(nameof(List)); // json
            }
            catch
            {
                return RedirectToAction(nameof(List));
            }
        }

        // POST: FriendsController/Del/login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string login, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index)); // json
            }
            catch
            {
                return View();
            }
        }
        // GET: FriendsController/Export
        public ActionResult Export()
        {
            return View();
        }

        // GET: FriendsController/Import
        public ActionResult Import()
        {
            return View();
        }
    }
}
