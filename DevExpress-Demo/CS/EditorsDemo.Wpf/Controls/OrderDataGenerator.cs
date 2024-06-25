using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.DemoBase;
using System.Data;
using DevExpress.Xpf.DemoBase.DataClasses;
using EmployeesWithPhotoData = DevExpress.Xpf.DemoBase.DataClasses.EmployeesWithPhotoData;
using Employee = DevExpress.Xpf.DemoBase.DataClasses.Employee;
using Order = DevExpress.DemoData.Models.Order;

namespace GridDemo {
    public static class OrderDataGenerator {
        static object SyncRoot = new object();
        static List<string> customerNames = new List<string>();
        static List<CategoryData> categoryData = new List<CategoryData>();
        static List<ProductData> productData = new List<ProductData>();

        static List<string> ExtractCustomerNames() {
            if(customerNames.Count == 0) {
                var customers = NWindContext.Create().Customers.ToList();
                customerNames.Capacity = customers.Count;
                foreach(var row in customers) {
                    customerNames.Add(row.ContactName);
                }
            }
            return customerNames;
        }
        public static List<CategoryData> CategoryDataList {
            get {
                if(categoryData.Count == 0) {
                    var categories = NWindContext.Create().Categories.ToList();
                    categoryData.Capacity = categories.Count;
                    foreach(var row in categories) {
                        categoryData.Add(new CategoryData() {
                            Name = row.CategoryName,
                            Picture = row.Icon25
                        });
                    }
                }
                return categoryData;
            }
        }
        public static List<ProductData> ExtractProductDataList(List<CategoryData> categoriesList) {
            if(productData.Count == 0) {
                var categoryProducts = NWindContext.Create().CategoryProducts.ToList();
                productData.Capacity = categoryProducts.Count;
                Random rand = new Random();
                foreach(var row in categoryProducts) {
                    productData.Add(new ProductData() {
                        Category = FindCategory(categoriesList, row.CategoryName),
                        Name = row.ProductName,
                        Price = (decimal)(rand.Next(20) + rand.Next(99)) / 100m
                    });
                }
            }
            return productData;
        }

        public static List<OrderData> GenerateOrders(int generateCount) {
            List<OrderData> result = new List<OrderData>(generateCount);
            List<string> customerNames = ExtractCustomerNames();
            List<CategoryData> categoriesList = CategoryDataList;
            List<ProductData> productsList = ExtractProductDataList(categoriesList);

            Random rand = new Random();
            for(int i = 0; i < generateCount; i++) {
                ProductData randomProduct = productsList[rand.Next(productsList.Count)];
                string randomName = customerNames[rand.Next(customerNames.Count)];
                OrderData data = new OrderData();
                data.OrderId = i;
                data.OrderDate = DateTime.Today.Subtract(TimeSpan.FromDays(rand.Next(180)));
                data.CustomerName = randomName;
                data.Quantity = rand.Next(200) + 1;
                data.CustomerID = i + 1;
                data.ProductCategory = randomProduct.Category;
                data.ProductName = randomProduct.Name;
                data.Price = randomProduct.Price;
                data.IsReady = (rand.Next(2) == 0);
                result.Add(data);
            }
            return result;
        }

        static CategoryData FindCategory(List<CategoryData> categoriesList, string name) {
            foreach(CategoryData category in categoriesList) {
                if(category.Name == name) return category;
            }
            return null;
        }
    }

    public class CategoryData : IComparable, IComparable<CategoryData> {
        public string Name { get; set; }
        public byte[] Picture { get; set; }
        public override string ToString() {
            return Name;
        }

        #region IComparable Members
        public int CompareTo(object obj) {
            if(obj is CategoryData)
                return CompareTo((CategoryData)obj);
            return -1;
        }
        #endregion
        #region IComparable<CategoryData> Members
        public int CompareTo(CategoryData other) {
            return StringComparer.CurrentCulture.Compare(Name, other.Name);
        }
        #endregion
    }
    public class ProductData {
        public string Name { get; set; }
        public CategoryData Category { get; set; }
        public decimal Price { get; set; }
        public override string ToString() {
            return Name;
        }
    }

    public class OrderData {
        public int OrderId { get; set; }
        public bool IsReady { get; set; }
        public string CustomerName { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public CategoryData ProductCategory { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
