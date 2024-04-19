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
    public class AdminRoleController : Controller
    {
        QL_SACHEntities db = new QL_SACHEntities();
        // GET: Admin/AdminRole
       
        public ActionResult Index()
        {
            return View(db.ROLES.ToList());
        }
        public ActionResult Detail(int roleId)
        {
            var a = db.ROLES.FirstOrDefault(s => s.RoleID == roleId);
            if (a == null)
                return HttpNotFound();
            return View(a);
            
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ROLE model)
        {
            if (ModelState.IsValid)
            {
                ROLE role = new ROLE();
                role.RoleName = model.RoleName;
                role.Description = model.Description;
                db.ROLES.Add(role);
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
        public ActionResult Edit(int roleId)
        {
            var edit = db.ROLES.Find(roleId);
            //db.SaveChanges();
            return View(edit);
        }
        [HttpPost]
        public ActionResult Edit(ROLE model)
        {
            if (ModelState.IsValid)
            {
                var edit = db.ROLES.Find(model.RoleID);
                edit.RoleName = model.RoleName;
                edit.Description = model.Description;
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
        public ActionResult Delete(int roleId)
        {
            if (db.USERs.Any(b => b.RoleID == roleId))
            {
                TempData["ErrorMessage"] = "This role be used in some Account!";
                return RedirectToAction("Index", "AdminRole");
            }    
            else
            {
                var delete = db.ROLES.Find(roleId);
                db.ROLES.Remove(delete);
                db.SaveChanges();
            }    
            
            return RedirectToAction("Index");
        }
    }
}