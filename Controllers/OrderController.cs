using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScanAndGo.Models;

namespace ScanAndGo.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        ScanAndGoContext context = new ScanAndGoContext();
        public ActionResult GenerateOrder()
        {

            if (Session["UserID"] != null)
            {
                int cartContentsCount = context.Carts.Select(c => c.Id).Count();
                int user = Convert.ToInt32(Session["UserID"]);
                if (cartContentsCount < 1)
                    return RedirectToAction("EmptyCart");
                return View(context.Carts.Where(u => u.userId == user).ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult PaymentFailed()
        {
            int user = Convert.ToInt32(Session["UserID"]);
            ViewBag.total = context.Carts.Where(u => u.userId == user).Sum(e => e.Product.Price * e.quantity);
            return View();
        }

        public ActionResult pay()
        {

            if (Session["UserID"] != null && context.Carts.Select(c => c.Id).Count() >=1)
            {
                int user = Convert.ToInt32(Session["UserID"]);
                double total = context.Carts.Where(u => u.userId == user).Sum(e => e.Product.Price * e.quantity);
                double balance = Convert.ToInt32(Session["Balance"]) - total;
                
                if (total < Convert.ToInt32(Session["Balance"]))
                {
                    ViewBag.paid = total;
                    context.Carts.RemoveRange(context.Carts.Where(u => u.userId == user));
                    var selectedUser = context.Users.Single(i => i.Id == user);
                    selectedUser.Balance = Convert.ToInt32(balance);
                    context.SaveChanges();
                    
                    Session["Balance"] = balance;
                    return View();
                }
                else return RedirectToAction("PaymentFailed", "Order");
            }
            else
            {
                return View();
            }
        }
    }
}
