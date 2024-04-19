using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop_WebApplication.Filters;
using BookShop_WebApplication.Models;
using PagedList;

namespace BookShop_WebApplication.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class BookManagementController : Controller
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
                pagesize = 8;
            }
            ViewBag.PageSize = pagesize;
            var listbook = db.BOOKs.ToList();
            return View(listbook.ToPagedList((int)page, (int)pagesize));
        }
       
        public ActionResult AddBook()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddBook(BOOK model, HttpPostedFileBase fileUpLoad)
        {
            if (fileUpLoad == null)
            {
                ViewBag.Notice = "Please choose an image!";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var filename = Path.GetFileName(fileUpLoad.FileName);
                var path = Path.Combine(Server.MapPath("~/assets/product-img"), filename);

                if (System.IO.File.Exists(path))
                {
                    ViewBag.Notice = "This image already exists!";
                    return View(model);
                }
                else
                {
                    fileUpLoad.SaveAs(path);
                }

                model.Image = fileUpLoad.FileName;
                db.BOOKs.Add(model);
                db.SaveChanges();

                ViewBag.a = "Add book successfully!";
               
            }

            return View(model);
        }
        public ActionResult EditBook(int bookId)
        {
            // Lấy sách từ database dựa trên ID
            BOOK book = db.BOOKs.Find(bookId);

            if (book == null)
            {
                return HttpNotFound(); // Trả về 404 nếu sách không tồn tại
            }

            return View(book);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditBook(BOOK model, HttpPostedFileBase fileUpLoad)
        {

            if (ModelState.IsValid)
            {

                BOOK a = db.BOOKs.SingleOrDefault(t => t.BookID == model.BookID);
                // Update sách trong database
                a.BookName = model.BookName;
                a.Price = model.Price;
                a.Decription = model.Decription;
                a.UpdateDate = model.UpdateDate;
                a.Quanlity = model.Quanlity;
                a.PUBLISHER = model.PUBLISHER;
                a.CATEGORY = model.CATEGORY;

                
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
        public ActionResult DetailBook(int bookId)
        {
            var a = db.BOOKs.FirstOrDefault(s => s.BookID == bookId);
            if (a == null)
                return HttpNotFound();
            return View(a);

        }
        public ActionResult DeleteBook(int bookId)
        {
            var delete = db.BOOKs.Find(bookId);
            if (db.ORDER_DETAIL.Any(b => b.BookID == bookId))
            {
                // Nếu có sách liên quan, hiển thị thông báo và không thực hiện xóa
                TempData["ErrorMessage"] = "Books cannot be removed during shipping!";
                return RedirectToAction("Index", "BookManagement"); // Hoặc chuyển hướng về trang nào đó
            }
            else
            {
                if (delete != null && delete.Quanlity == 0)
                {
                    // Lấy đường dẫn của ảnh
                    var imagePath = Path.Combine(Server.MapPath("~/assets/product-img"), delete.Image);

                    // Kiểm tra xem tệp tin ảnh có tồn tại không trước khi xóa
                    if (System.IO.File.Exists(imagePath))
                    {
                        // Xóa tệp tin ảnh
                        System.IO.File.Delete(imagePath);
                    }

                    // Xóa sách từ database
                    db.BOOKs.Remove(delete);
                    db.SaveChanges();
                }
            }
            // Kiểm tra xem sách có tồn tại không
            
            return RedirectToAction("Index", "BookManagement");
        }
        [HttpPost]
        public ActionResult SearchBook(string searchInput)
        {
            var result = db.BOOKs.Where(t => t.BookName.Contains(searchInput)).ToList();
            if (result.Count == 0)
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