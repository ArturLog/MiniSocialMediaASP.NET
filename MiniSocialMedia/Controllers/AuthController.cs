using Microsoft.AspNetCore.Mvc;
using MiniSocialMedia.Models;

namespace MiniSocialMedia.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        [Route("Login/{login}")]
        public IActionResult Login(string login)
        {
            if (login == "admin")
            {
                HttpContext.Session.SetString("LoggedInUser", login);
                return RedirectToAction("List", "User");
            }
            var user = UserRepository.GetUserByLogin(login);
            if (user != null)
            {
                HttpContext.Session.SetString("LoggedInUser", login);
                return RedirectToAction("Index", "Friends");
            }
            return NotFound("User not exist.");
        }
        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("LoggedInUser");
            return RedirectToAction("Index", "Home");
        }
    }
}
