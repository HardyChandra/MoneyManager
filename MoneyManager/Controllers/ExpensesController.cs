using MoneyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MoneyManager.DataLibrary.Repository.ExpensesProcessor;
using static MoneyManager.DataLibrary.Repository.ChartProcessor;
using static MoneyManager.DataLibrary.Repository.CategoryProcessor;

namespace MoneyManager.Controllers
{
    public class ExpensesController : Controller
    {
        // GET: Expenses
        public ActionResult Index()
        {
            return View();
        }

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
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public ActionResult AddExpenses()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var data = LoadCategory(Convert.ToInt32(Session["UserID"]));
                SelectList list = new SelectList(data, "CategoryID", "CategoryName");
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
                return RedirectToAction("Login", "User");
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
                        CreatedDate = row.CreatedDate,
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
                return RedirectToAction("Login", "User");
            }
            else
            {
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
                return RedirectToAction("Login", "User");
            }
            else
            {
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