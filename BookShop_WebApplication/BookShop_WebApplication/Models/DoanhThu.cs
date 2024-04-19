using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop_WebApplication.Models
{
    public class DoanhThu
    {
        public int BookID { get; set; }
        public int SoLuongBanChay { get; set; }
        public string TenSanPham { get; set; }
        public string HinhAnh { get; set; }
    }
}