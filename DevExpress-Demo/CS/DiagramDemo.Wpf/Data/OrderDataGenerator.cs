using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.DemoData.Models;

namespace DevExpress.Diagram.Demos {
    public static class OrderDataGenerator {
        static List<CustomerData> ExtractCustomers(int count) {
            var customers = NWindContext.Create().Customers.Take(count).ToList();
            var customerData = new List<CustomerData>(count);
            foreach(var row in customers) {
                customerData.Add(new CustomerData(row.ContactName, row.CompanyName, row.Phone, row.City));
            }
            return customerData;
        }
        static List<CategoryData> ExtractCategoryDataList(int count) {
            List<CategoryData> categoryData = new List<CategoryData>(count);
            var categories = NWindContext.Create().Categories.Take(count).ToList();
            foreach(var row in categories) {
                categoryData.Add(new CategoryData(row.CategoryName, row.Description, row.Icon17));
            }
            return categoryData;
        }
        static List<ProductData> ExtractProductDataList(List<CategoryData> categoriesList) {
            List<ProductData> productData = new List<ProductData>();
            var categoryProducts = NWindContext.Create().CategoryProducts.ToList();
            productData.Capacity = categoryProducts.Count;
            Random rand = new Random();
            foreach(var row in categoryProducts) {
                var category = FindCategory(categoriesList, row.CategoryName);
                if(category != null)
                    productData.Add(new ProductData(row.ProductName, category, (decimal)(rand.Next(20) + rand.Next(99)) / 100m));
            }
            return productData;
        }

        static CategoryData FindCategory(List<CategoryData> categoriesList, string name) {
            foreach(CategoryData category in categoriesList) {
                if(category.Name == name) return category;
            }
            return null;
        }

        static List<OrderData> GenerateOrders(int orderCount, int customerCount, int categoryCount) {
            List<OrderData> result = new List<OrderData>(orderCount);
            List<CustomerData> customers = ExtractCustomers(customerCount);
            List<CategoryData> categoriesList = ExtractCategoryDataList(categoryCount);
            List<ProductData> productsList = ExtractProductDataList(categoriesList);

            Random rand = new Random();
            int generateCountPerCent = orderCount / 100;
            for(int i = 0; i < orderCount; i++) {
                ProductData randomProduct = productsList[rand.Next(productsList.Count)];
                OrderData data = new OrderData(
                    orderId: i,
                    orderDate: DateTime.Today.Subtract(TimeSpan.FromDays(rand.Next(180))),
                    customer: customers[rand.Next(customers.Count)],
                    quantity: rand.Next(200) + 1,
                    productCategory: randomProduct.Category,
                    productName: randomProduct.Name,
                    price: randomProduct.Price
                );
                result.Add(data);
            }
            return result;
        }
        public static ProductFlowInfo GenerateProductFlowInfo() {
            var orders = GenerateOrders(50, 4, 5).ToArray();
            var customers = orders.Select(x => x.Customer).Distinct().ToArray();
            var categories = orders.Select(x => x.Category).Distinct().ToArray();
            var groupedOrders = orders.GroupBy(x => new { x.Customer, x.Category }).ToArray();
            var minCount = groupedOrders.Min(x => x.Sum(y => y.Quantity));
            var maxCount = groupedOrders.Max(x => x.Sum(y => y.Quantity));
            const float minWeight = 1, maxWeight = 8;
            var productFlows = orders
                .GroupBy(x => new { x.Customer, x.Category })
                .Select(x => new ProductFlowData(x.Key.Customer, x.Key.Category, minWeight + (maxWeight - minWeight) * (x.Sum(d => d.Quantity) - minCount) / (maxCount - minCount)))
                .ToArray();
            return new ProductFlowInfo(productFlows, orders, customers, categories);
        }
    }
    public class ProductFlowInfo {
        public ProductFlowInfo(ProductFlowData[] productFlows, OrderData[] orders, CustomerData[] customers, CategoryData[] categories) {
            ProductFlows = productFlows;
            Orders = orders;
            Customers = customers;
            Categories = categories;
            Items = Customers.Cast<object>().Concat(Categories).ToArray();
        }
        public object[] Items { get; private set; }
        public ProductFlowData[] ProductFlows { get; private set; }
        public OrderData[] Orders { get; private set; }
        public CustomerData[] Customers { get; private set; }
        public CategoryData[] Categories { get; private set; }
    }
    public class CategoryData {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public byte[] Picture { get; private set; }
        public CategoryData(string name, string description, byte[] picture) {
            this.Name = name;
            this.Description = description;
            this.Picture = picture;
        }
        public override string ToString() {
            return Name;
        }
    }
    public class ProductData {
        public ProductData(string name, CategoryData category, decimal price) {
            Name = name;
            Category = category;
            Price = price;
        }
        public string Name { get; private set; }
        public CategoryData Category { get; private set; }
        public decimal Price { get; private set; }
        public override string ToString() {
            return Name;
        }
    }
    public class CustomerData {
        public string Name { get; private set; }
        public string CompanyName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string City { get; private set; }

        public CustomerData(string name, string companyName, string phoneNumber, string city) {
            this.Name = name;
            this.CompanyName = companyName;
            this.PhoneNumber = phoneNumber;
            this.City = city;
        }
        public override string ToString() {
            return CompanyName + "\r\n\r\n" + City + "\r\n" + PhoneNumber;
        }
    }
    public class OrderData {
        public OrderData(int orderId, CustomerData customer, CategoryData productCategory, string productName, DateTime orderDate, int quantity, decimal price) {
            OrderId = orderId;
            Customer = customer;
            Category = productCategory;
            ProductName = productName;
            OrderDate = orderDate;
            Quantity = quantity;
            Price = price;
        }
        public int OrderId { get; private set; }
        public CustomerData Customer { get; private set; }
        public CategoryData Category { get; private set; }
        public string ProductName { get; private set; }
        public DateTime OrderDate { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; set; }
    }
    public class ProductFlowData {
        public ProductFlowData(CustomerData customer, CategoryData category, float weight) {
            Customer = customer;
            Category = category;
            Weight = weight;
        }
        public CustomerData Customer { get; private set; }
        public CategoryData Category { get; private set; }
        public float Weight { get; private set; }
    }

}
