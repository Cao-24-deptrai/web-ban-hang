using ecoom.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecoom.Controllers
{
    public class ProductController : Controller
    {
        qlbhEntities1 db = new qlbhEntities1();
        // GET: Product
        public ActionResult IndexCustomer(decimal? _min, decimal? _max)
        {
            var proFilter = db.Products.AsQueryable();

            if (_min.HasValue)
                proFilter = proFilter.Where(s => s.Price >= _min.Value);
            if (_max.HasValue)
                proFilter = proFilter.Where(s => s.Price <= _max.Value);

            ViewBag.MinPrice = _min;
            ViewBag.MaxPrice = _max;
            ViewBag.Count = GetCartCount();

            return View(proFilter.ToList());
        }
        public ActionResult Index(decimal? _min, decimal? _max)
        {
            var proFilter = db.Products.AsQueryable();
            if (_min.HasValue)
                proFilter = proFilter.Where(s => s.Price >= _min.Value);
            if (_max.HasValue)
                proFilter = proFilter.Where(s => s.Price <= _max.Value);

            ViewBag.MinPrice = _min;
            ViewBag.MaxPrice = _max;
            ViewBag.Count = GetCartCount();

            return View(proFilter.ToList());
        }

        private int GetCartCount()
        {
            var cart = Session["Cart"] as ICollection;
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
