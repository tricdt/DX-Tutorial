using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using DevExpress.DemoData.Helpers;
using DevExpress.Xpf.DemoBase.Helpers;

namespace DockingDemo.Helpers {
    public static class DataLoader {
        static DataSet LoadData(string fileName) {
            string path = DataFilesHelper.FindFile(string.Format("{0}.xml", fileName), DataFilesHelper.DataPath);
            DataSet ds = new DataSet();
            ds.ReadXml(path, XmlReadMode.ReadSchema);
            return ds;
        }
        public static DataSet LoadSales() {
            return LoadData("DashboardSales");
        }
    }
    public abstract class SalesDataGenerator {
        public class Context {
            readonly string st;
            readonly string prodName;
            readonly string catName;
            readonly decimal lPrice;
            readonly UnitsSoldRandomGenerator uSoldGenerator;

            public string State { get { return st; } }
            public string ProductName { get { return prodName; } }
            public string CategoryName { get { return catName; } }
            public decimal ListPrice { get { return lPrice; } }
            public UnitsSoldRandomGenerator UnitsSoldGenerator { get { return uSoldGenerator; } }

            public Context(string st, string prodName, string catName, decimal lPrice, UnitsSoldRandomGenerator uSoldGenerator) {
                this.st = st;
                this.prodName = prodName;
                this.catName = catName;
                this.lPrice = lPrice;
                this.uSoldGenerator = uSoldGenerator;
            }
        }

        protected static string GetState(DataRow region) {
            return (string)region["Region"];
        }
        protected static string GetProductName(DataRow product) {
            return (string)product["Name"];
        }
        protected static decimal GetListPrice(DataRow product) {
            return (decimal)product["ListPrice"];
        }

        readonly DataSet ds;
        readonly DataTable categoriesTable;
        readonly DataTable productsTable;
        readonly DataTable regionsTable;
        readonly Random rand = new Random(1);
        readonly ProductClasses prodClasses;
        readonly RegionClasses regClasses;

        protected DataRowCollection Regions { get { return regionsTable.Rows; } }
        protected DataRowCollection Products { get { return productsTable.Rows; } }
        protected ProductClasses ProdClasses { get { return prodClasses; } }
        protected RegionClasses RegClasses { get { return regClasses; } }
        protected Random Random { get { return rand; } }

        protected SalesDataGenerator(DataSet ds) {
            this.ds = ds;
            categoriesTable = ds.Tables["Categories"];
            productsTable = ds.Tables["Products"];
            regionsTable = ds.Tables["Regions"];
            prodClasses = new ProductClasses(productsTable.Rows);
            regClasses = new RegionClasses(regionsTable.Rows);
        }
        protected double GetRegionWeigtht(DataRow region) {
            return regClasses[(int)region["RegionID"]];
        }
        protected ProductClass GetProductClass(DataRow product) {
            return prodClasses[(int)product["ProductID"]];
        }
        protected string GetCategoryName(DataRow product) {
            return (string)categoriesTable.Select(string.Format("CategoryID = {0}", product["CategoryID"]))[0]["CategoryName"];
        }
        protected UnitsSoldRandomGenerator CreateUnitsSoldGenerator(double regionWeight, ProductClass productClass) {
            return new UnitsSoldRandomGenerator(rand, (int)Math.Ceiling(productClass.SaleProbability * regionWeight));
        }
        protected abstract void Generate(Context context);
        protected virtual void EndGenerate() {
        }
        public void Generate() {
            foreach(DataRow region in Regions) {
                string state = GetState(region);
                double regionWeight = GetRegionWeigtht(region);
                foreach(DataRow product in Products) {
                    UnitsSoldRandomGenerator unitsSoldgenerator = CreateUnitsSoldGenerator(regionWeight, GetProductClass(product));
                    Generate(new Context(state, GetProductName(product), GetCategoryName(product), GetListPrice(product), unitsSoldgenerator));
                }
            }
            EndGenerate();
        }
    }
    public class SalesPerformanceDataGenerator : SalesDataGenerator {
        readonly List<MonthlySalesItem> monthlySales = new List<MonthlySalesItem>();
        readonly List<TotalSalesItem> totalSales = new List<TotalSalesItem>();
        readonly KeyMetricsItem item = new KeyMetricsItem();

        public IEnumerable<MonthlySalesItem> MonthlySales { get { return monthlySales; } }
        public IEnumerable<TotalSalesItem> TotalSales { get { return totalSales; } }
        public IEnumerable<KeyMetricsItem> KeyMetrics { get { return new KeyMetricsItem[] { item }; } }

        public SalesPerformanceDataGenerator(DataSet dataSet)
            : base(dataSet) {
        }
        protected override void Generate(Context context) {
            TotalSalesItem tsItem = new TotalSalesItem { State = context.State, Category = context.CategoryName, Product = context.ProductName };
            int y = DateTime.Today.Year - 1;
            for(int currentMonth = 1; currentMonth <= 12; currentMonth++) {
                DateTime dt = new DateTime(y, currentMonth, 1);
                context.UnitsSoldGenerator.Next();
                int uSold = context.UnitsSoldGenerator.UnitsSold;
                int uSoldTarget = context.UnitsSoldGenerator.UnitsSoldTarget;
                decimal rev = uSold * context.ListPrice;
                decimal revTarget = uSoldTarget * context.ListPrice;
                monthlySales.Add(new MonthlySalesItem {
                    State = context.State,
                    Product = context.ProductName,
                    Category = context.CategoryName,
                    CurrentDate = dt,
                    UnitsSold = uSold,
                    UnitsSoldTarget = uSoldTarget,
                    Revenue = rev,
                    RevenueTarget = revTarget
                });
                tsItem.RevenueYTD += rev;
                tsItem.RevenueYTDTarget += revTarget;
                tsItem.UnitsSoldYTD += uSold;
                tsItem.UnitsSoldYTDTarget += uSoldTarget;
                if(currentMonth >= 10 && currentMonth <= 12) {
                    tsItem.RevenueQTD += rev;
                    tsItem.RevenueQTDTarget += revTarget;
                }
                item.RevenueYTD += rev;
                item.RevenueYTDTarget += revTarget;
            }
            totalSales.Add(tsItem);
        }
        protected override void EndGenerate() {
            base.EndGenerate();
            item.ExpencesYTD = item.RevenueYTDTarget * 0.2m;
            item.ExpencesYTDTarget = item.RevenueYTDTarget * 0.1999m;
            item.ProfitYTD = item.RevenueYTD - item.ExpencesYTD;
            item.ProfitYTDTarget = item.RevenueYTDTarget - item.ExpencesYTDTarget;
            item.AvgOrderSizeYTD = item.RevenueYTD * 0.006m;
            item.AvgOrderSizeYTDTarget = item.RevenueYTDTarget * 0.0055m;
            item.NewCustomersYTD = (int)Math.Round(item.RevenueYTD * 0.0013m);
            item.NewCustomersYTDTarget = (int)Math.Round(item.RevenueYTDTarget * 0.00125m);
            item.MarketShare = 0.23f;
        }
    }
    public class UnitsSoldRandomGenerator {
        const int MinUnitsSold = 5;

        readonly Random rand;
        readonly int startUnitsSold;
        int? prevUnitsSold;
        int? prevPrevUnitsSold;
        int unitsSold;
        int unitsSoldTarget;
        bool isFirst = true;

        public int UnitsSold { get { return unitsSold; } }
        public int UnitsSoldTarget { get { return unitsSoldTarget; } }

        public UnitsSoldRandomGenerator(Random rand, int startUnitsSold) {
            this.rand = rand;
            this.startUnitsSold = Math.Max(startUnitsSold, MinUnitsSold);
        }
        public void Next() {
            if(isFirst) {
                unitsSold = startUnitsSold;
                isFirst = false;
            } else {
                unitsSold = unitsSold + (int)Math.Round(DataHelper.Random(rand, unitsSold * 0.5));
                unitsSold = Math.Max(unitsSold, MinUnitsSold);
            }
            int unitsSoldSum = unitsSold;
            int count = 1;
            if(prevUnitsSold.HasValue) {
                unitsSoldSum += prevUnitsSold.Value;
                count++;
            }
            if(prevPrevUnitsSold.HasValue) {
                unitsSoldSum += prevPrevUnitsSold.Value;
                count++;
            }
            unitsSoldTarget = (int)Math.Round((double)unitsSoldSum / count);
            unitsSoldTarget = unitsSoldTarget + (int)Math.Round(DataHelper.Random(rand, unitsSoldTarget));
            prevPrevUnitsSold = prevUnitsSold;
            prevUnitsSold = unitsSold;
        }
    }
    public class ProductClasses : List<ProductClass> {
        public new ProductClass this[int productID] {
            get {
                foreach(ProductClass productClass in this)
                    if(productClass.ContainsProduct(productID))
                        return productClass;
                throw new ArgumentException("procutID");
            }
        }
        public ProductClasses(ICollection products) {
            Add(new ProductClass(null, 100m, 0.5));
            Add(new ProductClass(100m, 500m, 0.4));
            Add(new ProductClass(500m, 1500m, 0.3));
            Add(new ProductClass(1500m, null, 0.2));
            foreach(DataRow product in products) {
                int productID = (int)product["ProductID"];
                decimal listPrice = (decimal)product["ListPrice"];
                foreach(ProductClass productClass in this)
                    if(productClass.AddProduct(productID, listPrice))
                        break;
            }
        }
    }
    public class RegionClasses : Dictionary<int, double> {
        public RegionClasses(ICollection regions) {
            int? numberEmployeesMin = null;
            foreach(DataRow region in regions) {
                short numberEmployees = (short)region["NumberEmployees"];
                numberEmployeesMin = numberEmployeesMin.HasValue ? Math.Min(numberEmployeesMin.Value, numberEmployees) : numberEmployees;
            }
            foreach(DataRow region in regions)
                Add((int)region["RegionID"], (short)region["NumberEmployees"] / (double)numberEmployeesMin.Value);
        }
    }
    public class ProductClass {
        readonly List<int> productIDs = new List<int>();
        readonly decimal? minPrice;
        readonly decimal? maxPrice;
        readonly double saleProbability;

        public double SaleProbability { get { return saleProbability; } }

        public ProductClass(decimal? minPrice, decimal? maxPrice, double saleProbability) {
            this.minPrice = minPrice;
            this.maxPrice = maxPrice;
            this.saleProbability = saleProbability;
        }
        public bool AddProduct(int productID, decimal price) {
            bool satisfyMinPrice = !minPrice.HasValue || price >= minPrice.Value;
            bool satisfyMaxPrice = !maxPrice.HasValue || price < maxPrice.Value;
            if(satisfyMinPrice && satisfyMaxPrice) {
                productIDs.Add(productID);
                return true;
            }
            return false;
        }
        public bool ContainsProduct(int productID) {
            return productIDs.Contains(productID);
        }
    }
    public static class DataHelper {
        public static double Random(Random random, double deviation, bool positive) {
            int rand = random.Next(positive ? 0 : -1000000, 1000000);
            return (double)rand / 1000000 * deviation;
        }
        public static double Random(Random random, double deviation) {
            return Random(random, deviation, false);
        }
    }
    public class TotalSalesItem {
        public string State { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public decimal RevenueYTD { get; set; }
        public decimal RevenueYTDTarget { get; set; }
        public decimal RevenueQTD { get; set; }
        public decimal RevenueQTDTarget { get; set; }
        public int UnitsSoldYTD { get; set; }
        public int UnitsSoldYTDTarget { get; set; }
    }
    public class MonthlySalesItem {
        public string State { get; set; }
        public string Product { get; set; }
        public string Category { get; set; }
        public DateTime CurrentDate { get; set; }
        public decimal Revenue { get; set; }
        public decimal RevenueTarget { get; set; }
        public int UnitsSold { get; set; }
        public int UnitsSoldTarget { get; set; }
    }
    public class KeyMetricsItem {
        public decimal RevenueYTD { get; set; }
        public decimal RevenueYTDTarget { get; set; }
        public decimal ExpencesYTD { get; set; }
        public decimal ExpencesYTDTarget { get; set; }
        public decimal ProfitYTD { get; set; }
        public decimal ProfitYTDTarget { get; set; }
        public decimal AvgOrderSizeYTD { get; set; }
        public decimal AvgOrderSizeYTDTarget { get; set; }
        public int NewCustomersYTD { get; set; }
        public int NewCustomersYTDTarget { get; set; }
        public float MarketShare { get; set; }
    }
}
