using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace BanBanh.Controllers
{
    public class KhachhangController : Controller
    {
        UserDAO userDAO = new UserDAO();
        // GET: KhachHang
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection filed)
        {
            String username = filed["username"];
            String password = XString.ToMD5(filed["password"]);

            User row_user = userDAO.getRow(username, "Customer");
            String strError = "";
            if (row_user == null)
            {
                strError = "Tên đang nhập không tồn tại";
            }
            else
            {
                if (password.Equals(row_user.Password))
                {
                    Session["UserCustomer"] = username;
                    Session["CustomerId"] = row_user.Id;
                    return Redirect("~/");
                }
                else
                {
                    strError = password;
                }
            }
            ViewBag.Error = "<span class='text-danger'> " + strError + "</div>";
            return View("DangNhap");
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(User user)
        {

            if (ModelState.IsValid)
            {
                user.CreatedAt = DateTime.Now;
                user.Roles = "Customer";
                user.Status = 1;
                if (user.Password == user.ConfimPassword)
                {
                    userDAO.Insert(user);
                    TempData["message"] = new XMessage("success", "Thêm Thành công");
                    return Redirect("~/");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Xác nhận mật khẩu không đúng");
                    return Redirect("~/");
                }

            }
            return View(user);
        }
        public ActionResult DangKyThanhCong()
        {
            return View("DangKyThanhCong");
        }
        public ActionResult DangXuat()
        {
            Session["UserCustomer"] = "";
            Session["CustomerId"] = "";
            return Redirect("~/");
        }
    }

}