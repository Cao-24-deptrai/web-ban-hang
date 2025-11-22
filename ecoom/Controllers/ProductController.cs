using ecoom.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecoom.Controllers
{
    public class ProductController : Controller
    {
        qlbhEntities db = new qlbhEntities();
        // GET: Product
        public ActionResult IndexCustomer(decimal? _min, decimal? _max)
        {
            var proFilter = db.Products.AsQueryable();
            
            if (_min.HasValue)
                proFilter = proFilter.Where(s => s.Price >= _min.Value);
            if (_max.HasValue)
                proFilter = proFilter.Where(s => s.Price <= _max.Value);
            return View(proFilter.ToList());
        }
        public ActionResult Index(decimal? _min, decimal? _max)
        {
            var proFilter = db.Products.AsQueryable();
            if (_min.HasValue)
                proFilter = proFilter.Where(s => s.Price >= _min.Value);
            if (_max.HasValue)
                proFilter = proFilter.Where(s => s.Price <= _max.Value);
            return View(proFilter.ToList());
        }
    }
}
