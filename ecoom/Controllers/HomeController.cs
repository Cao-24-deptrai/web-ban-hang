using ecoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecoom.Controllers
{
    public class HomeController : Controller
    {
        private qlbhEntities1 db = new qlbhEntities1();

        public ActionResult Index()
        {
            // Featured products: take latest 6 products
            var featured = db.Products.OrderByDescending(p => p.ProductID).Take(6).ToList();
            ViewBag.Count = GetCartCount();
            return View(featured);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private int GetCartCount()
        {
            var cart = Session["Cart"] as System.Collections.ICollection;
            return cart != null ? cart.Count : 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}