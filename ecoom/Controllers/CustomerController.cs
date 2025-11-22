using ecoom.Models;
using System.Linq;
using System.Web.Mvc;

namespace ecoom.Controllers
{
    public class CustomerController : Controller
    {
        private qlbhEntities1 db = new qlbhEntities1();

        // GET: Customer/Register
        public ActionResult Register()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        // POST: Customer/Register
        [HttpPost]
        public ActionResult Register(Customer model)
        {
            if (ModelState.IsValid)
            {
                var exists = db.Customers.FirstOrDefault(c => c.Email == model.Email);
                if (exists != null)
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng");
                    if (Request.IsAjaxRequest())
                        return Json(new { success = false, errors = GetModelErrors() });
                    return View(model);
                }

                if (string.IsNullOrEmpty(model.Password))
                {
                    ModelState.AddModelError("Password", "Vui lòng nhập mật khẩu");
                    if (Request.IsAjaxRequest())
                        return Json(new { success = false, errors = GetModelErrors() });
                    return View(model);
                }

                db.Customers.Add(model);
                db.SaveChanges();

                Session["ID"] = model.Name;

                if (Request.IsAjaxRequest())
                    return Json(new { success = true, name = model.Name });

                return RedirectToAction("Index", "Home");
            }

            if (Request.IsAjaxRequest())
                return Json(new { success = false, errors = GetModelErrors() });

            return View(model);
        }

        // GET: Customer/Login
        public ActionResult Login()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        // POST: Customer/Login
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Vui lòng nhập email và mật khẩu để đăng nhập.");
                if (Request.IsAjaxRequest())
                    return Json(new { success = false, errors = GetModelErrors() });
                return View();
            }

            var user = db.Customers.FirstOrDefault(c => c.Email == email && c.Password == password);
            if (user != null)
            {
                Session["ID"] = user.Name;
                if (Request.IsAjaxRequest())
                    return Json(new { success = true, name = user.Name });
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Thông tin đăng nhập không đúng.");
            if (Request.IsAjaxRequest())
                return Json(new { success = false, errors = GetModelErrors() });

            return View();
        }

        // GET: Customer/Logout
        public ActionResult Logout()
        {
            Session.Remove("ID");
            return RedirectToAction("Index", "Home");
        }

        private object GetModelErrors()
        {
            var errors = ModelState.Where(kvp => kvp.Value.Errors.Any())
                                   .Select(kvp => new { key = kvp.Key, errors = kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray() })
                                   .ToArray();
            return errors;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
