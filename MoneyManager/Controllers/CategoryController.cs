using MoneyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MoneyManager.DataLibrary.Repository.CategoryProcessor;
using static MoneyManager.DataLibrary.Repository.ExpensesProcessor;

namespace MoneyManager.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCategory()
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
        public ActionResult AddCategory(CategoryS add, string CategoryName)
        {
            if (ModelState.IsValid)
            {
                var check = CheckCategory(Convert.ToInt32(Session["UserID"]), CategoryName);
                if (check.CategoryName != add.CategoryName)
                {
                    StoreCategory(Convert.ToInt32(Session["UserID"]),
                                  add.CategoryName);

                    return RedirectToAction("ViewCategory");
                }
                else
                {
                    ViewBag.Message = "Category already exist!";
                    return View();
                }
                
            }

            return View();
        }

        public ActionResult ViewCategory()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "User");
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
        public ActionResult UpdateCategory(int CategoryID)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
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
                return RedirectToAction("Login", "User");
            }
            else
            {
                var category = LoadCategoryByID(Convert.ToInt32(CategoryID));

                return View(category);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveCategory(CategoryS del)
        {
            var check = CheckCategoryFromExpenses(Convert.ToInt32(Session["UserID"]));
            if (del.CategoryID != check.CategoryID)
            {
                if (DeleteCategory(del.CategoryID) > 0)
                {
                    return RedirectToAction("ViewCategory");
                }
            }
            else
            {
                ViewBag.Message = "Cannot delete category because is still in used";

                //return RedirectToAction("ViewCategory");
                return View();
            }
            return View(del);
        }
    }
}