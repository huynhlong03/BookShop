using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop_WebApplication.Models;
using System.Data.SqlClient;
using PagedList;
using BookShop_WebApplication.App_Start;

namespace BookShop_WebApplication.Controllers
{
    public class BookController : Controller
    {
        QL_SACHEntities db = new QL_SACHEntities();

        //GET: Book
        public ActionResult BookList(int? page, int? pagesize)
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
            if (page == null)
            {
                page = 1;
            }
            if (pagesize == null)
            {
                pagesize = 8;
            }
            ViewBag.PageSize = pagesize;
            var listbook = db.BOOKs.ToList();
            return View(listbook.ToPagedList((int)page, (int)pagesize));
        }

        public ActionResult BookDetail(int idBook)
        {
            if (idBook == null)
            {
                return HttpNotFound();
            }
            var book = db.BOOKs.FirstOrDefault(m => m.BookID == idBook);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        public ActionResult SearchBookByCategory(int CateId)
        {
            var list = db.BOOKs.Where(s => s.CategoryID == CateId).ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult SearchBook(string searchInput)
        {


            var result = db.BOOKs.Where(t => t.BookName.Contains(searchInput)).ToList();
            if (string.IsNullOrEmpty(searchInput) && result.Count == 0)
            {
                ViewBag.Notice = "No product found!";
                return View(result);
            }
            else
            {
                ViewBag.Notice = "Find " + result.Count + " products!";
                return View(result);
            }
        }
    }
}