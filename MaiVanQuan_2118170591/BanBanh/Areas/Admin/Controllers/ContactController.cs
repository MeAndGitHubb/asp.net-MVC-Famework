using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;

namespace BanBanh.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        ContactDAO contactDAO = new ContactDAO();

        // GET: Admin/Contact
        public ActionResult Index()
        {
            return View(contactDAO.getList("Index"));
        }

        public ActionResult Trash()
        {
            return View(contactDAO.getList("Trash"));
        }

        // GET: Admin/Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact Contact = contactDAO.getRow(id);
            if (Contact == null)
            {
                return HttpNotFound();
            }
            return View(Contact);
        }

        // GET: Admin/Contact/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(contactDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(contactDAO.getList("Index"), "Order", "Name", 0);
            return View();
        }

        // POST: Admin/Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact Contact)
        {
            if (ModelState.IsValid)
            {

                Contact.FullName = XString.Str_Slug(Contact.FullName);
                Contact.Created_At = DateTime.Now;

                Contact.Updated_At = DateTime.Now;
                contactDAO.Insert(Contact);
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(contactDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(contactDAO.getList("Index"), "Order", "Name", 0);
            return View(Contact);
        }

        // GET: Admin/Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact Contact = contactDAO.getRow(id);
            if (Contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCat = new SelectList(contactDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(contactDAO.getList("Index"), "Order", "Name", 0);
            return View(Contact);
        }

        // POST: Admin/Contact/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact Contact)
        {
            if (ModelState.IsValid)
            {

                Contact.FullName = XString.Str_Slug(Contact.FullName);
                Contact.Created_At = DateTime.Now;

                Contact.Updated_At = DateTime.Now;

                contactDAO.Update(Contact);
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(contactDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(contactDAO.getList("Index"), "Order", "Name", 0);
            return View(Contact);
        }

        // GET: Admin/Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = contactDAO.getRow(id);
            contactDAO.Delete(contact);
            return RedirectToAction("Index");
        }
        // GET: Admin/Contact/status/5
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Contact");
            }
            Contact contact = contactDAO.getRow(id);
            if (contact == null)
            {
                return RedirectToAction("Index", "Contact");
            }
            contact.Status = (contact.Status == 1) ? 2 : 1;
            contact.Updated_By = Convert.ToInt32(Session["UserId"]);
            contact.Updated_At = DateTime.Now;
            contactDAO.Update(contact);
            return RedirectToAction("Index", "Contact");
        }
        
    }
}
