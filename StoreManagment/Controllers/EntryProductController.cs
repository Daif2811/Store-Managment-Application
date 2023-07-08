using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreManagement.Models;
using StoreManagment.Models;
using Microsoft.AspNet.Identity;

namespace StoreManagment.Controllers
{
   [Authorize]
    public class EntryProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EntryProduct
        public ActionResult Index()
        {

            var entryProducts = db.EntryProducts.Include(e => e.Category).Include(e => e.Company).Include(e => e.User);

           

            return View(entryProducts.ToList());


        }




        // All product Of the user that stored  
        public ActionResult MyProducts()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Find(userId);

            var entryProducts = db.EntryProducts.Include(e => e.Category).Include(e => e.Company).Include(e => e.User);
            var Products = entryProducts.Where(a => a.Company.CompanyEmail == user.Email && a.Company.CompanyName == user.UserName);
            return View(Products.ToList());
        }

        public ActionResult MyOutProducts()
        {
            var userId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Find(userId);
            

            var Products = db.ExitProducts.Where(a => a.EntryProduct.Company.CompanyEmail == user.Email && a.EntryProduct.Company.CompanyName == user.UserName);
            return View(Products.ToList());
        }


        // GET: EntryProduct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryProduct entryProduct = db.EntryProducts.Find(id);
            if (entryProduct == null)
            {
                return HttpNotFound();
            }
            return View(entryProduct);
        }

        // GET: EntryProduct/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName");
            
            return View();
        }

        // POST: EntryProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EntryProductId,ProductName,Price,EntryDate,Damaged,Count,CategoryId,CompanyId,UserId")] EntryProduct entryProduct)
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", entryProduct.CategoryId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", entryProduct.CompanyId);

            entryProduct.UserId = User.Identity.GetUserId();
            entryProduct.EntryDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.EntryProducts.Add(entryProduct);
                db.SaveChanges();


                // Create Another object from  EntryHistory class  to save  all products and Their  Date
                EntryHistory history = new EntryHistory();

                history.ProductName = entryProduct.ProductName;
                history.Price = entryProduct.Price;
                history.EntryDate = DateTime.Now;
                history.Damaged = entryProduct.Damaged;
                history.Count = entryProduct.Count;
                history.CategoryId = entryProduct.CategoryId;
                history.CompanyId = entryProduct.CompanyId;
                history.UserId = User.Identity.GetUserId();

                db.EntryHistories.Add(history);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(entryProduct);
        }

        // GET: EntryProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryProduct entryProduct = db.EntryProducts.Find(id);
            if (entryProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", entryProduct.CategoryId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", entryProduct.CompanyId);
           
            return View(entryProduct);
        }

        // POST: EntryProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EntryProductId,ProductName,Price,EntryDate,Damaged,Count,CategoryId,CompanyId,UserId")] EntryProduct entryProduct)
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", entryProduct.CategoryId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", entryProduct.CompanyId);
            entryProduct.UserId = User.Identity.GetUserId();
            entryProduct.EntryDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(entryProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(entryProduct);
        }

        // GET: EntryProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryProduct entryProduct = db.EntryProducts.Find(id);
            if (entryProduct == null)
            {
                return HttpNotFound();
            }
            return View(entryProduct);
        }

        // POST: EntryProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntryProduct entryProduct = db.EntryProducts.Find(id);
            db.EntryProducts.Remove(entryProduct);
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
