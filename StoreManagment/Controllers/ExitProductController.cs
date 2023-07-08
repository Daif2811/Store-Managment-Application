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
    public class ExitProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExitProduct
        public ActionResult Index()
        {
            var exitProducts = db.ExitProducts.Include(e => e.EntryProduct).Include(e => e.User);
            return View(exitProducts.ToList());
        }

        // GET: ExitProduct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExitProduct exitProduct = db.ExitProducts.Find(id);
            if (exitProduct == null)
            {
                return HttpNotFound();
            }
            return View(exitProduct);
        }

        // GET: ExitProduct/Create
        public ActionResult Create()
        {
            ViewBag.EntryProductId = new SelectList(db.EntryProducts, "EntryProductId", "ProductName");

            return View();
        }

        // POST: ExitProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExitProductId,ExitDate,Count,UserId,EntryProductId")] ExitProduct exitProduct)
        {
            ViewBag.EntryProductId = new SelectList(db.EntryProducts, "EntryProductId", "ProductName", exitProduct.EntryProductId);
            exitProduct.UserId = User.Identity.GetUserId();
            exitProduct.ExitDate = DateTime.Now;
            //ViewBag.Available = from a in db.EntryProducts
            //                    where a.EntryProductId == exitProduct.EntryProductId
            //                    select a.Count;

            if (ModelState.IsValid)
            {


                EntryProduct Balance = db.EntryProducts.Find(exitProduct.EntryProductId);
                var CheckCount = db.EntryProducts.Where(a => a.EntryProductId == exitProduct.EntryProductId && a.Count < exitProduct.Count).ToList();
                if (CheckCount.Count < 1)
                {
                    db.ExitProducts.Add(exitProduct);
                    db.SaveChanges();

                    if (Balance != null)
                    {
                        Balance.Count = Balance.Count - exitProduct.Count;
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    if (Balance.Count > 0)
                    {
                        //ModelState.AddModelError("", "This Quantity is not Available. The Available quantity is : " + Balance.Count);
                        ViewBag.Error = "This Quantity is not Available. The Available quantity is : " + Balance.Count;
                    }
                    else
                    {
                        ViewBag.Error = "The Store has No quantity from this product";
                    }
                }



            }

            return View(exitProduct);
        }




        public ActionResult ExitFromProducts(int? id)
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


            Session["ProductId"] = id;

            return View();

        }
        [HttpPost]
        public ActionResult ExitFromProducts(ExitProduct exitProduct)
        {
            exitProduct.UserId = User.Identity.GetUserId();
            exitProduct.ExitDate = DateTime.Now;
            exitProduct.EntryProductId = (int)Session["ProductId"];

            if (ModelState.IsValid)
            {


                EntryProduct Balance = db.EntryProducts.Find(exitProduct.EntryProductId);
                var CheckCount = db.EntryProducts.Where(a => a.EntryProductId == exitProduct.EntryProductId && a.Count < exitProduct.Count).ToList();
                if (CheckCount.Count < 1)
                {
                    db.ExitProducts.Add(exitProduct);
                    db.SaveChanges();

                    if (Balance != null)
                    {
                        Balance.Count = Balance.Count - exitProduct.Count;
                        db.SaveChanges();

                    }
                    return RedirectToAction("Index");

                }
                else
                {
                    if (Balance.Count > 0)
                    {
                        ViewBag.Error = "This Quantity is not Available. The Available quantity is : " + Balance.Count;
                    }
                    else
                    {
                        ViewBag.Error = "The Store has No quantity from this product";
                    }
                }



            }

            return View(exitProduct);
        }










        // GET: ExitProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExitProduct exitProduct = db.ExitProducts.Find(id);
            if (exitProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.EntryProductId = new SelectList(db.EntryProducts, "EntryProductId", "ProductName", exitProduct.EntryProductId);

            return View(exitProduct);
        }

        // POST: ExitProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExitProductId,ExitDate,Count,UserId,EntryProductId")] ExitProduct exitProduct)
        {
            ViewBag.EntryProductId = new SelectList(db.EntryProducts, "EntryProductId", "ProductName", exitProduct.EntryProductId);
            exitProduct.UserId = User.Identity.GetUserId();
            exitProduct.ExitDate = DateTime.Now;


            if (ModelState.IsValid)
            {
                db.Entry(exitProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exitProduct);
        }

        // GET: ExitProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExitProduct exitProduct = db.ExitProducts.Find(id);
            if (exitProduct == null)
            {
                return HttpNotFound();
            }
            return View(exitProduct);
        }

        // POST: ExitProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExitProduct exitProduct = db.ExitProducts.Find(id);
            db.ExitProducts.Remove(exitProduct);
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
