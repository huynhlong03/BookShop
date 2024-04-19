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
    public class PublisherManagementController : Controller
    {
        QL_SACHEntities db = new QL_SACHEntities();
        // GET: Admin/Publisher
        public ActionResult Index(int? page, int? pagesize)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pagesize == null)
            {
                pagesize = 8;
            }
            ViewBag.PageSize = pagesize;
            var listbook = db.PUBLISHERs.ToList();
            return View(listbook.ToPagedList((int)page, (int)pagesize));
        }
        public ActionResult AddPublisher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPublisher(PUBLISHER model)
        {
            if (ModelState.IsValid)
            {
                PUBLISHER a = new PUBLISHER();
                a.PublisherName = model.PublisherName;
                a.PublisherAddress = model.PublisherAddress;
                a.PublisherPhone = model.PublisherPhone;
                db.PUBLISHERs.Add(a);
                db.SaveChanges();
                ViewBag.Create = "Create successfully!";
                return View();
            }
            else
            {
                ModelState.AddModelError("New error", "Invalid Data");
                return View();
            }

        }
        public ActionResult EditPublisher(int publisherId)
        {
            var edit = db.PUBLISHERs.Find(publisherId);
            //db.SaveChanges();
            return View(edit);
        }
        [HttpPost]
        public ActionResult EditPublisher(PUBLISHER model)
        {
            if (ModelState.IsValid)
            {
                var edit = db.PUBLISHERs.Find(model.PublisherID);
                edit.PublisherName = model.PublisherName;
                edit.PublisherAddress = model.PublisherAddress;
                edit.PublisherPhone = model.PublisherPhone;
                db.SaveChanges();
                ViewBag.Edit = "Edit successfully!";
                return View(model);

            }
            else
            {
                ModelState.AddModelError("New error", "Invalid Data");
                return View(model);
            }
        }
        public ActionResult DetailPublisher(int publisherId)
        {
            var a = db.PUBLISHERs.FirstOrDefault(s => s.PublisherID == publisherId);
            if (a == null)
                return HttpNotFound();
            return View(a);

        }
        public ActionResult DeletePublisher(int publisherId)
        {
            if (db.BOOKs.Any(b => b.PublisherID == publisherId))
            {
                // Nếu có sách liên quan, hiển thị thông báo và không thực hiện xóa
                TempData["ErrorMessage"] = "Cannot delete this publisher!";
                return RedirectToAction("Index"); // Hoặc chuyển hướng về trang nào đó
            }
            var a = db.CATEGORies.Find(publisherId);
            if (a != null)
            {
                db.CATEGORies.Remove(a);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
            //var delete = db.CATEGORies.Find(categoryId);
            //db.CATEGORies.Remove(delete);
            //db.SaveChanges();
            //return RedirectToAction("Index", "CategoryManagement");
        }
    }
}