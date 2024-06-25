using DevExpress.DemoData.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GridDemo {
    public static class ProductSaleDataGenerator {
        public static List<ProductSaleData> GenerateSales(int count) {
            var random = new Random();
            var products = NWindContext.Create().Invoices.Select(invoice => invoice.ProductName).Distinct().ToList();
            List<ProductSaleData> sales = new List<ProductSaleData>();
            var countries = NWindContext.Create().CountriesArray;
            return Enumerable.Range(0, count).Select(index => {
                return CreateSale(random, countries[index % countries.Length], products[index % products.Count], DateTime.Today.Year - index % 9);
            }).ToList();
        }
        static ProductSaleData CreateSale(Random random, string country, string product, int year) {
            var sale = new ProductSaleData();
            sale.Country = country;
            sale.ProductName = product;
            sale.Year = year;
            sale.Sales = random.Next(50000000, 500000000);
            sale.SalesVsTarget = (random.NextDouble() - 0.5) / 5;
            sale.Profit = random.Next(-30000000, 50000000);
            sale.CustomersSatisfaction = Math.Round((random.NextDouble() + 1) * 2.5, 1);
            sale.MarketShare = random.NextDouble() / 3 + 0.15;
            return sale;
        }
    }
}
