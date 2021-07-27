using MoneyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using static DataLibrary.Logic.UserProcessor;
using static DataLibrary.Logic.CategoryProcessor;
using static ClassLibrary.Logic.BalanceProcessor;
using DataLibrary.Models;

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
        public ActionResult SignUp(UserS usr)
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
                    Session["UserID"] = usrLogin.UserID.ToString();
                    Session["Name"] = usrLogin.Name.ToString();

                    return RedirectToAction("MainPage");
                }
            }
            return View();
        }

        public ActionResult MainPage()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult AddCategory()
        {
            if (Session["UserID"] == null)
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
        public ActionResult AddCategory(CategoryS add)
        {
            if (ModelState.IsValid)
            {
                StoreCategory(Convert.ToInt32(Session["UserID"]),
                   add.CategoryName);

                return RedirectToAction("ViewCategory");
            }

            return View();
        }

        public ActionResult ViewCategory()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var data = LoadCategory(Convert.ToInt32(Session["UserID"]));
                List<CategoryS> category = new List<CategoryS>();

                foreach (var row in data)
                {
                    category.Add(new CategoryS
                    {
                        CategoryID = row.CategoryID,
                        CategoryName = row.CategoryName, 
                        UserID = row.UserID
                    });
                }

                return View(category);
            }
        }

        [HttpGet]
        public ActionResult UpdateCategory(int? CategoryID)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.message = "Edit Category";             
                var category = LoadCategoryByID(Convert.ToInt32(CategoryID));

                return View(category);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCategory(CategoryS edit)
        {
            if (ModelState.IsValid)
            {
                EditCategory(edit.CategoryID,
                    edit.CategoryName);

                return RedirectToAction("ViewCategory");
            }

            return View();
        }

        public ActionResult RemoveCategory(int CategoryID)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.message = "Delete Category";             
                var category = LoadCategoryByID(Convert.ToInt32(CategoryID));

                return View(category);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveCategory(CategoryS del)
        {
            if(DeleteCategory(del.CategoryID) > 0)
            {
                return RedirectToAction("ViewCategory");
            }
            return View(del);
        }

        public ActionResult AddBalance()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.message = "Add Balance";

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBalance(BalanceS add)
        {
            if (ModelState.IsValid)
            {
                StoreBalance(Convert.ToInt32(Session["UserID"]),
                     add.TotalBalance);

                return RedirectToAction("MainPage");
            }

            return View();
        }
    }
}