using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class CheckoutController : Controller
    {
        private FoodFunDBEntities db = new FoodFunDBEntities();

        // GET: Checkout
        public ActionResult Checkout()
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Cart");
            }
            var model = new CheckoutModel
            {
                CartItems = cart
            };
            return View(model);
        }

        // POST: Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(CheckoutModel model)
        {
            if (ModelState.IsValid)
            {
                // Process order
                // Clear cart
                Session["Cart"] = null;
                return RedirectToAction("OrderConfirmation");
            }
            return View(model);
        }

        public ActionResult OrderConfirmation()
        {
            return View();
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
