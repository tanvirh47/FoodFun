using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.Data; // Ensure this is included

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        // Use the FoodFunDBEntities for database operations
        private FoodFunDBEntities db = new FoodFunDBEntities();

        // Registration
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if username or email already exists in the database
                var existingUser = db.Users.SingleOrDefault(u => u.Username == model.Username || u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Username or email already exists.");
                    return View(model);
                }

                // Create new user
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    PasswordHash = HashPassword(model.Password)
                };

                // Add new user to the database and save changes
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(model);
        }

        // Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Search for user by either Username or Email
                var user = db.Users.SingleOrDefault(u => u.Username == model.UsernameOrEmail || u.Email == model.UsernameOrEmail);
                if (user != null && VerifyPassword(model.Password, user.PasswordHash))
                {
                    // Store username in the session
                    Session["Username"] = user.Username; // Store Username
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(model);
        }


        // Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        // Helper methods
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                return System.Convert.ToBase64String(sha256.ComputeHash(bytes));
            }
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            var hashInput = HashPassword(inputPassword);
            return storedHash == hashInput;
        }
    }
}
