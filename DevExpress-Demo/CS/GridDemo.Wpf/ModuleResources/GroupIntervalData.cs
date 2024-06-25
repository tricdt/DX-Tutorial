using DevExpress.DemoData.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GridDemo {
    public static class GroupIntervalData {
        public static Random rnd = new Random();
        public static List<Invoice> Invoices {
            get {
                var invoices = NWindContext.Create().Invoices.ToList();
                foreach(var invoice in invoices) {
                    invoice.OrderDate = GetDate(true);
                }
                return invoices;
            }
        }
        public static List<ProductInfo> Products {
            get {
                const int rowCount = 1000;
                var products = NWindContext.Create().Products.ToList();
                var shuffledProducts = new List<ProductInfo>();
                for(int i = 0; i < rowCount; i++) {
                    Product product = products[rnd.Next(products.Count - 1)];
                    shuffledProducts.Add(new ProductInfo() {
                        ProductName = product.ProductName,
                        UnitPrice = product.UnitPrice,
                        Count = GetCount(),
                        OrderDate = GetDate(false)
                    });
                }
                return shuffledProducts;
            }
        }
        static DateTime GetDate(bool range) {
            DateTime result = DateTime.Now;
            int r = rnd.Next(20);
            if(range) {
                if(r > 1) result = result.AddDays(rnd.Next(80) - 40);
                if(r > 18) result = result.AddMonths(rnd.Next(18));
            } else {
                result = result.AddDays(rnd.Next(r * 30) - r * 15);
            }
            return result;
        }
        static decimal GetCount() {
            return rnd.Next(50) + 10;
        }
    }
    public class ProductInfo {
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal Count { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
