using MoneyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MoneyManager.DataLibrary.Repository.BalanceProcessor;

namespace MoneyManager.Controllers
{
    public class BalanceController : Controller
    {
        // GET: Balance
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddBalance()
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
                return RedirectToAction("Login", "User");
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
                return RedirectToAction("Login", "User");
            }
            else
            {
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
    }
}