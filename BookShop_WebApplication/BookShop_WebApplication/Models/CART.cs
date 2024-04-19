using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop_WebApplication.Models
{
    public class CART
    {
        QL_SACHEntities db = new QL_SACHEntities();
        public int iBookID { get; set; }
        public string sBookName { get; set; }
        public string sImage { get; set; }
        public double dPrice { get; set; }
        public int iQuantity { get; set; }
        public double dTotal
        {
            get { return iQuantity * dPrice; }
        }
        public CART(int BookID)
        {
            iBookID = BookID;
            BOOK book = db.BOOKs.Single(s => s.BookID == iBookID);
            sBookName = book.BookName;
            sImage = book.Image;
            dPrice = double.Parse(book.Price.ToString());
            iQuantity = 1;
        }
        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity > 0)
            {
                iQuantity = newQuantity;
            }
        }
    }
}