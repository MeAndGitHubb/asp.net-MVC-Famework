using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;
namespace BanBanh.Controllers
{
    public class GiohangController : Controller
    {
        ProductDAO productDAO = new ProductDAO();
        UserDAO userDAO = new UserDAO();
        OrderDAO orderDAO = new OrderDAO();
        OrderDetailDAO orderDetailDAO = new OrderDetailDAO();
        XCart xcart = new XCart();
        // GET: Cart
        public ActionResult Index()
        {
            List<CartItem> listcart = xcart.getCart();
            return View("Index", listcart);
        }
        public ActionResult CartAdd(int productid)
        {
            Product product = productDAO.getRow(productid);
            CartItem cartitem = new CartItem(product.Id, product.Name, product.Img, product.Price, 1);
            List<CartItem> listcart = xcart.AddCart(cartitem);
            return RedirectToAction("Index","Giohang");
        }
        public ActionResult CartDel (int productid)
        {
            xcart.DelCart(productid); 
            return RedirectToAction("Index", "Giohang");
        }
        public ActionResult CartUpdate(FormCollection form)
        {
            if (!string.IsNullOrEmpty(form["CapNhat"]))
            {
                var listqty = form["qty"];
                var listarr = listqty.Split(',');
                xcart.UpdateCart(listarr);

            }
            return RedirectToAction("Index", "Giohang");
        }
        public ActionResult CartDelAll(FormCollection form)
        {
            xcart.DelCart();
            return RedirectToAction("Index", "Giohang");
        }
        // thanh toan
        public ActionResult ThanhToan()
        {
            
            List<CartItem> listcart = xcart.getCart();
            if (Session["UserCustomer"].Equals(""))
            {
                return Redirect("~/dang-nhap");
            }
            int userid = int.Parse(Session["CustomerId"].ToString());// ma nguon dang nhap
            User user = userDAO.getRow(userid);
            ViewBag.user = user;
            return View("ThanhToan", listcart);
        }
        public ActionResult DatMua(FormCollection field)
        {
            //luu thong tin
            //luu thong tin vao csdl
            int userid = int.Parse(Session["CustomerId"].ToString());
            User user = userDAO.getRow(userid);
            //lay thong tin
            String note = field["Note"];
            String receiverEmail = field["ReceiverEmail"];
            String receiverPhone = field["ReceiverPhone"];
            String receiverAddress = field["ReceiverAddress"];
            String receiverName = field["ReceiverName"];
            //tao doi tuong don hang
            Order order = new Order();
            order.UserId = userid;
            order.Note = note;
            order.ReceiverAddress = receiverAddress;
            order.ReceiverEmail = receiverEmail;
            order.ReceiverName = receiverName;
            order.ReceiverPhone = receiverPhone;
            order.Status = 1;
            order.Created_At = DateTime.Now;
            if (orderDAO.Insert(order) == 1)
            {
                //them chi tiet don hang
                List<CartItem> listcart = xcart.getCart();
                foreach (CartItem cartItem in listcart)
                {
                    OrderDetail orderdetail = new OrderDetail();
                    orderdetail.OrderId = order.Id;
                    orderdetail.ProductId = cartItem.ProductId;
                    orderdetail.Price = cartItem.Price;
                    orderdetail.Number = cartItem.Qty;
                    orderdetail.Amount = cartItem.Amount;
                    orderDetailDAO.Insert(orderdetail);
                }
            }
            return Redirect("~/thanh-cong");
        }
        public ActionResult ThanhCong()
        {
            xcart.DelCart();
            return View("ThanhCong");
        }
    }
}
