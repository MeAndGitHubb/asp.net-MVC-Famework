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
    public class HotController : BaseController
    {
        private MyDBContext db = new MyDBContext();

        // GET: Admin/Hot
        public ActionResult Index()
        {
            return View(db.Hots.ToList());
        }

        // GET: Admin/Hot/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hot hot = db.Hots.Find(id);
            if (hot == null)
            {
                return HttpNotFound();
            }
            return View(hot);
        }

        // GET: Admin/Hot/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Hot/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,ProductId,Imgs,Price,Amount,Status")] Hot hot)
        {
            if (ModelState.IsValid)
            {
                db.Hots.Add(hot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hot);
        }

        // GET: Admin/Hot/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hot hot = db.Hots.Find(id);
            if (hot == null)
            {
                return HttpNotFound();
            }
            return View(hot);
        }

        // POST: Admin/Hot/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,ProductId,Imgs,Price,Amount,Status")] Hot hot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hot);
        }

        // GET: Admin/Hot/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hot hot = db.Hots.Find(id);
            if (hot == null)
            {
                return HttpNotFound();
            }
            return View(hot);
        }

        // POST: Admin/Hot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hot hot = db.Hots.Find(id);
            db.Hots.Remove(hot);
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
