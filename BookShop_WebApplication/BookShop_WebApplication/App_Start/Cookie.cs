using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop_WebApplication.App_Start
{
    public static class Cookie
    {
        public static void  Create(string name,string value,DateTime expire)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = value;
            cookie.Expires = expire;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static string get(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie != null)
                return cookie.Value;
            else
                return "";
        }
        public static void Remove(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if(cookie!=null)
            {
                HttpContext.Current.Response.Cookies[name].Expires = DateTime.Now.AddDays(-30);
            }    
            
        }

        internal static object Get(string v)
        {
            throw new NotImplementedException();
        }
    }
}