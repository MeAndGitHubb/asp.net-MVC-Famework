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
    public class GiftController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Admin/Gift
        public ActionResult Index()
        {
            return View(db.Gifts.ToList());
        }

        // GET: Admin/Gift/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gift gift = db.Gifts.Find(id);
            if (gift == null)
            {
                return HttpNotFound();
            }
            return View(gift);
        }

        // GET: Admin/Gift/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Gift/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Phone,Address,Email,Amount,Status")] Gift gift)
        {
            if (ModelState.IsValid)
            {
                db.Gifts.Add(gift);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gift);
        }

        // GET: Admin/Gift/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gift gift = db.Gifts.Find(id);
            if (gift == null)
            {
                return HttpNotFound();
            }
            return View(gift);
        }

        // POST: Admin/Gift/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,Address,Email,Amount,Status")] Gift gift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gift).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gift);
        }

        // GET: Admin/Gift/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gift gift = db.Gifts.Find(id);
            if (gift == null)
            {
                return HttpNotFound();
            }
            return View(gift);
        }

        // POST: Admin/Gift/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gift gift = db.Gifts.Find(id);
            db.Gifts.Remove(gift);
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
