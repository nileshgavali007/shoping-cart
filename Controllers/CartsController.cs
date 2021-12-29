using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScanAndGo.Models;

namespace ScanAndGo.Controllers
{
    public class CartsController : Controller
    {
        // GET: Carts
        ScanAndGoContext context = new ScanAndGoContext();

        public ActionResult Indexes()
        {
            if (Session["UserID"] != null)
            {
                List<Product> see = context.Products.ToList();
                return View(see);
            }
            else
            {
                return RedirectToAction("Login","Account");
            }
            
        }

        public ActionResult AddToCart()
        {
            ViewBag.MenuItemID = new SelectList(context.Products.ToList(), "Id", "Id");
            return View();
        }
        [HttpPost]
        public ActionResult AddToCart(Cart c)
        {
            ViewBag.MenuItemID = new SelectList(context.Products.ToList(), "Id", "Name");
            Product menu = context.Products.Where(m => m.Id ==c.Id ).FirstOrDefault();
            Cart cart = new Cart();
            cart.productId = c.Id;
            cart.userId = Convert.ToInt32(Session["UserID"]);
            cart.quantity = c.quantity;
            context.Carts.Add(cart);
            context.SaveChanges();

            return RedirectToAction("ViewCart");
        }

        public ActionResult ViewCart()
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
                return RedirectToAction("Login","Account");
            }
           
        }

        public ActionResult EmptyCart()
        {
            return View();
        }

        public ActionResult Delete(int? cartId)
        {
            Cart record = context.Carts.Where(k => k.Id == cartId).SingleOrDefault();
            context.Carts.Remove(record);
            context.SaveChanges();
            return RedirectToAction("ViewCart");
        }
        public double ComputeTotalValue()
        {
            return context.Carts.Sum(e => e.Product.Price);
        }
    }
}