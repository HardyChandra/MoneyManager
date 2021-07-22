using MoneyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using static DataLibrary.Logic.UserProcessor;
using static DataLibrary.Logic.CategoryProcessor;
using static DataLibrary.DataAccess.SqlDataAccess;

namespace MoneyManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            ViewBag.Message = "Sign Up";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(User usr)
        {
            if (ModelState.IsValid)
            {
                CreateUser(usr.Name,
                    usr.Username,
                    usr.Password);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Login";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginUser usr)
        {
            if (ModelState.IsValid)
            {
                var usrLogin = LoginUser(usr.Username, usr.Password);

                if (usrLogin != null)
                {
                    ViewBag.message = "LoggedIn";
                    Session["Username"] = usr.Username.ToString();
           
                    return RedirectToAction("MainPage", new { Username = usr.Username });
                }
            }
            return View();
        }

        public ActionResult AddCategory()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.message = "Add Category";

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(AddCategory add)
        {
            if (ModelState.IsValid)
            {
                StoreCategory(Session["Username"].ToString(),
                     add.CategoryName);

                return RedirectToAction("MainPage");
            }

            return View();
        }

        public ActionResult MainPage()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.Username = Session["Username"];
                return View();
            }
        }
    }
}