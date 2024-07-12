using System;
using System.Net;
using System.Net.Mail;


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
        [HttpPost]
        public ActionResult Contact(string Name, string Email, string Subject, string Message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("tanvirh934@gmail.com");
                mail.To.Add("tanvirh934@gmail.com");
                mail.Subject = Subject;
                mail.Body = $"Name: {Name}\nEmail: {Email}\nMessage: {Message}";
                mail.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new NetworkCredential("tanvirh934@gmail.com", "jmlwvsvazwropqjx"),
                    EnableSsl = true
                };

                smtp.Send(mail);
                ViewBag.SuccessMessage = "Email sent successfully!";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error sending email: " + ex.Message;
            }

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