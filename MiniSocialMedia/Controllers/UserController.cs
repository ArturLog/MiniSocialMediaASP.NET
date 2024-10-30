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
        [HttpGet]
        public IActionResult List()
        {
            var allUsers = UserRepository.Users.ToList();
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
            if (UserRepository.AddUser(login)) return RedirectToAction(nameof(List));
            ModelState.AddModelError("", "Login użytkownika jest wymagany i musi być unikalny.");
            return RedirectToAction(nameof(Add));
        }

        [HttpGet]
        [Route("User/Del/{login}")]
        public IActionResult Del(string login)
        {
            if(UserRepository.RemoveUser(login)) return RedirectToAction(nameof(List));
            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        public IActionResult DelPost(string login)
        {
            if(UserRepository.RemoveUser(login)) return RedirectToAction(nameof(List));
            return RedirectToAction(nameof(List));
        }
        public IActionResult Init()
        {
            UserRepository.InitializeSampleData();
            return RedirectToAction(nameof(List));
        }

    }
}
