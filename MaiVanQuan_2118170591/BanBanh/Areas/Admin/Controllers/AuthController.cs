using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;

namespace BanBanh.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        private MyDBContext db = new MyDBContext();
        UserDAO userDAO = new UserDAO();
        //GET: Admin/Auth
        public ActionResult Login()
        {
            ViewBag.Error = "";
            return View("login");
        }
        [HttpPost]
        public ActionResult DoLogin(FormCollection field)
        {
            ViewBag.Error = "";
            string username = field["username"];
            string password = field["password"];
            User user = userDAO.getRow(username, "Admin");
            if (user != null)
            {
                if (user.CountError >= 5 && user.Roles != "Admin")
                {
                    ViewBag.Error = "<p class='login-box-msg text danger'>Liên hệ lại người quản lý</p>";
                }
                else
                {
                    if (user.Password.Equals(password))
                    {
                        Session["UserAdmin"] = username;
                        Session["UserId"] = user.Id.ToString();
                        Session["FullName"] = user.Name;
                        Session["Img"] = user.Img;
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        if (user.CountError == null)
                        {
                            user.CountError = 1;
                        }
                        else
                        {
                            user.CountError += 1;
                        }
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.Error = "<p class='login-box-msg text danger'>Mật khẩu không chính xác</p>";
                    }
                }

            }
            else
            {
                ViewBag.Error = "<p class='login-box-msg text danger'>Tài khoản <strong>" + username + "</strong> không tồn tại!</p>";
            }
            return View("Login");
        }
        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            Session["UserId"] = "";
            Session["FullName"] = "";
            Session["Img"] = "";
            return Redirect("~/Admin/login");
        }
    }
}