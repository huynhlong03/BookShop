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
    public class CategoryManagementController : Controller
    {
        QL_SACHEntities db = new QL_SACHEntities();
        // GET: Admin/CategoryManagement
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
            var listbook = db.CATEGORies.ToList();
            return View(listbook.ToPagedList((int)page, (int)pagesize));
        }
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(CATEGORY model)
        {
            if (ModelState.IsValid)
            {
                CATEGORY a = new CATEGORY();
                a.CategoryName = model.CategoryName;
               
                db.CATEGORies.Add(a);
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
        public ActionResult EditCategory(int categoryId)
        {
            var edit = db.CATEGORies.Find(categoryId);
            //db.SaveChanges();
            return View(edit);
        }
        [HttpPost]
        public ActionResult EditCategory(CATEGORY model)
        {
            if (ModelState.IsValid)
            {
                var edit = db.CATEGORies.Find(model.CategoryID);
                edit.CategoryName = model.CategoryName;
                
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
        public ActionResult DeleteCategory(int categoryId)
        {
            if (db.BOOKs.Any(b => b.CategoryID == categoryId))
            {
                // Nếu có sách liên quan, hiển thị thông báo và không thực hiện xóa
                TempData["ErrorMessage"] = "Cannot delete this category!.";
                return RedirectToAction("Index"); // Hoặc chuyển hướng về trang nào đó
            }
            var category = db.CATEGORies.Find(categoryId);
            if (category != null)
            {
                db.CATEGORies.Remove(category);
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