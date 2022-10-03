using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using System.Net;
using System.IO;

namespace BanBanh.Areas.Admin.Controllers
{
    public class PageController : BaseController
    {
        // GET: Admin/Post
        private PostDAO postDAO = new PostDAO();
        private LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/Post
        public ActionResult Index()
        {
            List<Post> list = postDAO.getList("Index", "Page");
            return View("Index", list);
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
                post.PostType = "Page";
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
                if (postDAO.Insert(post) == 1)
                {
                    Link link = new Link();
                    link.Slug = post.Slug;
                    link.TableId = post.Id;
                    link.Type = "page";
                    linkDAO.Insert(link);
                }
                TempData["message"] = new XMessage("success", "Thêm Thành công");
                return RedirectToAction("Index");
            }
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
                post.PostType = "Page";
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
                if (postDAO.Update(post) == 1)
                {
                    Link link = linkDAO.getRow(post.Id, "page");
                    link.Slug = post.Slug;
                    linkDAO.Update(link);
                }

                return RedirectToAction("Index");
            }
            TempData["message"] = new XMessage("success", "Cập nhật thành công");
            return View(post);
        }

        // GET: Admin/Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Page");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Page");
            }
            if (post.Status != 0)
            {
                TempData["message"] = new XMessage("danger", "chỉ xóa mẫu tin trong thùng rác");
                return RedirectToAction("Trash", "Page");
            }
            return View(post);
        }

        // POST: Admin/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = postDAO.getRow(id);
            if (postDAO.Delete(post) == 1)
            {
                Link link = linkDAO.getRow(post.Id, "page");
                link.Slug = post.Slug;
                linkDAO.Delete(link);
            }
            return RedirectToAction("Trash", "Page");
        }
        public ActionResult Trash()
        {
            List<Post> list = postDAO.getList("Trash", "Page");
            return View("Trash", list);
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Page");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            post.Status = (post.Status == 1) ? 2 : 1;
            post.Updated_At = DateTime.Now;
            post.Created_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "khôi phục thành công");
            return RedirectToAction("Index", "Page");
        }
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            post.Status = 0;
            post.Updated_At = DateTime.Now;
            post.Created_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Page");
        }
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Page");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "mẫu tin không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            post.Status = 2;
            post.Updated_At = DateTime.Now;
            post.Created_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "thành công");
            return RedirectToAction("Trash", "Page");
        }

    }
}
