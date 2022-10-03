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
    public class OrderController : BaseController
    {

        OrderDAO orderDAO = new OrderDAO();
        OrderDetailDAO orderdetailDAO = new OrderDetailDAO();
        ProductDAO productDAO = new ProductDAO();
        // GET: Admin/order
        public ActionResult Index()
        {

            return View(orderDAO.getList("Index"));
        }

        // GET: Admin/order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListChiTiet = orderdetailDAO.getList(id);
            return View(order);
        }

        // GET: Admin/order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                order.Created_At = DateTime.Now;
                TempData["message"] = new XMessage("success ", "Thêm Thành công");
                return RedirectToAction("Index", "order");
            }
            return View(order);
        }

        // GET: Admin/order/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ListCat = new SelectList(orderDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(orderDAO.getList("Index"), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                order.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
                order.Updated_At = DateTime.Now;
               
                TempData["message"] = new XMessage("success ", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Admin/order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = orderDAO.getRow(id);
            
            TempData["message"] = new XMessage("success ", "Xoá mẫu tin thành công");
            return RedirectToAction("Trash", "order");
        }
        public ActionResult Trash()
        {
            return View(orderDAO.getList("Trash"));
        }
        public ActionResult Destroy(int? id)
        {

            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger ", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "order");
            }
            if (order.Status == 1 || order.Status == 2)
            {
                order.Status = 0;
                order.Updated_At = DateTime.Now;
                order.Updated_By = 1;
            }
            else
            {
                if (order.Status == 3)
                {
                    TempData["message"] = new XMessage("danger ", "Đơn hàng đã vận chuyển không thể huỷ");
                }
                if (order.Status == 4)
                {
                    TempData["message"] = new XMessage("danger ", "Đơn hàng đang giao không thể huỷ");
                }
                return RedirectToAction("Index", "order");
            }
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success ", "Hủy đơn hàng thành công");
            return RedirectToAction("Index", "order");
        }
        public ActionResult DaXacMinh(int? id)
        {

            if (id == null)
            {
                TempData["message"] = new XMessage("danger ", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger ", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "order");
            }
            order.Status = 2;// trang thai rac
            order.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            order.Updated_At = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success ", "Đã Xác Minh");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult DangVanChuyen(int? id)
        {

            if (id == null)
            {
                TempData["message"] = new XMessage("danger ", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger ", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "order");
            }
            order.Status = 3;// trang thai rac
            order.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            order.Updated_At = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success ", "Đang Vận Chuyển");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult ThanhCong(int? id)
        {

            if (id == null)
            {
                TempData["message"] = new XMessage("danger ", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger ", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "order");
            }
            order.Status = 4;// trang thai rac
            order.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            order.Updated_At = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success ", "Vận Chuyển thành công");
            return RedirectToAction("Index", "Order");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger ", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger ", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "order");
            }
            order.Status = 0;// trang thai rac
            order.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            order.Updated_At = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success ", "Xoá vào thùng rác thành công");
            return RedirectToAction("Index", "order");
        }
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger ", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger ", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "order");
            }
            order.Status = 2;// trang thai rac
            order.Updated_By = Convert.ToInt32(Session["UserId"].ToString());
            order.Updated_At = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success ", "Khôi phục thành công");
            return RedirectToAction("Trash", "order");
        }
        //
        
    }
}
