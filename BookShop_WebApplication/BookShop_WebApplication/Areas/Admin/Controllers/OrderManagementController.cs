using BookShop_WebApplication.Filters;
using BookShop_WebApplication.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop_WebApplication.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class OrderManagementController : Controller
    {
        QL_SACHEntities db = new QL_SACHEntities();
        // GET: Admin/BookManagement
        public ActionResult Index(int? page, int? pagesize)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pagesize == null)
            {
                pagesize = 12;
            }
            ViewBag.PageSize = pagesize;
            var listbook = db.ORDERs.OrderByDescending(x => x.OrderID).ToList();
            return View(listbook.ToPagedList((int)page, (int)pagesize));
        }
        [HttpPost]
        public ActionResult UpdateDeliveryStatus(int orderId)
        {
            // Lấy đơn hàng từ database dựa trên orderId
            ORDER order = db.ORDERs.SingleOrDefault(t => t.OrderID == orderId);// Lấy dữ liệu từ database

            // Cập nhật trạng thái giao hàng
            order.Status = "Đang giao";
            order.DeliveryDate = DateTime.Now;
            // Lưu thay đổi vào database
            db.SaveChanges();
            // Chuyển hướng về trang danh sách đơn hàng
            return RedirectToAction("Index", "OrderManagement");
        }
        [HttpPost]
            public ActionResult CancelOrder(int orderId)
        {
            // Lấy đơn hàng từ database dựa trên orderId
            ORDER order = db.ORDERs.SingleOrDefault(t => t.OrderID == orderId);// Lấy dữ liệu từ database
            foreach (var item in order.ORDER_DETAIL)
            {
                BOOK book = db.BOOKs.SingleOrDefault(t => t.BookID == item.BookID);
                book.Quanlity += item.Quantity;
            }    
                // Cập nhật trạng thái giao hàng
            order.Status = "Đã hủy đơn hàng";
           // order.DeliveryDate = DateTime.Now;
            // Lưu thay đổi vào database
            db.SaveChanges();
            // Chuyển hướng về trang danh sách đơn hàng
            return RedirectToAction("Index", "OrderManagement");
        }
        public ActionResult DetailOrder(int orderId)
        {
            var a = db.ORDERs.FirstOrDefault(s => s.OrderID == orderId);
            if (a == null)
                return HttpNotFound();
            return View(a);

        }
    }
}