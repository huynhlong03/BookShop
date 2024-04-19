using BookShop_WebApplication.Models;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookShop_WebApplication.Controllers
{
    public class NewsController : Controller
    {
        /* string apiKey = "b43962cddcb94fc09284e58c40fb1c25"; đây là ApiKey trên NewsAPI của bạn, có thể copy ApiKey này để chạy đường dẫn */


        /* Do đây là 1 api tin tức nên đường dẫn apiUrl sẽ tồn tại một khoảng thời gian nhất định, để chắn chắn 
          thì mỗi lần chạy controller api vui lòng copy đường dẫn apiUrl bên dưới chạy trên trang Google, Microsoft Egde,... trước để mà 
         cập nhật lại tin tức có trong đường dẫn này, nếu không thực hiện bước trên thì action này sẽ trả về một view lỗi!  */

        /*private readonly string apiUrl = "https://newsapi.org/v2/everything?q=tesla&from=2023-11-05&sortBy=publishedAt&apiKey=b43962cddcb94fc09284e58c40fb1c25"; */
        private readonly string apiUrl = "https://newsapi.org/v2/everything?q=Apple&from=2023-12-04&sortBy=popularity&apiKey=b43962cddcb94fc09284e58c40fb1c25"; // phần đuôi là ApiKey

        public async Task<ActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetStringAsync(apiUrl);
                    var newsData = JsonConvert.DeserializeObject<NewsApiResponse>(response);
                    return View(newsData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return View("ErrorView");
                }
            }
            
        }
        public ActionResult ErrorView()
        {
            return View();
        }
    }
}