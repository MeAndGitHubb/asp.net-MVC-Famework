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

namespace BanBanh.Controllers
{
    public class DangKyController : Controller
    {
        private UserDAO userDAO = new UserDAO();
        OrderDAO orderDAO = new OrderDAO();
        // GET: Admin/User
        public ActionResult Index()
        {
            List<User> list = userDAO.getList("Index");
            return View("Index", list);
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            return View("Index", "Site");
        }

        // POST: Admin/User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {

            if (ModelState.IsValid)
            {
                user.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                user.CreatedAt = DateTime.Now;
                userDAO.Insert(user);
                TempData["message"] = new XMessage("success", "Đăng Ký Thành công");
                return RedirectToAction("Index", "Site");
            }
            return View(user);
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {

            if (ModelState.IsValid)
            {
                user.UpdatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                user.UpdatedAt = DateTime.Now;
                userDAO.Update(user);
                TempData["message"] = new XMessage("success", "Thêm Thành công");
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Trash", "User");
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Trash", "User");
            }
            if (user.Status != 0)
            {
                TempData["message"] = new XMessage("danger", "chỉ xóa mẫu tin trong thùng rác");
                return RedirectToAction("Trash", "User");
            }
            return View(user);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = userDAO.getRow(id);
            userDAO.Delete(user);
            return RedirectToAction("Trash", "User");
        }
        public ActionResult Trash()
        {
            List<User> list = userDAO.getList("Trash");
            return View("Trash", list);
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Trash", "User");
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Index", "User");
            }
            user.Status = (user.Status == 1) ? 2 : 1;
            user.UpdatedAt = DateTime.Now;
            user.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            userDAO.Update(user);
            TempData["message"] = new XMessage("success", "khôi phục thành công");
            return RedirectToAction("Index", "User");
        }
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Index", "User");
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Index", "User");
            }
            user.Status = 0;
            user.UpdatedAt = DateTime.Now;
            user.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            userDAO.Update(user);
            TempData["message"] = new XMessage("success", "xóa vào thùng rác thành công");
            return RedirectToAction("Index", "User");
        }
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Trash", "User");
            }
            User user = userDAO.getRow(id);
            if (user == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Index", "User");
            }
            user.Status = 2;
            user.UpdatedAt = DateTime.Now;
            user.CreatedBy = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            userDAO.Update(user);
            TempData["message"] = new XMessage("success", "thành công");
            return RedirectToAction("Trash", "User");
        }
       
    }
}
