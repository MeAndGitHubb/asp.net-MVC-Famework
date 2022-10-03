using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BanBanh
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            /*Giỏ hàng*/
            routes.MapRoute(
                name: "TatCaSanPham",
                url: "tat-ca-san-pham",
                defaults: new { controller = "Site", action = "Product", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "TatCaBaiViet",
                url: "tat-ca-bai-viet",
                defaults: new { controller = "Site", action = "Post", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ChiTietSP",
                url: "chi-tiet",
                defaults: new { controller = "Site", action = "ProductDetail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "LienHe",
                url: "lien-he",
                defaults: new { controller = "Lienhe", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "GioHang",
               url: "gio-hang",
               defaults: new { controller = "Giohang", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "DKThanhCong",
              url: "dang-ky-thanh-cong",
              defaults: new { controller = "Khachhang", action = "DangKyThanhCong", id = UrlParameter.Optional }
          );
            routes.MapRoute(
               name: "ThanhToan",
               url: "thanh-toan",
               defaults: new { controller = "Giohang", action = "ThanhToan", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "ThanhCong",
              url: "thanh-cong",
              defaults: new { controller = "Giohang", action = "ThanhCong", id = UrlParameter.Optional }
          );
            routes.MapRoute(
             name: "TimKiem",
             url: "search",
             defaults: new { controller = "Module", action = "Search", id = UrlParameter.Optional }
         );
            routes.MapRoute(
              name: "DangNhap",
              url: "dang-nhap",
              defaults: new { controller = "Khachhang", action = "DangNhap", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "DangXuat",
              url: "dang-xuat",
              defaults: new { controller = "Khachhang", action = "DangXuat", id = UrlParameter.Optional }
          );
            routes.MapRoute(
             name: "DangKy",
             url: "dang-ky",
             defaults: new { controller = "Khachhang", action = "DangKy", id = UrlParameter.Optional }
         );
            //khai bao url động
            routes.MapRoute(
              name: "SiteSlug",
              url: "{slug}",
              defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
