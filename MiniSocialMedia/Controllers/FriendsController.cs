using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiniSocialMedia.Controllers
{
    public class FriendsController : Controller
    {
        // GET: FriendsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FriendsController/List
        public ActionResult List()
        {
            return View();
        }

        // GET: FriendsController/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: FriendsController/Add/login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(string login)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FriendsController/Del/login
        public ActionResult Del(string login)
        {
            return View();
        }

        // POST: FriendsController/Del/login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string login, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
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
