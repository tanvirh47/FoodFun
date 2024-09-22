using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Data;
using System.Data.Entity;


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
        // GET: Cart/GetCartCount
        public JsonResult GetCartCount()
        {
            var cart = GetCart();
            int totalCount = cart.Sum(item => item.Quantity);
            return Json(totalCount, JsonRequestBehavior.AllowGet);
        }

        // GET: Cart/Checkout
        public ActionResult Checkout()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            var userId = (int)Session["UserId"]; // Get the logged-in user's ID
            var cartItems = GetCart(); // Retrieve the cart items from the session

            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToAction("Index"); // If no cart items, redirect back to cart
            }

            // Calculate total price of the order
            var totalPrice = cartItems.Sum(ci => ci.Price * ci.Quantity);

            // Create a new order
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalPrice = totalPrice
            };

            db.Orders.Add(order);
            db.SaveChanges(); // Save the order to get the generated OrderId

            // Save each cart item as an order item
            foreach (var cartItem in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    FoodItemId = cartItem.FoodItemId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Price,
                    Name = cartItem.Name
                };

                db.OrderItems.Add(orderItem);
            }

            db.SaveChanges();

            // Clear the cart after checkout
            Session["Cart"] = null;

            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }

        // GET: Cart/OrderHistory
        public ActionResult OrderHistory()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            var userId = (int)Session["UserId"];
            var orders = db.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }

        // GET: Cart/OrderDetails
        public ActionResult OrderDetails(int orderId)
        {
            var orderItems = db.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .Include(oi => oi.FoodItem)
                .ToList();

            return View(orderItems);
        }

        // Helper method to retrieve the cart from session
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

        // Helper method to save the cart to session
        private void SaveCart(List<CartItem> cart)
        {
            Session["Cart"] = cart;
        }
        public ActionResult OrderConfirmation(int orderId)
        {
            var order = db.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
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
