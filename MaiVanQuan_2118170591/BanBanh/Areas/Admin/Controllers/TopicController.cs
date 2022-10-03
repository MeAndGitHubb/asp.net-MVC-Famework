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
    public class TopicController : BaseController
    {

        TopicDAO topicDAO = new TopicDAO();
        LinkDAO linkDAO = new LinkDAO();
        public ActionResult Index()
        {
            return View(topicDAO.getList("Index"));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = topicDAO.getRow(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(topicDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Topic topic)
        {
            if (ModelState.IsValid)
            {
                // Xu ly them thong tin
                topic.Slug = XString.Str_Slug(topic.Name);
                if (topic.ParentId == null)
                {
                    topic.ParentId = 0;
                }
                if (topic.Orders == null)
                {
                    topic.Orders = 1;
                }
                else
                {
                    topic.Orders += 1;
                }
                topic.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                topic.CreatedAt = DateTime.Now;
                if (topicDAO.Insert(topic) == 1)
                {

                    Link link = new Link();
                    link.Slug = topic.Slug;
                    link.TableId = topic.Id;
                    link.Type = "topic";
                    linkDAO.Insert(link);
                }
                TempData["message"] = new XMessage("success ", "Thêm Thành công");
                return RedirectToAction("Index", "Topic");
            }
            ViewBag.ListCat = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(topicDAO.getList("Index"), "Orders", "Name", 0);
            return View(topic);
        }

        public ActionResult Edit(int? id)
        {
            ViewBag.ListCat = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(topicDAO.getList("Index"), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = topicDAO.getRow(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Topic topic)
        {
            if (ModelState.IsValid)
            {
                topic.Slug = XString.Str_Slug(topic.Name);
                if (topic.ParentId == null)
                {
                    topic.ParentId = 0;
                }
                if (topic.Orders == null)
                {
                    topic.Orders = 1;
                }
                else
                {
                    topic.Orders += 1;
                }
                topic.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                topic.UpdatedAt = DateTime.Now;
                if (topicDAO.Update(topic) == 1)
                {
                    Link link = linkDAO.getRow(topic.Id, "topic");
                    link.Slug = topic.Slug;
                    linkDAO.Update(link);
                }
                TempData["message"] = new XMessage("success ", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(topicDAO.getList("Index"), "Orders", "Name", 0);
            return View(topic);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = topicDAO.getRow(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topic topic = topicDAO.getRow(id);
            Link link = linkDAO.getRow(topic.Id, "topic");
            if (topicDAO.Delete(topic) == 1)
            {
                linkDAO.Delete(link);
            }
            TempData["message"] = new XMessage("success ", "Xoá mẫu tin thành công");
            return RedirectToAction("Trash", "Topic");
        }
        public ActionResult Trash()
        {
            return View(topicDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger ", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Topic");
            }
            Topic topic = topicDAO.getRow(id);
            if (topic == null)
            {
                TempData["message"] = new XMessage("danger ", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Topic");
            }
            topic.Status = (topic.Status == 1) ? 2 : 1;
            topic.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            topic.UpdatedAt = DateTime.Now;
            topicDAO.Update(topic);
            TempData["message"] = new XMessage("success ", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Topic");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger ", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Topic");
            }
            Topic topic = topicDAO.getRow(id);
            if (topic == null)
            {
                TempData["message"] = new XMessage("danger ", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Topic");
            }
            topic.Status = 0;// trang thai rac
            topic.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            topic.UpdatedAt = DateTime.Now;
            topicDAO.Update(topic);
            TempData["message"] = new XMessage("success ", "Xoá vào thùng rác thành công");
            return RedirectToAction("Index", "Topic");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger ", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Topic");
            }
            Topic topic = topicDAO.getRow(id);
            if (topic == null)
            {
                TempData["message"] = new XMessage("danger ", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Topic");
            }
            topic.Status = 2;// trang thai rac
            topic.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            topic.UpdatedAt = DateTime.Now;
            topicDAO.Update(topic);
            TempData["message"] = new XMessage("success ", "Khôi phục thành công");
            return RedirectToAction("Trash", "Topic");
        }
    }
}
