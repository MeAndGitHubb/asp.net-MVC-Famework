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
    public class SliderController : BaseController
    {
        SliderDAO sliderDAO = new SliderDAO();
        // GET: Admin/slider
        public ActionResult Index()
        {
            return View(sliderDAO.getList("Index"));
        }

        // GET: Admin/slider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Admin/slider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/slider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider slider)
        {

            if (ModelState.IsValid)
            {
                slider.Url = XString.Str_Slug(slider.Name);
                slider.Created_By = Session["UserId"].Equals("") ? 1 : int.Parse(Session["UserId"].ToString());
                slider.Created_At = DateTime.Now;
                var fileImg = Request.Files["Img"];
                if (fileImg.ContentLength != 0)
                {
                    string[] FileExtensions = new string[] { ".jpg", ".png", ".gif", ".jepg" };
                    if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
                    {
                        string imgName = slider.Url + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
                        string pathDir = "~/Public/images/products/";
                        string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
                        //upload file
                        fileImg.SaveAs(pathImg);
                        //luu hinh vao csdl
                        slider.Img = imgName;
                    }

                }
                sliderDAO.Insert(slider);
                TempData["message"] = new XMessage("success", "Thêm Thành công");
                return RedirectToAction("Index");
            }


            return View(slider);
        }

        // GET: Admin/slider/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }

        // POST: Admin/slider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {

                if (slider.Orders == null)
                {
                    slider.Orders = 1;
                }
                else
                {
                    slider.Orders += 1;
                }
                var img = Request.Files["img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", "gif" };
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        //upload hinh 
                        string imgName = slider.Name + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        slider.Img = imgName;
                        string PathDir = "~/Public/images/sliders/";
                        string PathFIle = Path.Combine(Server.MapPath(PathDir), imgName);
                        //xoa file
                        if (slider.Img != null)
                        {
                            string DelPath = Path.Combine(Server.MapPath(PathDir), slider.Img);
                            System.IO.File.Delete(DelPath);//xoa hinh

                        }
                        img.SaveAs(PathFIle);

                    }
                }
                slider.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
                slider.Updated_At = DateTime.Now;
                sliderDAO.Update(slider);
               
                TempData["message"] = new XMessage("success ", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListOrder = new SelectList(sliderDAO.getList("Index"), "Orders", "Name", 0);
            return View(slider);
        }

        // GET: Admin/slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListProduct = new SelectList(sliderDAO.getList("Index"), "Id", "Name", 0);
            return View(slider);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = sliderDAO.getRow(id);
            sliderDAO.Delete(slider);
            TempData["message"] = new XMessage("success ", "Xoá mẫu tin thành công");
            return RedirectToAction("Trash", "Slider");
        }
        public ActionResult Trash()
        {
            return View(sliderDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger ", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger ", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            slider.Status = (slider.Status == 1) ? 2 : 1;
            slider.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            slider.Updated_At = DateTime.Now;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success ", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Slider");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger ", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger ", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            slider.Status = 0;// trang thai rac
            slider.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            slider.Updated_At = DateTime.Now;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success ", "Xoá vào thùng rác thành công");
            return RedirectToAction("Index", "Slider");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger ", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger ", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Slider");
            }
            slider.Status = 2;// trang thai rac
            slider.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            slider.Updated_At = DateTime.Now;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success ", "Khôi phục thành công");
            return RedirectToAction("Trash", "Slider");
        }
    }
}
