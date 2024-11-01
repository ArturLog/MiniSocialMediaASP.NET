using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSocialMedia.Data;
using MiniSocialMedia.Models;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MiniSocialMedia.Controllers
{
    public class UserAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var loggedInUser = context.HttpContext.Session.GetString("LoggedInUser");
            if (loggedInUser == null)
            {
                context.Result = new ContentResult
                {
                    Content = "Access prohibited. Only logged in users have access to this action.",
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }

    public class UserActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var loggedInUser = context.HttpContext.Session.GetString("LoggedInUser");
            var user = UserRepository.GetUserByLogin(loggedInUser);
            if (user == null)
            {
                context.Result = new ContentResult
                {
                    Content = "Logged user is not exist",
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }
    }

    [ServiceFilter(typeof(UserAuthorizationFilter))]
    [ServiceFilter(typeof(UserActionFilter))]
    public class FriendsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            var user = UserRepository.GetUserByLogin(loggedInUser);

            return View(user);
        }

        [HttpGet]
        public ActionResult List()
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            var user = UserRepository.GetUserByLogin(loggedInUser);

            return Json(user.Friends);
        }

        [HttpGet]
        public ActionResult Add(string login)
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            var user = UserRepository.GetUserByLogin(loggedInUser);
            var friend = UserRepository.GetUserByLogin(login);

            if (user == null || friend == null || user.Friends.Contains(friend) || user == friend)
            {
                return Json(false);
            }

            user.Friends.Add(friend);
            return Json(true);
        }

        [HttpPost]
        [HttpGet]
        public IActionResult Del(string login)
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            var user = UserRepository.GetUserByLogin(loggedInUser);
            if (user == null || !user.Friends.Any(friend => friend.Login == login))
            {
                return Json(false);
            }

            user.Friends.RemoveAll(friend => friend.Login == login);
            return Json(true);
        }

        [HttpGet]
        public IActionResult Export()
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            var user = UserRepository.GetUserByLogin(loggedInUser);

            var friendListJson = JsonSerializer.Serialize(user.Friends);
            var fileName = $"{user.Login}_friends.txt";
            var fileBytes = System.Text.Encoding.UTF8.GetBytes(friendListJson);

            return File(fileBytes, "text/plain", fileName);
        }

        [HttpPost]
        public IActionResult Import(IFormFile file)
        {
            var loggedInUser = HttpContext.Session.GetString("LoggedInUser");
            var user = UserRepository.GetUserByLogin(loggedInUser);

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var content = reader.ReadToEnd();
                List<User> importFriends = JsonSerializer.Deserialize<List<User>>(content) ?? new List<User>();
                user.Friends = new List<User>();
                foreach (var friend in importFriends)
                {
                    if (UserRepository.IsUserExist(friend.Login))
                    {
                        user.Friends.Add(friend);
                    }
                }
            }

            return RedirectToAction("List");
        }
    }
}