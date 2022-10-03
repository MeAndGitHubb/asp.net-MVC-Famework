using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BanBanh
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
       protected void Session_Start()
        {
            Session["UserId"] = "1";
            Session["MyCart"] = "";
            Session["UserAdmin"] = "";
            Session["UserCustomer"] = "";
            Session["CustomerId"] = "";
        }
    }
}
