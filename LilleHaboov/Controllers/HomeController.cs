using LilleHaboov.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LilleHaboov.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            var cart = userID != null ? db.Users.Find(userID).Cart : new Cart();
            var catagories = db.Catagories.ToList();
            var panel = db.AdminPanels.FirstOrDefault();
            var model = new HomePageViewModel() { Catagories = catagories, AdminPanel = panel };
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetProducts(int catagoryId)
        {
            var model = db.Catagories.Where(c => c.Id == catagoryId).FirstOrDefault().Products;
            return PartialView("_Products", model);
        }

        public string GetCartCount()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userID = User.Identity.GetUserId();
                var cart = db.Users.Find(userID).Cart;
                return cart != null ? cart.Products.Count().ToString() : 0.ToString();
            }
            return "0";
        }

        public ActionResult Cart()
        {
            var userId = User.Identity.GetUserId();
            var model = User.Identity.IsAuthenticated ? db.Users.Find(userId).Cart : new Cart();

            return View(model);
        }
    }
}