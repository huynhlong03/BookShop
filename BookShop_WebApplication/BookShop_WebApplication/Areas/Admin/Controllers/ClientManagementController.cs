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
    public class ClientManagementController : Controller
    {
        QL_SACHEntities db = new QL_SACHEntities();
        // GET: Admin/ClientManage
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
            var listbook = db.CLIENTs.ToList();
            return View(listbook.ToPagedList((int)page, (int)pagesize));
        }
        public ActionResult DetailClient(int clientId)
        {
            var a = db.CLIENTs.FirstOrDefault(s => s.ClientID == clientId);
            if (a == null)
                return HttpNotFound();
            return View(a);

        }
        public ActionResult EditClient(int clientId)
        {
            var edit = db.CLIENTs.Find(clientId);
            //db.SaveChanges();
            return View(edit);
        }
        [HttpPost]
        public ActionResult EditClient(CLIENT model)
        {
            if (ModelState.IsValid)
            {
                var edit = db.CLIENTs.Find(model.ClientID);
                edit.ClientName = model.ClientName;
                edit.Gender = model.Gender;
                edit.DateOfBirth = model.DateOfBirth;
                edit.PhoneNumber = model.PhoneNumber;
                edit.Email = model.Email;
                edit.Address = model.Address;
                edit.UserName = model.UserName;
                edit.Password = model.Password;
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
        public ActionResult DeleteClient(int clientId)
        {
            var delete = db.CLIENTs.Find(clientId);
            db.CLIENTs.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Index", "ClientManagement");
        }
    }
}