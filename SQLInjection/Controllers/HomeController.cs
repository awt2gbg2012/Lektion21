using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQLInjection.Models;

namespace SQLInjection.Controllers
{
    public class HomeController : Controller
    {
        private DBManager _db;
        public HomeController(DBManager db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SQLInjection(string username)
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            string result = "";
            if (!string.IsNullOrEmpty(username))
            {
                result = _db.GetUserByUserName(username);
            }

            return View((object)result.Replace("</br>", " "));
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult AboutV2()
        {
            return View(new BlogViewModel { Comments = CommentRepo.Instance.FindAll(), Comment = null });
        }

        public ActionResult Evil()
        {
            return View();
        }

        public ActionResult Details()
        {
            var user = new UserViewModel { Password = UserRepo
                                                        .Instance
                                                        .GetPasswordForUser(Guid.Empty) };
            return View(user);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string Password)
        {
            UserRepo.Instance.SetPasswordForUser(Guid.Empty, Password);
            return RedirectToAction("Details");
        }

        [HttpPost]
        //[ValidateInput(false)]
        public RedirectToRouteResult AboutV2(string comment)
        {
            CommentRepo.Instance.Save(comment);
            return RedirectToAction("AboutV2");
        }

        public RedirectToRouteResult CreateAndSeedUsers()
        {
            _db.CreateAndSeedUsers();

            return RedirectToAction("SQLInjection");
        }

        public string ShowAllUsers()
        {
            var users = _db.GetUsers();

            return users;
        }
    }
}
