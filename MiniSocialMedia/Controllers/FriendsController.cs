using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSocialMedia.Data;
using MiniSocialMedia.Models;
using System.Linq;
using System.Text.Json;

namespace MiniSocialMedia.Controllers
{
    public class FriendsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            if (loggedInUser == null) return NotFound("Uzytkownik niezalogowany");

            var user = UserRepository.GetUserByLogin(loggedInUser);
            if (user == null) return NotFound("Zalogowany użytkownik nie istnieje.");

            return View(user);
        }
        [HttpGet]
        public ActionResult List()
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            if (loggedInUser == null) return NotFound("Uzytkownik niezalogowany");

            var user = UserRepository.GetUserByLogin(loggedInUser);
            if (user == null) return NotFound("Zalogowany użytkownik nie istnieje.");

            return Json(user.Friends);
        }

        [HttpGet]
        public ActionResult Add(string login)
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            if (loggedInUser == null) return NotFound("Uzytkownik niezalogowany");

            var user = UserRepository.GetUserByLogin(loggedInUser);
            var friend = UserRepository.GetUserByLogin(login);

            if (user == null || friend == null || user.Friends.Contains(new User(login)))
            {
                return Json(false);
            }

            user.Friends.Add(new User(login));
            return Json(true);
        }
        [HttpGet]
        public IActionResult Del(string login)
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            if (loggedInUser == null) return NotFound("Uzytkownik niezalogowany");

            var user = UserRepository.GetUserByLogin(loggedInUser);
            if (user == null || !user.Friends.Contains(new User(login)))
            {
                return Json(false);
            }

            user.Friends.Remove(new User(login));
            return Json(true);
        }
        [HttpGet]
        public IActionResult Export()
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            if (loggedInUser == null) return NotFound("Uzytkownik niezalogowany");

            var user = UserRepository.GetUserByLogin(loggedInUser);
            if (user == null) return NotFound("Zalogowany użytkownik nie istnieje.");

            var friendListJson = JsonSerializer.Serialize(user.Friends);
            var fileName = $"{user.Login}_friends.txt";
            var fileBytes = System.Text.Encoding.UTF8.GetBytes(friendListJson);

            return File(fileBytes, "text/plain", fileName);
        }

        [HttpPost]
        public IActionResult Import(IFormFile file)
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            if (loggedInUser == null) return NotFound("Uzytkownik niezalogowany");

            var user = UserRepository.GetUserByLogin(loggedInUser);
            if (user == null) return NotFound("Zalogowany użytkownik nie istnieje.");

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var content = reader.ReadToEnd();
                //user.Friends = JsonSerializer.Deserialize<List<string>>(content) ?? new List<string>();
            }

            return RedirectToAction("List");
        }
    }
}
