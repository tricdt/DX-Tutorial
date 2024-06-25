using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using DevExpress.DemoData.Helpers;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace PivotGridDemo.Helpers {
    public static class DatabaseHelper {
        #region Fields

        static readonly Random random = new Random();
        static readonly string[] FirstNames = { "Julia", "Stephanie", "Alex", "John", "Curtis", "Keith", "Timothy", "Jack", "Miranda", "Alice" };
        static readonly string[] LastNames = { "Black", "White", "Brown", "Smith", "Cooper", "Parker", "Walker", "Hunter", "Burton", "Douglas", "Fox", "Simpson" };
        static readonly string[] Adjectives = { "Ancient", "Modern", "Mysterious", "Elegant", "Red", "Green", "Blue", "Amazing", "Wonderful", "Astonishing", "Lovely", "Beautiful", "Inexpensive", "Famous", "Magnificent", "Fancy" };
        static readonly string[] ProductNames = { "Ice Cubes", "Bicycle", "Desk", "Hamburger", "Notebook", "Tea", "Cellphone", "Butter", "Frying Pan", "Napkin",
		            "Armchair", "Chocolate", "Yoghurt", "Statuette", "Keychain" };
        static readonly string[] CategoryNames = { "Business", "Presents", "Accessories", "Home", "Hobby" };

        static readonly string[] CustomersNames;
        static readonly string[] SalesPersonsNames;
        static readonly ProductDataRecord[] Products;

        #endregion

        static DatabaseHelper() {
            CustomersNames = GenerateUniqueValues(random.Next(40, 50), GeneratePersonName).ToArray();
            SalesPersonsNames = GenerateUniqueValues(random.Next(40, 50), GeneratePersonName).ToArray();
            Products = GenerateProducts();
        }

        #region Public
        
        public static Dictionary<string, object> CreateValues() {
            var dict = new Dictionary<string, object>();
            var product = DatabaseHelper.GetProduct();
            dict.Add("OrderDate", DatabaseHelper.GetOrderDate());
            dict.Add("Quantity", DatabaseHelper.GetQuantity());
            dict.Add("UnitPrice", DatabaseHelper.GetProductPrice(product));
            dict.Add("CustomerName", DatabaseHelper.GetCustomerName());
            dict.Add("ProductName", product.ProductName);
            dict.Add("CategoryName", product.CategoryName);
            dict.Add("SalesPersonName", DatabaseHelper.GetSalesPersonName());
            return dict;
        }

        public static DateTime GetOrderDate() {
            return new DateTime(random.Next(2007, 2015), random.Next(1, 13), random.Next(1, 28));
        }
        public static int GetQuantity() {
            return random.Next(1, 100);
        }
        public static decimal GetProductPrice(ProductDataRecord product) {
            var price = product.UnitPrice * (decimal)(0.5 + random.NextDouble());
            return Math.Round(price, 2);
        }
        public static ProductDataRecord GetProduct() {
            return Products[random.Next(Products.Length)];
        }
        public static string GetCustomerName() {
            return CustomersNames[random.Next(CustomersNames.Length)];
        }
        public static string GetSalesPersonName() {
            return SalesPersonsNames[random.Next(SalesPersonsNames.Length)];
        }

        #endregion
        #region Private

        static List<T> GenerateUniqueValues<T>(int count, Func<T> generateValue) {
            var values = new HashSet<T>();
            while(values.Count < count) {
                var value = generateValue();
                if(!values.Contains(value))
                    values.Add(value);
            }
            return values.ToList();
        }

        static ProductDataRecord[] GenerateProducts() {
            return GenerateUniqueValues(random.Next(80, 100), GenerateProductName)
                .Select(productName => new ProductDataRecord {
                    ProductName = productName,
                    UnitPrice = random.Next(10, 500),
                    CategoryName = GenerateCategoryName()
                })
                .ToArray();
        }
        static string GenerateCategoryName() {
            return CategoryNames[random.Next(CategoryNames.Length)];
        }
        static string GeneratePersonName() {
            return FirstNames[random.Next(FirstNames.Length)] + " " + LastNames[random.Next(LastNames.Length)];
        }
        static string GenerateProductName() {
            return Adjectives[random.Next(Adjectives.Length)] + " " + ProductNames[random.Next(ProductNames.Length)];
        }
        #endregion
    }
}
