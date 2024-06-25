using DevExpress.Xpf.DemoBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;

namespace PivotGridDemo {
    public class OrderDataContext : DbContext {
        public static readonly string FileName = Path.GetFullPath("OrderData.db");
        public OrderDataContext()
            : base(SQLiteDataBaseGenerator.CreateConnection(ServerModeViewModel.DatabaseInfo), true) {
        }
        public DbSet<OrderDataRecord> OrderDataRecords { get; set; }
    }

    public class OrderDataRecord {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string SalesPersonName { get; set; }
    }
    public class ProductDataRecord {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
