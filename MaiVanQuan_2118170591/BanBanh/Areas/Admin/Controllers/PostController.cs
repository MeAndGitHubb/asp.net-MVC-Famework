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
using System.IO;

namespace BanBanh.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        private PostDAO postDAO = new PostDAO();
        private TopicDAO topicDAO = new TopicDAO();
        private LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/Post
        public ActionResult Index()
        {
            return View(postDAO.getList("Index", "Post"));
        }

        // GET: Admin/Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            ViewBag.ListTopic = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {

            if (ModelState.IsValid)
            {
                post.PostType = "Post";
                post.Slug = XString.Str_Slug(post.Title);
                post.Created_By = Session["UserId"].Equals("") ? 1 : int.Parse(Session["UserId"].ToString());
                post.Created_At = DateTime.Now;
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)
                {
                    string[] FileExtensions = new string[] { ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = post.Slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        string pathDir = "~/Public/images/posts/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //upload file
                        fileImg.SaveAs(pathImg);
                        //luu hinh vao csdl
                        post.Img = imgName;
                    }

                }
                postDAO.Insert(post);
                TempData["message"] = new XMessage("success", "Thêm Thành công");
                return RedirectToAction("Index");
            }

            ViewBag.ListTopic = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);

            return View(post);
        }

        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListTopic = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // POST: Admin/Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {

            if (ModelState.IsValid)
            {
                post.PostType = "Post";
                post.Slug = XString.Str_Slug(post.Title);
                post.Updated_At = DateTime.Now;
                post.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)
                {
                    string[] FileExtensions = new string[] { ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = post.Slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        string pathDir = "~/Public/images/posts/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //xoa hinh cu~
                        if (post.Img != null)
                        {
                            string pathImgDelete = Path.Combine(Server.MapPath(pathDir), post.Img);
                            System.IO.File.Delete(pathImgDelete);
                        }

                        //upload file
                        fileImg.SaveAs(pathImg);
                        //luu hinh vao csdl
                        post.Img = imgName;
                    }

                }
                postDAO.Update(post);
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            TempData["message"] = new XMessage("success", "Cập nhật thành công");
            ViewBag.ListTopic = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            return View(post);
        }

        // GET: Admin/Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Post");
            }
            if (post.Status != 0)
            {
                TempData["message"] = new XMessage("danger", "chỉ xóa mẫu tin trong thùng rác");
                return RedirectToAction("Trash", "Post");
            }
            return View(post);
        }

        // POST: Admin/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = postDAO.getRow(id);
            postDAO.Delete(post);
            return RedirectToAction("Index");
        }
        public ActionResult Trash()
        {
            return View(postDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            post.Status = (post.Status == 1) ? 2 : 1;
            post.Updated_At = DateTime.Now;
            post.Created_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "khôi phục thành công");
            return RedirectToAction("Index", "Post");
        }
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger ", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger ", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            post.Status = 0;// trang thai rac
            post.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            post.Updated_At = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success ", "Xoá vào thùng rác thành công");
            return RedirectToAction("Index", "Category");
        }
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Post");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Index", "Post");
            }
            post.Status = 2;
            post.Updated_At = DateTime.Now;
            post.Created_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "thành công");
            return RedirectToAction("Trash", "Post");
        }


    }
}
