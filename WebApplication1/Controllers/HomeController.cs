using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

 
    public class HomeController : Controller
    {

        private FoodFunDBEntities db = new FoodFunDBEntities();

        public ActionResult Index()
        {
            
            var foodItems = db.FoodItems.ToList(); 
            return View(foodItems);
        }

        public ActionResult About()
        {
            var foodItems = db.FoodItems.ToList(); // Assuming FoodItems is the name of your DbSet
            return View(foodItems);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Menu()
        {
            var foodItems = db.FoodItems.ToList(); 
            return View(foodItems);
        }
        public ActionResult Element()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}