using MoneyManager.Models;
using MoneyManager.Repository;
using System;
using System.Web.Mvc;

namespace MoneyManager.Controllers
{
    public class UserController : Controller
    {
        //Get: AddUser
        public ActionResult AddUser()
        {
            return View();
        }

        //Post: AddUser
        [HttpPost]
        public ActionResult AddUser(User usr)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserRepository userRepo = new UserRepository();
                    userRepo.AddUser(usr);

                    ViewBag.Message = "Account has been created successfully!";
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}