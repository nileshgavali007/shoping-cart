using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScanAndGo.Models;

namespace ScanAndGo.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        ScanAndGoContext context = new ScanAndGoContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User objUser)
        {

                {
                    var obj = context.Users.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.Id.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        Session["Balance"] = obj.Balance.ToString();
                        return RedirectToAction("Indexes","Carts");
                    }
                return View(objUser);
            }
            }
        
        
        public ActionResult LogOut()
        {

            Session["UserID"] = null;
            Session["UserName"] = null;
            Session["Balance"] = null;
            return RedirectToAction("Login", "Account");
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(AdminLoginModel objUser)
        {
            if (ModelState.IsValid)
            {

                {
                    var obj = context.AdminLoginModels.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = null;
                        Session["UserName"] = null;
                        Session["Balance"] = null;
                        return RedirectToAction("AdminIndex", "Home");
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult RegisterUser()
        {
            List<SelectListItem> Maritalstatus = new List<SelectListItem>();
            Maritalstatus.Add(new SelectListItem { Text = "Married", Value = "Married" });
            Maritalstatus.Add(new SelectListItem { Text = "Unmarried", Value = "Unmarried" });
            ViewBag.Maritalstatus = Maritalstatus;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(User user)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> Maritalstatus = new List<SelectListItem>();
                Maritalstatus.Add(new SelectListItem { Text = "Married", Value = "Married" });
                Maritalstatus.Add(new SelectListItem { Text = "Unmarried", Value = "Unmarried" });
                ViewBag.Maritalstatus = Maritalstatus;
                return View("RegisterUser");
            }
            user.Balance = 0;
            context.Users.Add(user);
            context.SaveChanges();
            return RedirectToAction("RegistrationSuccessful", "Account");
        }

        public ActionResult RegistrationSuccessful()
        {
            return View();
        }

    }
}