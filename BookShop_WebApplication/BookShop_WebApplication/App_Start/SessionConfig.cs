using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookShop_WebApplication.Models;
namespace BookShop_WebApplication.App_Start
{
    public class SessionConfig
    {
        public static void SetUser(CLIENT user)
        {
            HttpContext.Current.Session["user"] = user;
        }
        public static CLIENT GetUser()
        {
            return (CLIENT)HttpContext.Current.Session["user"];

        }
        public static void SetUserId(int userId)
        {
            HttpContext.Current.Session["user"] = userId;
        }

        public static int? GetUserId()
        {
            return HttpContext.Current.Session["user"] as int?;
        }
        public static void SetAdmin(USER user)
        {
            HttpContext.Current.Session["admin"] = user;
        }
        public static USER GetAdmin()
        {
            return (USER)HttpContext.Current.Session["admin"];

        }
    }
}