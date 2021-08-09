using MoneyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MoneyManager.DataLibrary.Repository.UserProcessor;
using static MoneyManager.DataLibrary.Repository.BalanceProcessor;
using static MoneyManager.DataLibrary.Repository.ExpensesProcessor;

namespace MoneyManager.Controllers
{
    public class UserController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserS usr)
        {
            if (ModelState.IsValid)
            {
                var check = CheckUsername();

                if (check?.Username != usr.Username)
                {
                    CreateUser(usr.Name,
                               usr.Username,
                               usr.Password);
                    return RedirectToAction("MainPage");
                }
                else
                {
                    ViewBag.Message = "Username has been registered!";
                    return View();
                }

            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginUser usr)
        {
            if (ModelState.IsValid)
            {
                var usrLogin = LoginUser(usr.Username, usr.Password);
                var check = CheckUsername();
                if (check?.Username == usr.Username)
                {
                    if (usrLogin != null)
                    {
                        Session["Username"] = usr.Username.ToString();
                        Session["UserID"] = usrLogin.UserID.ToString();
                        Session["Name"] = usrLogin.Name.ToString();

                        return RedirectToAction("MainPage");
                    }
                    else
                    {
                        ViewBag.Message = "Incorrect Password!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Username or password is wrong!";
                }               
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();

            return RedirectToAction("Index", "User");
        }

        public ActionResult MainPage()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var Bdata = LoadBalance(Convert.ToInt32(Session["UserID"]));
                var Edata = LoadExpenses(Convert.ToInt32(Session["UserID"]));

                ViewBag.TB = Bdata.Sum(x => x.TotalBalance);
                ViewBag.TE = Edata.Sum(x => x.TotalExpenses);
                ViewBag.T2 = Bdata.Sum(x => x.TotalBalance) - Edata.Sum(x => x.TotalExpenses);
                ViewBag.Time = DateTime.Now.ToString("M-d-yyyy");

                return View();
            }
        }

        public ActionResult ViewUserProfile()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var user = LoadUserProfile(Convert.ToInt32(Session["UserID"]));

                return View(user);
            }
        }

        [HttpGet]
        public ActionResult EditUserProfile(int UserID)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var user = LoadUserProfile(Convert.ToInt32(UserID));

                return View(user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserProfile(UserProfile edit)
        {
            if (ModelState.IsValid)
            {
                EditUser(edit.UserID,
                    edit.Name, edit.PhoneNumber, edit.Email);

                return RedirectToAction("ViewUserProfile");
            }

            return View();
        }

        public ActionResult ChangeUserPassword()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeUserPassword(PasswordS edit)
        {
            if (ModelState.IsValid)
            {
                var check = CheckPassword(Convert.ToInt32(Session["UserID"]));
                if (check.Password == edit.CurrentPassword)
                {
                    ChangePassword(edit.UserID, edit.Password);

                    return RedirectToAction("ViewUserProfile");
                }
                else
                {
                    ViewBag.Message = "Incorrect Current Password!";
                    return View();
                }

            }
            return View();
        }
    }
}