using Microsoft.AspNetCore.Mvc;

namespace MiniSocialMedia.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        [Route("Login/{login}")]
        public IActionResult Login(string login)
        {
            var user = UserRepository.GetUserByLogin(login);
            if (user != null)
            {
                HttpContext.Session.SetString("LoggedInUser", login);
                return RedirectToAction("List", "Friends");
            }
            return NotFound("Użytkownik nie istnieje.");
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
