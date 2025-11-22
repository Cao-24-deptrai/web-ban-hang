using ecoom.Models;
using System.Linq;
using System.Web.Mvc;

namespace ecoom.Controllers
{
    public class OrderController : Controller
    {
        private qlbhEntities1 db = new qlbhEntities1();

        public ActionResult Index()
        {
            var orders = db.OrderProes.OrderByDescending(o => o.DateOrder).ToList();
            return View(orders);
        }

        public ActionResult Details(int id)
        {
            var order = db.OrderProes.Find(id);
            if (order == null)
                return HttpNotFound();
            return View(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
