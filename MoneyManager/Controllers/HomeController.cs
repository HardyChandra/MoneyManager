using MoneyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using static ClassLibrary.Logic.UserProccessor;

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
    }
}