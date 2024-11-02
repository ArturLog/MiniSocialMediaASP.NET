using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
                HttpContext.Response.Cookies.Append("LoggedInUser", login);
                return RedirectToAction("List", "User");
            }
            var user = UserRepository.GetUserByLogin(login);
            if (user != null)
            {
                HttpContext.Response.Cookies.Append("LoggedInUser", login);
                return RedirectToAction("Index", "Friends");
            }
            return NotFound("User not exist.");
        }
        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("LoggedInUser");
            return RedirectToAction("Index", "Home");
        }
    }
}
