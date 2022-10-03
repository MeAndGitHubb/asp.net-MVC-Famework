using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;

namespace BanBanh.Areas.Admin.Controllers
{
    public class NewYearController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Admin/NewYear
        public ActionResult Index()
        {
            return View(db.NewYears.ToList());
        }

        // GET: Admin/NewYear/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewYear newYear = db.NewYears.Find(id);
            if (newYear == null)
            {
                return HttpNotFound();
            }
            return View(newYear);
        }

        // GET: Admin/NewYear/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NewYear/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Gift,Phome")] NewYear newYear)
        {
            if (ModelState.IsValid)
            {
                db.NewYears.Add(newYear);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newYear);
        }

        // GET: Admin/NewYear/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewYear newYear = db.NewYears.Find(id);
            if (newYear == null)
            {
                return HttpNotFound();
            }
            return View(newYear);
        }

        // POST: Admin/NewYear/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Gift,Phome")] NewYear newYear)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newYear).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newYear);
        }

        // GET: Admin/NewYear/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewYear newYear = db.NewYears.Find(id);
            if (newYear == null)
            {
                return HttpNotFound();
            }
            return View(newYear);
        }

        // POST: Admin/NewYear/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewYear newYear = db.NewYears.Find(id);
            db.NewYears.Remove(newYear);
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
