using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanBanh.Library
{
    public static class Notification
    {
        public class NotificationModel
        {
            public string msg { get; set; }
            public String msg_type { get; set; }
        }

        public static bool has_flash()
        {
            if (System.Web.HttpContext.Current.Session["Notification"].Equals(""))
            {
                return false;
            }
            return true;
        }
        public static void set_flash(String msg, String msg_type)
        {
            NotificationModel notify = new NotificationModel();
            notify.msg = msg;
            notify.msg_type = msg_type;
            System.Web.HttpContext.Current.Session["Notification"] = notify;
        }
        public static NotificationModel get_flash()
        {
            NotificationModel notify = (NotificationModel)System.Web.HttpContext.Current.Session["Notification"];
            System.Web.HttpContext.Current.Session["Notification"] = "";
            return notify;
        }
    }
}