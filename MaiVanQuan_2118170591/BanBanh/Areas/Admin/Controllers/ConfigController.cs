using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace BanBanh.Areas.Admin.Controllers
{
    public class ConfigController : BaseController
    {
        private MyDBContext db = new MyDBContext();
        ConfigDAO configDAO = new ConfigDAO();
        // GET: Admin/Config
        public ActionResult Index()
        {
            return View(configDAO.getList("Index"));
        }

        // GET: Admin/Config/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = configDAO.getRow(id);
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        // GET: Admin/Config/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Config/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Config config)
        {
            if (ModelState.IsValid)
            {
                configDAO.Update(config);
                return RedirectToAction("Index");
            }
            config.Value = XString.Str_Slug(config.Name);
            ViewBag.ListCat = new SelectList(configDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(configDAO.getList("Index"), "Orders", "Name", 0);
            return RedirectToAction("Index", "Config");
        }

        // GET: Admin/Config/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = configDAO.getRow(id);
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        // POST: Admin/Config/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Config config)
        {
            if (ModelState.IsValid)
            {
                
                configDAO.Update(config);
                TempData["message"] = new XMessage("success ", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            config.Value = XString.Str_Slug(config.Name);
            configDAO.Update(config);
            ViewBag.ListCat = new SelectList(configDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(configDAO.getList("Index"), "Orders", "Name", 0);
            return View(config);
        }

        // GET: Admin/Config/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = configDAO.getRow(id);
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Config config = configDAO.getRow(id);
            configDAO.Delete(config);
            TempData["message"] = new XMessage("success ", "Xoá mẫu tin thành công");
            return RedirectToAction("Trash", "Config");
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Config");
            }
            Config config = configDAO.getRow(id);
            if (config == null)
            {
                return RedirectToAction("Index", "Config");
            }
            config.Id = (config.Id == 1) ? 2 : 1;
            config.Id = Convert.ToInt32(Session["UserId"]);          
            configDAO.Update(config);
            return RedirectToAction("Index", "Config");
        }


    }
}
