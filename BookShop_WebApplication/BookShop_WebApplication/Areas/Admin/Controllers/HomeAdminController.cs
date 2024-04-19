using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop_WebApplication.Filters;
using BookShop_WebApplication.Models;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using BookShop_WebApplication.App_Start;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BookShop_WebApplication.Areas.Admin.Controllers
{
    [AdminAuthorization]
    [RouteArea("Admin")]
    public class HomeAdminController : Controller
    {
        // GET: Admin/Home
        QL_SACHEntities db = new QL_SACHEntities();
        public ActionResult Index()
        {
            var dt = db.ORDER_DETAIL.Sum(d => d.Price);
            var sl = db.ORDER_DETAIL.Sum(d => d.Quantity);
            var order = db.ORDERs.Select(or => or.OrderID).Count();
            var cl = db.ORDERs.Select(c => c.ClientID).Distinct().Count();
            ViewBag.tongdt = dt;
            ViewBag.tongsl = sl;
            ViewBag.slorder = order;
            ViewBag.tongcl = cl;
            var productbook = db.ORDER_DETAIL
        .GroupBy(ct => ct.BookID)
        .Select(group => new DoanhThu
        {
            BookID = group.Key,
            SoLuongBanChay = (int)group.Sum(ct => ct.Quantity),
            TenSanPham = db.BOOKs.FirstOrDefault(sp => sp.BookID == group.Key).BookName,
            HinhAnh = db.BOOKs.FirstOrDefault(sp => sp.BookID == group.Key).Image,

        })
        .OrderByDescending(result => result.SoLuongBanChay)
        .Take(10)
        .ToList();

            return View(productbook);
            return View();
        }
      
    }
}