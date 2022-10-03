using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;

namespace BanBanh.Controllers
{
    public class TimkiemController : Controller
    {
        // GET: Search
        MyDBContext db = new MyDBContext();
        public ActionResult KQTimKiem(string sTuKhoa)
        {
            var lstSP = db.Products.Where(n => n.Name.Contains(sTuKhoa));
            return View(lstSP.OrderBy(n=>n.Name));
        }
    }
}