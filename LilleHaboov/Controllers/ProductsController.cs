using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LilleHaboov.Models;

namespace LilleHaboov.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var catagories = db.Catagories.ToList();
            var model = new CatagoriesProductViewModel { Catagories = catagories };
            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,LongDescription,Price,Currency")] Product product, string selectedCatagories, string selectedImages)
        {
            if (ModelState.IsValid)
            {

                product.Catagories = new List<Catagory>();
                product.Images = new List<ProductImage>();
                foreach (var item in selectedCatagories.Split(','))
                {
                    int.TryParse(item, out int id);
                    Catagory catagory = db.Catagories.Where(c => c.Id == id).FirstOrDefault();
                    product.Catagories.Add(catagory);
                }
                foreach (var item in selectedImages.Split(','))
                {
                    product.Images.Add(new ProductImage()
                    {
                        Url = item
                    });
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var catagories = db.Catagories.ToList();
            var model = new CatagoriesProductViewModel { Catagories = catagories, Product = product };

            return View(model);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var catagories = db.Catagories;
            var model = new CatagoriesProductViewModel()
            {
                Catagories = catagories,
                Product = product
            };
            return View(model);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,LongDescription,Price,Currency")] Product product, string selectedCatagories, string selectedImages)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(product).State = EntityState.Modified;
                //db.SaveChanges();

                var prod = db.Products.Find(product.Id);
                prod.Catagories.Clear();
                foreach (var item in selectedCatagories.Split(','))
                {
                    if (int.TryParse(item, out int id))
                    {
                        var catagory = db.Catagories.Find(id);
                        if (catagory != null)
                        {
                            prod.Catagories.Add(catagory);
                        }
                    }
                }
                prod.Name = product.Name;
                prod.Description = product.Description;
                prod.LongDescription = product.LongDescription;
                prod.Price = product.Price;
                prod.Currency = product.Currency;
                prod.Images = new List<ProductImage>();
                foreach (var url in selectedImages.Split(','))
                {
                    prod.Images.Add(new ProductImage()
                    {
                        Url = url
                    });
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SeedTestProducts(int quantity)
        {
            Random rnd = new Random();

            for (int i = 0; i < quantity; i++)
            {
                Random rand = new Random();
                int toSkip = rand.Next(0, db.Catagories.Count());

                Catagory cat = db.Catagories.OrderBy(c=>c.Id).Skip(toSkip).Take(1).First();
                db.Products.Add(new Product()
                {
                    Catagories = new List<Catagory>() { cat },
                    Name = Guid.NewGuid().ToString(),
                    Price = rand.Next(50, 10000)
                });
            }
            db.SaveChanges();
            return RedirectToAction("Index");
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
