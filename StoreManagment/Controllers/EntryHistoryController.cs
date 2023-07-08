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
    public class EntryHistoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EntryHistory
        public ActionResult Index()
        {
            var entryHistories = db.EntryHistories.Include(e => e.Category).Include(e => e.Company).Include(e => e.User);
            return View(entryHistories.ToList());
        }















        // I don't need any thing from these Actions   Because  this is a history and I dont need to create , Edit or delete any thing



        // GET: EntryHistory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryHistory entryHistory = db.EntryHistories.Find(id);
            if (entryHistory == null)
            {
                return HttpNotFound();
            }
            return View(entryHistory);
        }

        // GET: EntryHistory/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName");
           
            return View();
        }

        // POST: EntryHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EntryHistoryId,ProductName,Price,EntryDate,Damaged,Count,CategoryId,CompanyId,UserId")] EntryHistory entryHistory)
        {
            if (ModelState.IsValid)
            {
                db.EntryHistories.Add(entryHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", entryHistory.CategoryId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", entryHistory.CompanyId);
            entryHistory.UserId = User.Identity.GetUserId();
            return View(entryHistory);
        }

        // GET: EntryHistory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryHistory entryHistory = db.EntryHistories.Find(id);
            if (entryHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", entryHistory.CategoryId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", entryHistory.CompanyId);
            
            return View(entryHistory);
        }

        // POST: EntryHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EntryHistoryId,ProductName,Price,EntryDate,Damaged,Count,CategoryId,CompanyId,UserId")] EntryHistory entryHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entryHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", entryHistory.CategoryId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", entryHistory.CompanyId);
            entryHistory.UserId = User.Identity.GetUserId();
            return View(entryHistory);
        }

        // GET: EntryHistory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntryHistory entryHistory = db.EntryHistories.Find(id);
            if (entryHistory == null)
            {
                return HttpNotFound();
            }
            return View(entryHistory);
        }

        // POST: EntryHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntryHistory entryHistory = db.EntryHistories.Find(id);
            db.EntryHistories.Remove(entryHistory);
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
