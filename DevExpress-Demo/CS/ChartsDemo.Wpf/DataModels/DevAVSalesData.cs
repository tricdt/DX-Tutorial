using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace ChartsDemo {

    public class DevAVSaleItem {
        readonly List<DevAVSaleItem> saleItems = new List<DevAVSaleItem>();

        public DateTime OrderDate { get; set; }
        public string Product { get; set; }
        public string Company { get; set; }
        public string Month { get; set; }
        public double Income { get; set; }
        public double Revenue { get; set; }
        public string Category { get; set; }
        public List<DevAVSaleItem> SaleItems { get { return this.saleItems; } }
        public double TotalIncome {
            get {
                double total = 0;
                foreach (DevAVSaleItem saleItem in SaleItems)
                    total += saleItem.Income;
                return total;
            }
        }

    }


    public class DevAVSales {
        readonly string[] companies = new string[] {
            "DevAV North",
            "DevAV South",
            "DevAV West",
            "DevAV East",
            "DevAV Central"
        };
        Dictionary<string, List<string>> categorizedProducts;
        readonly Random rnd = new Random(2019);
        DateTime endDate;

        Dictionary<string, List<string>> CategorizedProducts {
            get {
                if (this.categorizedProducts == null) {
                    this.categorizedProducts = new Dictionary<string, List<string>>();
                    this.categorizedProducts["Cell Phones"] = new List<string>() { "Smartphones", "Mobile Phones", "Smart Watches", "Sim Cards" };
                    this.categorizedProducts["Computers"] = new List<string>() { "PCs", "Laptops", "Tablets", "Printers" };
                    this.categorizedProducts["TV, Audio"] = new List<string>() { "TVs", "Home Audio", "Headphones", "DVD Players" };
                    this.categorizedProducts["Car Electronics"] = new List<string>() { "GPS Units", "Radars", "Car Alarms", "Car Accessories" };
                    this.categorizedProducts["Power Devices"] = new List<string>() { "Batteries", "Chargers", "Converters", "Testers", "AC/DC Adapters" };
                    this.categorizedProducts["Photo"] = new List<string>() { "Cameras", "Camcorders", "Binoculars", "Flashes", "Tripodes" };
                }
                return this.categorizedProducts;
            }
        }

        public DevAVSales() {
            DateTime now = DateTime.Now;
            this.endDate = new DateTime(now.Year, now.Month, 1);
        }

        public List<DevAVSaleItem> GetProductsByMonths() {
            Random rnd = new Random(1);
            List<DevAVSaleItem> items = new List<DevAVSaleItem>();
            foreach (string company in this.companies)
                foreach (string product in CategorizedProducts["Photo"]) {
                    DateTime dateTime = new DateTime(DateTime.Now.Year, 12, 01);
                    for (int i = 0; i < 12; i++) {
                        int income = rnd.Next(20, 100);
                        int revenue = income + rnd.Next(20, 50);
                        items.Add(new DevAVSaleItem() {
                            Company = company,
                            Product = product,
                            Month = dateTime.AddMonths(1).ToString("MMMM", CultureInfo.InvariantCulture),
                            Income = income,
                            Revenue = revenue
                        });
                        dateTime = dateTime.AddMonths(1);
                    }
                }
            return items;
        }
        public List<DevAVSaleItem> GetProductsByCompany(int companyIndex) {
            Random rnd = new Random(companyIndex);
            List<DevAVSaleItem> items = new List<DevAVSaleItem>();
            foreach (string category in CategorizedProducts.Keys) {
                foreach (string product in CategorizedProducts[category]) {
                    int income = rnd.Next(20, 100);
                    int revenue = income + rnd.Next(20, 50);
                    items.Add(new DevAVSaleItem() {
                        Company = this.companies[companyIndex],
                        Product = product,
                        Income = income,
                        Revenue = revenue,
                        Category = category
                    });
                }
            }
            return items;
        }
        public DataTable GetSalesMixByRegion() {
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[] { new DataColumn("ProductCategory", typeof(string)), new DataColumn("Region", typeof(string)), new DataColumn("Sales", typeof(decimal)) });

            table.Rows.Add("Video players", "Asia", 853D);
            table.Rows.Add("Video players", "Australia", 321D);
            table.Rows.Add("Video players", "Europe", 655D);
            table.Rows.Add("Video players", "North America", 1325D);
            table.Rows.Add("Video players", "South America", 653D);

            table.Rows.Add("Automation", "Asia", 172D);
            table.Rows.Add("Automation", "Australia", 255D);
            table.Rows.Add("Automation", "Europe", 981D);
            table.Rows.Add("Automation", "North America", 963D);
            table.Rows.Add("Automation", "South America", 123D);

            table.Rows.Add("Monitors", "Asia", 1011D);
            table.Rows.Add("Monitors", "Australia", 359D);
            table.Rows.Add("Monitors", "Europe", 721D);
            table.Rows.Add("Monitors", "North America", 565D);
            table.Rows.Add("Monitors", "South America", 532D);

            table.Rows.Add("Projectors", "Asia", 998D);
            table.Rows.Add("Projectors", "Australia", 222D);
            table.Rows.Add("Projectors", "Europe", 865D);
            table.Rows.Add("Projectors", "North America", 787D);
            table.Rows.Add("Projectors", "South America", 332D);

            table.Rows.Add("Televisions", "Asia", 1356D);
            table.Rows.Add("Televisions", "Australia", 232D);
            table.Rows.Add("Televisions", "Europe", 1323D);
            table.Rows.Add("Televisions", "North America", 1125D);
            table.Rows.Add("Televisions", "South America", 865D);

            return table;
        }
        public List<DevAVSaleItem> GetProductsIncome() {
            Random rnd = new Random(1);
            List<DevAVSaleItem> items = new List<DevAVSaleItem>();
            for (int i = 0; i < 50; i++)
                foreach (string product in CategorizedProducts["Photo"]) {
                    items.Add(new DevAVSaleItem() {
                        Product = product,
                        Income = rnd.Next(20, 100)
                    });
                }
            return items;
        }
        public List<DevAVSaleItem> GetCategoriesIncome() {
            Random rnd = new Random(4);
            List<DevAVSaleItem> categoryItems = new List<DevAVSaleItem>();
            foreach (string category in CategorizedProducts.Keys) {
                DevAVSaleItem categoryItem = new DevAVSaleItem() { Category = category };
                foreach (string product in CategorizedProducts[category]) {
                    int year = DateTime.Now.Year - 1;
                    DateTime dateTime = new DateTime(year, 1, 1);
                    for (int i = 0; i < 12; i++) {
                        categoryItem.SaleItems.Add(new DevAVSaleItem() {
                            Category = category,
                            Product = product,
                            OrderDate = dateTime.AddMonths(i),
                            Income = Math.Round(rnd.NextDouble() * 50 + 20, 2)
                        });
                    }
                }
                categoryItems.Add(categoryItem);
            }
            return categoryItems;
        }
        List<DevAVMonthlyIncome> GenerateSalesForProduct(double companyFactor, double categoryFactor) {
            List<DevAVMonthlyIncome> data = new List<DevAVMonthlyIncome>();
            int year = DateTime.Now.Year - 1;
            DateTime baseDate = new DateTime(year, 1, 1);
            int maxIncome = rnd.Next(60, 140);
            for (int i = 0; i < 1000; i++) {
                if (i % 100 == 0)
                    maxIncome = Math.Max(40, rnd.Next(maxIncome - 20, maxIncome + 20));
                DateTime month = endDate.AddDays(-i - 1);
                double income = rnd.Next(20, maxIncome) * companyFactor * categoryFactor;
                DevAVMonthlyIncome monthlyIncome = new DevAVMonthlyIncome(month, income);
                data.Add(monthlyIncome);
            }
            return data;
        }
        List<DevAVProduct> GenerateCategoryProducts(KeyValuePair<string, List<string>> categoryProductsPair, double companyFactor, double categoryFactor) {
            List<DevAVProduct> products = new List<DevAVProduct>();
            foreach (string productName in categoryProductsPair.Value) {
                List<DevAVMonthlyIncome> sales = GenerateSalesForProduct(companyFactor, categoryFactor);
                DevAVProduct product = new DevAVProduct(productName, sales);
                products.Add(product);
            }
            return products;
        }
        List<DevAVProductCategory> GenerateBranchSales(double companyFactor) {
            List<DevAVProductCategory> categories = new List<DevAVProductCategory>();
            foreach (var categoryProductsPair in CategorizedProducts) {
                double categoryFactor = rnd.NextDouble() * 0.6 + 1;
                List<DevAVProduct> products = GenerateCategoryProducts(categoryProductsPair, companyFactor, categoryFactor);
                DevAVProductCategory category = new DevAVProductCategory(categoryProductsPair.Key, products);
                categories.Add(category);
            }
            return categories;
        }
        internal List<DevAVBranch> GetHierarchicalSalesData() {
            List<DevAVBranch> data = new List<DevAVBranch>();
            foreach (string branchName in this.companies) {
                double companyFactor = rnd.NextDouble() * 0.6 + 1;
                List<DevAVProductCategory> categories = GenerateBranchSales(companyFactor);
                DevAVBranch branch = new DevAVBranch(branchName, categories);
                data.Add(branch);
            }
            return data;
        }
    }
}
