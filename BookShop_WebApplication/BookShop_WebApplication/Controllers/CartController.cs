using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using BookShop_WebApplication.Models;
using BookShop_WebApplication.Filters;
using BookShop_WebApplication.App_Start;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Text;

namespace BookShop_WebApplication.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart

        QL_SACHEntities db = new QL_SACHEntities();
        public List<CART> GetCartAllItem()
        {
            List<CART> listCart = Session["Cart"] as List<CART>;
            if (listCart == null)
            {
                listCart = new List<CART>();
                Session["Cart"] = listCart;
            }
            return listCart;
        }
       
        public ActionResult AddToCartFromList(int idbook, int quantity)
        {
            List<CART> listCart = GetCartAllItem();
            CART product = listCart.Find(sp => sp.iBookID == idbook);

            if (product == null)
            {
                product = new CART(idbook);
                product.iQuantity = quantity; // Gán giá trị quantity từ input
                listCart.Add(product);
                return RedirectToAction("Cart");
            }
            else
            {
                product.iQuantity += quantity; // Cộng thêm giá trị quantity từ input
                return RedirectToAction("Cart");
            }

        }

        int TotalQuantity()
        {
            int sum = 0;
            List<CART> listCart = Session["Cart"] as List<CART>;

            if (listCart != null)
            {
                sum += listCart.Sum(sp => sp.iQuantity);
            }
            return sum;
        }
        public double TotalAmount()
        {
            double sum = 0;
            List<CART> lstGioHang = Session["Cart"] as List<CART>;
            if (lstGioHang != null)
            {
                sum = sum + lstGioHang.Sum(sp => sp.dTotal);
            }
            return sum;
        }
     

        [MyAuthenFilter]
        public ActionResult Cart()
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
            if (Session["Cart"] == null)
            {
                RedirectToAction("EmptyCart");
            }
            List<CART> listCart = GetCartAllItem();
            if (listCart.Count == 0)
                return RedirectToAction("EmptyCart");
            ViewBag.TotalQuantity = TotalQuantity();
            ViewBag.TotalAmount = TotalAmount();
            return View(listCart);
        }
        public ActionResult CartPartial()
        {
            List<CART> listCart = GetCartAllItem();
            ViewBag.TotalQuantity = TotalQuantity();
            return View();
        }
        public ActionResult EmptyCart()
        {
            return View();
        }
        public ActionResult DeleteCart(int idbook = 0)
        {
            List<CART> listCart = GetCartAllItem();
            CART a = listCart.Find(sp => sp.iBookID == idbook);
            if (a != null)
            {
                listCart.RemoveAll(sp => sp.iBookID == idbook);
                return RedirectToAction("Cart", "Cart");
            }
            if (listCart.Count == 0)
            {
                return RedirectToAction("EmptyCart");
            }
            return RedirectToAction("Cart", "Cart");
        }
        public ActionResult DeleteAllCart()
        {
            List<CART> listCart = GetCartAllItem();
            if (listCart.Count == 0)
            {
                return RedirectToAction("EmptyCart");
            }
            else
            {
                listCart.Clear();
            }
            return RedirectToAction("Cart", "Cart");
        }

        public ActionResult ReduceQuantity(int idbook)
        {
            int s = 0;
            List<CART> listCart = GetCartAllItem();
            CART product = listCart.Find(sp => sp.iBookID == idbook);
           
            if (product != null)
            {
                if (product.iQuantity > 1)
                {
                    s = --product.iQuantity;
                }
                //else
                //{
                //    listCart.Remove(product);
                //}    
            }
            return RedirectToAction("Cart", "Cart");
        }
        public ActionResult IncreaseQuantity(int idbook)
        {
            int s = 0;
            List<CART> listCart = GetCartAllItem();
            CART product = listCart.Find(sp => sp.iBookID == idbook);
            BOOK book = db.BOOKs.SingleOrDefault(a => a.BookID == idbook);
            if (product != null)
            {
                if (product.iQuantity >= 1 && product.iQuantity < book.Quanlity)
                {
                    s = ++product.iQuantity;
                }
                //else
                //{
                //    listCart.Remove(product);
                //}
            }
            return RedirectToAction("Cart", "Cart");
        }


        public ActionResult CheckOut()
        {
            // Kiểm tra xem có Cookie "User" không
            string UserName = Cookie.get("User-book");
            if (UserName == null)
            {
                return RedirectToAction("Login", "Home");
            }

            // Kiểm tra xem có Cookie "Cart" không
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //var cl = db.CLIENTs.FirstOrDefault(c => c.UserName == UserName);
            List<CART> listCart = GetCartAllItem();
            CLIENT client = GetClientFromCookie(); // Hàm lấy thông tin người dùng từ Cookie
            ORDER order = new ORDER();
            order.ClientID = client.ClientID;
            order.OrderDate = DateTime.Now;
            order.DeliveryDate = null;
            order.Payment = "Chưa thanh toán";
            order.Status = "Chờ xác nhận";

            db.ORDERs.Add(order);
            decimal x = 0;

            foreach (var item in listCart)
            {
                ORDER_DETAIL detail = new ORDER_DETAIL();
                BOOK book = db.BOOKs.SingleOrDefault(t => t.BookID == item.iBookID);
                detail.OrderID = order.OrderID;
                detail.BookID = item.iBookID;
                detail.Quantity = item.iQuantity;
                book.Quanlity -= item.iQuantity;
                detail.Price = (decimal)item.dPrice;

                db.ORDER_DETAIL.Add(detail);
                x += (decimal)(item.dPrice * item.iQuantity);
            }
            TempData["TotalCheckout"] = x;

            db.SaveChanges();
            DeleteAllCart();

            SendConfirmationEmail(client.Email, order.OrderID, x);
            // Gửi Cookie "User" cho người dùng
            //Response.Cookies["User"].Value = client.ClientID.ToString();
            //Response.Cookies["User"].Expires = DateTime.Now.AddDays(1);

            return View(order);
           
        }
        private void SendConfirmationEmail(string toEmail, int orderID, decimal totalAmount)
        {
            try
            {
                const string shopEmailAddress = "demobookshopapp@gmail.com"; // Your shop's Gmail address
                const string shopEmailPassword = "demo123456"; // Your shop's Gmail password
                const string shopName = "YourShop"; // Your shop's name

                var fromAddress = new MailAddress(shopEmailAddress, shopName);
                var toAddress = new MailAddress(toEmail, "Customer"); // Thay "Customer Name" bằng tên thực của khách hàng
                const string subject = "Order Confirmation";

                // Tạo nội dung email
                StringBuilder body = new StringBuilder();
                body.AppendLine($"Dear customer, your order with ID {orderID} has been placed successfully.");
                body.AppendLine($"Total amount: {totalAmount:C}");

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 465,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(shopEmailAddress, shopEmailPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body.ToString()
                })
                {
                    // Gửi email
                    smtp.Send(message);
                }
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"SMTP Exception: {ex.Message}");
                // Log the exception for further investigation
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                // Log the exception for further investigation
            }
        }

        private CLIENT GetClientFromCookie()
        {
            var user = SessionConfig.GetUser();
            int? clientId = user.ClientID;
            if (clientId.HasValue)
            {
                CLIENT client = db.CLIENTs.SingleOrDefault(c => c.ClientID == clientId);
                return client;
            }
            return null;
        }
        public ActionResult HistoryOrder()
        {

            CLIENT client = GetClientFromCookie();
            int? clientId = client.ClientID;
            if (clientId.HasValue)
            {
                var order = db.ORDERs.OrderByDescending(a => a.OrderID).Where(d => d.ClientID == clientId.Value).ToList();

                if (order.Any())
                {
                    return View(order);
                }
                else
                {
                    return View("EmptyHistory");
                }
            }

            // Handle the case where the client information is not available (e.g., not logged in)
            return RedirectToAction("Login", "Home");
        }
        public ActionResult EmptyHistory()
        {
            return View();
        }

            [HttpPost]
        public ActionResult UpdateDeliveryStatus(int orderId)
        {
            // Lấy đơn hàng từ database dựa trên orderId
            ORDER order = db.ORDERs.SingleOrDefault(t => t.OrderID == orderId); // Lấy dữ liệu từ database

            // Cập nhật trạng thái giao hàng
            order.Status = "Giao thành công";
            order.Payment = "Đã thanh toán";
            // Lưu thay đổi vào database
            db.SaveChanges();
            // Chuyển hướng về trang lịch sử đơn hàng
            return RedirectToAction("HistoryOrder");
        }
        [HttpPost]
        public ActionResult CancelOrder(int orderId)
        {
            // Lấy đơn hàng từ database dựa trên orderId
            ORDER order = db.ORDERs.SingleOrDefault(t => t.OrderID == orderId); // Lấy dữ liệu từ database

            // Cập nhật trạng thái giao hàng
            order.Status = "Chờ hủy đơn";
            order.Payment = "Chưa thanh toán";
            // Lưu thay đổi vào database
            db.SaveChanges();
            return RedirectToAction("HistoryOrder");
        }
    }
}