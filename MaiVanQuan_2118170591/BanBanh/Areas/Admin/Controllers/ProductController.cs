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
    public class ProductController : BaseController
    {
        ProductDAO productDAO = new ProductDAO();
        SupplierDAO supplierDAO = new SupplierDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(productDAO.getList("Index"));
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            //ViewBag.ListSupp = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Product product)
        {

            if (ModelState.IsValid)
            {
                product.Slug = XString.Str_Slug(product.Name);
                product.Created_By = Session["UserId"].Equals("") ? 1 : int.Parse(Session["UserId"].ToString());
                product.Created_At = DateTime.Now;
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)
                {
                    string[] FileExtensions = new string[] { ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = product.Slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        string pathDir = "~/Public/images/products/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //upload file
                        fileImg.SaveAs(pathImg);
                        //luu hinh vao csdl
                        product.Img = imgName;
                    }

                }
                productDAO.Insert(product);
                TempData["message"] = new XMessage("success", "Thêm Thành công");
                return RedirectToAction("Index");
            }

            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);

            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {

            if (ModelState.IsValid)
            {
                product.Slug = XString.Str_Slug(product.Name);
                product.Updated_At = DateTime.Now;
                product.Updated_By = (Session["UserId"].Equals("")) ? 1 : int.Parse(Session["UserId"].ToString());
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)
                {
                    string[] FileExtensions = new string[] { ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = product.Slug + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        string pathDir = "~/Public/images/products/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //xoa hinh cu~
                        if (product.Img != null)
                        {
                            string pathImgDelete = Path.Combine(Server.MapPath(pathDir), product.Img);
                            System.IO.File.Delete(pathImgDelete);
                        }

                        //upload file
                        fileImg.SaveAs(pathImg);
                        //luu hinh vao csdl
                        product.Img = imgName;
                    }

                }
                productDAO.Update(product);
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListProduct = new SelectList(productDAO.getList("Index"), "Id", "Name", 0);
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productDAO.getRow(id);
            productDAO.Delete(product);
            TempData["message"] = new XMessage("success ", "Xoá mẫu tin thành công");
            return RedirectToAction("Trash", "Product");
        }
        public ActionResult Trash()
        {
            return View(productDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger ", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger ", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = (product.Status == 1) ? 2 : 1;
            product.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            product.Updated_At = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success ", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger ", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger ", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = 0;// trang thai rac
            product.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            product.Updated_At = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success ", "Xoá vào thùng rác thành công");
            return RedirectToAction("Index", "Product");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger ", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger ", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            product.Status = 2;// trang thai rac
            product.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            product.Updated_At = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success ", "Khôi phục thành công");
            return RedirectToAction("Trash", "Product");
        }
        public String ProductImg(int? productid)
        {
            Product product = productDAO.getRow(productid);
            if (productid == null)
            {
                return "";
            }
            else
            {
                return product.Img;
            }
        }
        public String ProductName(int? productid)
        {
            Product product = productDAO.getRow(productid);
            if (productid == null)
            {
                return "";
            }
            else
            {
                return product.Name;
            }
        }
    }
}
