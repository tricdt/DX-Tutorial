using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Windows;
using DevExpress.Utils;

namespace CommonChartsDemo {

    public class BikeReportItem : SalesProductData.SaleItemBase {
    }
    public class BikeReportRangeItem {
        public decimal Revenue { get; set; }
        public DateTime ReportDate { get; set; }
    }

    public class BikeReport {
        public List<BikeReportItem> BicyclesData { get; set; }
        public List<BikeReportRangeItem> BicycleRangesData { get; set;}
    }

    public class SalesProductData {

        class ProductItemBase {
            public string Product { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
        }

        public class SaleItemBase {
            public string Category { get; set; }
            public int UnitsSold { get; set; }
            public decimal Revenue { get; set; }
            public int UnitsSoldTarget { get; set; }
            [DataType(DataType.Currency)]
            public decimal RevenueTarget { get; set; }
            [DisplayFormat(DataFormatString = "p")]
            public float SalesDynamic { get { return (float)((Revenue - RevenueTarget) / Revenue); } }
            public DateTime ReportDate { get; set; }
        }

        public class SaleItem : SaleItemBase {
            public string State { get; set; }
            public string Product { get; set; }
            [DataType(DataType.Currency)]
            public decimal Price { get; set; }
        }

        
       
        static Random rnd = new Random();
        static List<SaleItem> data;
        static BikeReport bicyclesReport;

        public static List<SaleItem> Data { get { return data ?? (data = GetSalesData()); } }
        public static BikeReport BicyclesReport { get { return bicyclesReport ?? (bicyclesReport = GenerateBicyclesReport()); } }

        public static List<string> BikeCategories = new List<string>() { "Mountain", "Hybrid/Cross", "Road", "Comfort", "Youth", "Cruiser", "Electric" };

        static DataSet LoadData() {
            DataSet ds = new DataSet();
            Uri uri = new Uri(string.Format("/{0};component/Data/DashboardSales.xml", AssemblyHelper.GetPartialName(typeof(SalesProductData).Assembly)), UriKind.RelativeOrAbsolute);
            ds.ReadXml(Application.GetResourceStream(uri).Stream);
            return ds;
        }
        static object CreateProductBase(DataRow dataRow, string categoryName) {
            return new ProductItemBase() {
                Price = dataRow.Field<decimal>("ListPrice"),
                Product = dataRow.Field<string>("Name"),
                Category = categoryName,

            };
        }
        static List<SaleItem> GenerateData(DataRowCollection regions, IEnumerable<ProductItemBase> products) {
            List<SaleItem> totalSales = new List<SaleItem>();
            foreach(DataRow region in regions) {
                string state = (string)region["Region"];
                int year = DateTime.Today.Year - 1;
                for(int month = 1; month <= 12; month++) {
                    foreach(ProductItemBase product in products) {
                        SaleItem tsItem = new SaleItem { State = state, Category = product.Category, Product = product.Product, Price = product.Price };
                        DateTime dt = new DateTime(year, month, 1);
                        int uSold = GetUnitsSold(product.Category);
                        int uSoldTarget = uSold + rnd.Next(-(int)(uSold * 0.2), (int)(uSold * 0.2));
                        decimal rev = uSold * product.Price;
                        decimal revTarget = uSoldTarget * product.Price;

                        tsItem.Revenue = rev;
                        tsItem.RevenueTarget = revTarget;
                        tsItem.UnitsSold = uSold;
                        tsItem.UnitsSoldTarget = uSoldTarget;
                        tsItem.ReportDate = dt;

                        totalSales.Add(tsItem);
                    }
                }
            }
            return totalSales;
        }
        static int GetUnitsSold(string category) {
            int max = category.Equals("Bikes") ? 50 : 250;
            return rnd.Next(1, max);
        }
        static string GetState(DataRow region) {
            return (string)region["Region"];
        }
        static List<SaleItem> GetSalesData() {
            DataSet dataSet = LoadData();
            DataTable products = dataSet.Tables["Products"];
            DataTable categories = dataSet.Tables["Categories"];
            DataTable regions = dataSet.Tables["Regions"];

            var items = from t1 in products.AsEnumerable()
                        join t2 in categories.AsEnumerable()
                        on t1["CategoryID"] equals t2["CategoryID"]
                        select CreateProductBase(t1, (string)t2["CategoryName"]);

            return GenerateData(regions.Rows, items.Cast<ProductItemBase>());
        }

        static BikeReport GenerateBicyclesReport() {
            List<BikeReportItem> result = new List<BikeReportItem>();
            List<BikeReportRangeItem> rangeData = new List<BikeReportRangeItem>();
            int year = DateTime.Today.Year - 1;
            DateTime startDate = new DateTime(year, 1, 1);
            int averageMonthSold = 1700;
            decimal averagePrice = 900; 
            DateTime date = startDate;
            for(int day = 1; day <= 365; day += 7) {
                decimal revenue = 0;
                int minDay = rnd.Next(100, 200);
                int maxDay = rnd.Next(250, 300);
                date = startDate.AddDays(day);
                for(int i = 0; i < BikeCategories.Count; i++) {
                    string category = BikeCategories[i];

                    double deltaCorrection = 2 * rnd.NextDouble() + 0.2;

                    BikeReportItem tsItem = new BikeReportItem { Category = category };
                    double correction = 22 - i * 3 - rnd.NextDouble();
                    if(day > minDay && day < maxDay)
                        correction += deltaCorrection;
                    if(day > maxDay)
                        correction -= deltaCorrection;

                    int uSold = (int)(averageMonthSold * correction / 100.0);
                    int uSoldTarget = uSold + rnd.Next(-(int)(uSold * 0.2), (int)(uSold * 0.2));
                    decimal rev = uSold * averagePrice;
                    decimal revTarget = uSoldTarget * averagePrice;

                    tsItem.Revenue = rev;
                    tsItem.RevenueTarget = revTarget;
                    tsItem.UnitsSold = uSold;
                    tsItem.UnitsSoldTarget = uSoldTarget;
                    tsItem.ReportDate = date;
                    revenue += rev;
                    result.Add(tsItem);
                }

                rangeData.Add(new BikeReportRangeItem() { ReportDate = date, Revenue = revenue });
            }
            return new BikeReport() { BicyclesData = result, BicycleRangesData = rangeData };
        }
    }
}
