using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop_WebApplication.Filters;
using BookShop_WebApplication.Models;

namespace BookShop_WebApplication.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class AccountController : Controller
    {
        QL_SACHEntities db = new QL_SACHEntities();
        // GET: Admin/Account
        public ActionResult Index()
        {
            return View(db.USERs.ToList());
        }
        public ActionResult Detail(int accountId)
        {
            var a = db.USERs.FirstOrDefault(s => s.UserID == accountId);
            if (a == null)
                return HttpNotFound();
            return View(a);

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(USER model)
        {
            if (ModelState.IsValid)
            {
                USER user = new USER();
                user.Username = model.Username;
                user.Password = model.Password;
                user.Fullname = model.Fullname;
                user.Email = model.Email;
                user.RoleID = model.RoleID;
                db.USERs.Add(user);
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
        public ActionResult Edit(int accountId)
        {
            var edit = db.USERs.Find(accountId);
            //db.SaveChanges();
            return View(edit);
        }
        [HttpPost]
        public ActionResult Edit(USER model)
        {
            if (ModelState.IsValid)
            {
                var user = db.USERs.Find(model.UserID);
                user.Username = model.Username;
                user.Password = model.Password;
                user.Fullname = model.Fullname;
                user.Email = model.Email;
                user.RoleID = model.RoleID;
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
        public ActionResult Delete(int accountId)
        {
            var delete = db.USERs.Find(accountId);
            if (delete.ROLE.RoleName.Equals("Admin"))
            {
                TempData["ErrorMessage"] = "Cannot remove ADMIN account!";
                return RedirectToAction("Index", "Account");
            }
            else 
            {
              
                db.USERs.Remove(delete);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}