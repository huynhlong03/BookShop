using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop_WebApplication.Models;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using BookShop_WebApplication.App_Start;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BookShop_WebApplication.Controllers
{

    public class HomeController : Controller
    {
        QL_SACHEntities db = new QL_SACHEntities();
        // GET: Home
        public ActionResult Index()
        {
            string UserName = Cookie.get("User-book");
            string PassWord = Cookie.get("PassWord-book");
            var user = db.CLIENTs.FirstOrDefault(t => t.UserName == UserName & t.Password == PassWord);
            if (user != null)
            {
                SessionConfig.SetUser(user);
            }
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Library()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(CLIENT cl)
        {
            if (ModelState.IsValid)
            {
                if (db.CLIENTs.Any(t => t.UserName == cl.UserName))
                {
                    ViewBag.thongbao = "Đã tồn tại tên đăng nhập";
                    return View();
                }
                else
                {
                    SessionConfig.SetUser(cl);
                    db.CLIENTs.Add(cl);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");

                }
            }
            
            else
            {
                return View(cl);
            }

        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string username = f["Username"];
            string password = f["Password"];
            //string admin = "Admin";

            var admin = db.USERs.FirstOrDefault(t => t.Username == username && t.Password == password);
            var user = db.CLIENTs.FirstOrDefault(t => t.UserName == username && t.Password == password);
            if (admin != null)
            {
               
                // Redirect to the Index action of HomeAdmin controller
                SessionConfig.SetAdmin(admin);
                return RedirectToRoute("HomeAdmin");
            }
            else if (user != null)
            {
                    // Set the clientId in the session or cookie along with the user information
                    SessionConfig.SetUser(user);
                    Cookie.Create("User-book", user.UserName, DateTime.Now.AddDays(10));
                    Cookie.Create("PassWord-book", user.Password, DateTime.Now.AddDays(10));

                    return RedirectToAction("Booklist", "Book");   
            }
            ViewBag.thongbao = "Username and Password incorrect!";
            return View();

        }
        public ActionResult Logout()
        {
            SessionConfig.SetUser(null);
            SessionConfig.SetAdmin(null);
            Cookie.Remove("User-book");
            Cookie.Remove("PassWord-book");
            return RedirectToAction("Login", "Home");
        }
    }
}