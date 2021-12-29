using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScanAndGo.Models;

namespace ScanAndGo.Controllers
{
    public class HomeController : Controller
    {
        ScanAndGoContext db = new ScanAndGoContext();

        public static string controllerName;
        public static string actionName;
        protected override void OnException(ExceptionContext exceptionContext)
        {
            controllerName = (string)exceptionContext.RouteData.Values["Controller"];
            actionName = (string)exceptionContext.RouteData.Values["action"];

            ViewResult view = new ViewResult();
            view.ViewName = "~/Views/Shared/Error.cshtml";
            exceptionContext.Result = view;
            exceptionContext.ExceptionHandled = true;

        }
        public ActionResult AdminValidation(bool isAdmin = false)
        {
            if (isAdmin == true)
                return RedirectToAction("AdminProducts");
            else
                return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminIndex()
        {
            return View();
        }

        public ActionResult AdminProducts()
        {
            var menuItemList = (from m in db.Products.Include("Category") select m).ToList();
            return View(menuItemList);
        }

        public ActionResult newRecord()
        {
            ViewBag.CategoryList = new SelectList(db.Categories.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult newRecord(Product menuItem)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = new SelectList(db.Categories.ToList(), "Id", "Name");
                return View("newRecord");
            }
            db.Products.Add(menuItem);
            db.SaveChanges();
            return RedirectToAction("AdminProducts", "Home");
        }
        public ActionResult deleteRecord(int? id)
        {
            Product record = db.Products.Where(k => k.Id == id).SingleOrDefault();
            db.Products.Remove(record);
            db.SaveChanges();
            return RedirectToAction("AdminProducts");
        }

        public ActionResult updateRecord(int id)
        {
            Product menuItem = new Product();
            menuItem = db.Products.Include("Category").SingleOrDefault(c => c.Id == id);
            if (menuItem == null)
                return HttpNotFound();

            ViewBag.CategoryList = new SelectList(db.Categories.ToList(), "Id", "Name");
            return View(menuItem);
        }

        [HttpPost]
        public ActionResult updateRecord(Product menuItem)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = new SelectList(db.Categories.ToList(), "Id", "Name");
                return View("updateRecord");
            }

            if (menuItem.Id == 0)
                db.Products.Add(menuItem);
            else
            {
                var selectedMenuItem = db.Products.Single(i => i.Id == menuItem.Id);

                selectedMenuItem.Name = menuItem.Name;
                selectedMenuItem.Price = menuItem.Price;
                selectedMenuItem.Active = menuItem.Active;
                selectedMenuItem.stockingDate = menuItem.stockingDate;
                selectedMenuItem.categoryId = menuItem.categoryId;
            }
            db.SaveChanges();
            return RedirectToAction("AdminIndex", "home");
        }

        public ActionResult RechargeSuccessful()
        {
            
            return View();
        }
        public ActionResult RechargeWallet()
        {
            ViewBag.UserList = new SelectList(db.Users.ToList(), "Id", "Username");
            return View();
        }
        [HttpPost]
        public ActionResult RechargeWallet(User user)
        {
            var selectedUser = db.Users.Single(i => i.Id == user.Id);
            selectedUser.Balance = selectedUser.Balance + user.Balance ;
            db.SaveChanges();
            return RedirectToAction("RechargeSuccessful");
        }

    }
}