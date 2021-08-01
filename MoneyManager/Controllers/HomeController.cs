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
using static ClassLibrary.Logic.ExpensesProcessor;
using static ClassLibrary.Logic.ChartProcessor;



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

        public ActionResult Logout()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult MainPage()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var Bdata = LoadBalance(Convert.ToInt32(Session["UserID"]));
                var Edata = LoadExpenses(Convert.ToInt32(Session["UserID"]));

                ViewBag.TB = Bdata.Sum(x => x.TotalBalance);
                ViewBag.TE = Edata.Sum(x => x.TotalExpenses);
                ViewBag.T2 = Bdata.Sum(x => x.TotalBalance) - Edata.Sum(x => x.TotalExpenses);

                return View();
            }
        }

        //Display the data to Chart from Pie
        public ActionResult ExpensesChart()
        {
            return View();
        }

        //Get Data
        public ActionResult Pie()
        {
            if (Session["UserID"] != null)
            {
                var PData = LoadExpensesChart(Convert.ToInt32(Session["UserID"]));

                return Json(PData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Login", "Home");
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
            if (DeleteCategory(del.CategoryID) > 0)
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

                return RedirectToAction("ViewBalance");
            }

            return View();
        }

        public ActionResult ViewBalance()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var data = LoadBalance(Convert.ToInt32(Session["UserID"]));
                List<BalanceS> balance = new List<BalanceS>();

                foreach (var row in data)
                {
                    balance.Add(new BalanceS
                    {
                        BalanceID = row.BalanceID,
                        UserID = row.UserID,
                        TotalBalance = row.TotalBalance
                    });
                }
                return View(balance);
            }
        }

        public ActionResult RemoveBalance(int BalanceID)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.message = "Delete Balance";
                var balance = LoadBalanceByID(Convert.ToInt32(BalanceID));

                return View(balance);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveBalance(BalanceS del)
        {
            if (DeleteBalance(del.BalanceID) > 0)
            {
                return RedirectToAction("ViewBalance");
            }
            return View(del);
        }

        [HttpGet]
        public ActionResult AddExpenses()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var data = LoadCategory(Convert.ToInt32(Session["UserID"]));
                SelectList list = new SelectList(data, "CategoryID" /*"CategoryName"*/);
                ViewBag.CategoryList = list;

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddExpenses(ExpensesS add)
        {
            if (ModelState.IsValid)
            {
                SaveExpenses(Convert.ToInt32(Session["UserID"]),
                   add.CategoryID, add.ExpensesDetail, add.TotalExpenses);

                return RedirectToAction("ViewExpenses");
            }

            return View();
        }

        public ActionResult ViewExpenses()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var data = LoadExpenses(Convert.ToInt32(Session["UserID"]));
                List<ExpensesS> expenses = new List<ExpensesS>();

                foreach (var row in data)
                {
                    expenses.Add(new ExpensesS
                    {
                        ExpensesID = row.ExpensesID,
                        UserID = row.UserID,
                        CategoryID = row.CategoryID,
                        CategoryName = row.CategoryName,
                        ExpensesDetail = row.ExpensesDetail,
                        TotalExpenses = row.TotalExpenses
                    });
                }

                return View(expenses);
            }
        }

        [HttpGet]
        public ActionResult UpdateExpenses(int? ExpensesID)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.message = "Edit Expenses";
                var expenses = LoadExpensesByID(Convert.ToInt32(ExpensesID));

                return View(expenses);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateExpenses(ExpensesS edit)
        {
            if (ModelState.IsValid)
            {
                EditExpenses(edit.ExpensesID,
                    edit.ExpensesDetail, edit.TotalExpenses);

                return RedirectToAction("ViewExpenses");
            }

            return View();
        }

        public ActionResult RemoveExpenses(int ExpensesID)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.message = "Delete Category";
                var expenses = LoadExpensesByID(Convert.ToInt32(ExpensesID));

                return View(expenses);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveExpenses(ExpensesS del)
        {
            if (DeleteExpenses(del.ExpensesID) > 0)
            {
                return RedirectToAction("ViewExpenses");
            }
            return View(del);
        }
    }
}