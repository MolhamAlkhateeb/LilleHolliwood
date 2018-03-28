using LilleHaboov.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LilleHaboov.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var model = db.AdminPanels.Where(p => p.UserID == userId).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "Id, ProductsInRow")] AdminPanel panel, string carouselImages)
        {
            var userId = User.Identity.GetUserId();
            var adminPanel = db.AdminPanels.Where(p => p.UserID == userId).FirstOrDefault();
            adminPanel.ProductsInRow = panel.ProductsInRow;
            adminPanel.CarouselImages.Clear();
            foreach (var item in carouselImages.Split(','))
            {
                adminPanel.CarouselImages.Add(new ProductImage() { Url = item });
            }
            db.Entry(adminPanel).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}