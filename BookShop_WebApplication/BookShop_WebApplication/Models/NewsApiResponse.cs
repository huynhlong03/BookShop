using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop_WebApplication.Models
{
    public class NewsApiResponse
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }
        public List<Article> Articles { get; set; }
    }
    
}