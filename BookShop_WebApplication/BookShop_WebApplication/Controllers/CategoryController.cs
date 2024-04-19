using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop_WebApplication.App_Start;
using BookShop_WebApplication.Models;

namespace BookShop_WebApplication.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
         QL_SACHEntities db = new QL_SACHEntities();
        public ActionResult CategoryPartial()
        {
            string UserName = Cookie.get("User-book");
            string PassWord = Cookie.get("PassWord-book");
            //Cookie.Create("User-Perfume", user.tentaikhoan, DateTime.Now.AddDays(10));
            //Cookie.Create("PassWord-Perfume", user.matkhau, DateTime.Now.AddDays(10));
            var user = db.CLIENTs.FirstOrDefault(t => t.UserName == UserName & t.Password == PassWord);
            if (user != null)
            {
                SessionConfig.SetUser(user);
            }
            var list = db.CATEGORies.Take(7).ToList();
            return View(list);
        }
    }
}