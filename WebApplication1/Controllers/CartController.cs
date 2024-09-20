using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        private FoodFunDBEntities db = new FoodFunDBEntities();

        // GET: Cart
        public ActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // POST: Cart/Add
        [HttpPost]
        public ActionResult Add(int id, int quantity)
        {
            var foodItem = db.FoodItems.Find(id);
            if (foodItem != null)
            {
                var cart = GetCart();
                var cartItem = cart.FirstOrDefault(x => x.FoodItemId == id);
                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        FoodItemId = foodItem.Id,
                        Name = foodItem.Name,
                        Price = foodItem.Price ?? 0,  // Handle nullable Price here
                        Quantity = quantity
                    });
                }
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        // POST: Cart/Remove
        [HttpPost]
        public ActionResult Remove(int id)
        {
            var cart = GetCart();
            var cartItem = cart.FirstOrDefault(x => x.FoodItemId == id);
            if (cartItem != null)
            {
                cart.Remove(cartItem);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Checkout()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            // Proceed with checkout logic
            return View();
        }


        private List<CartItem> GetCart()
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                Session["Cart"] = cart;
            }
            return cart;
        }

        private void SaveCart(List<CartItem> cart)
        {
            Session["Cart"] = cart;
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
