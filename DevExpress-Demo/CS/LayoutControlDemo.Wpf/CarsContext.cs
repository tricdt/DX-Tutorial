using System;
using System.Windows.Media;
using System.Text;
using System.Linq;
using System.IO;
#if DXCORE3
using Microsoft.EntityFrameworkCore;
using DbModelBuilder = Microsoft.EntityFrameworkCore.ModelBuilder;
#else
using System.Data.SQLite;
using System.Data.Entity;
#endif
using System.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using DevExpress.Internal;
using DevExpress.DemoData.Helpers;



namespace DevExpress.DemoData.Models {
    public partial class CarsContext : DbContext {
#if DXCORE3
        public CarsContext() : base() {
            SetFilePath();
            connectionString = string.Format("Data Source={0}", filePath);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseLazyLoadingProxies().UseSqlite(connectionString);
        }
        string connectionString;
#else
        public CarsContext() : base(CreateConnection(), true) { }
        public CarsContext(string connectionString) : base(connectionString) { }
        public CarsContext(DbConnection connection) : base(connection, true) { }
        
        static CarsContext() {
            Database.SetInitializer<CarsContext>(null);
        }
        static DbConnection CreateConnection() {
            SetFilePath();
            var connection = DbProviderFactories.GetFactory("System.Data.SQLite.EF6").CreateConnection();
            connection.ConnectionString = new SQLiteConnectionStringBuilder { DataSource = filePath }.ConnectionString;
            return connection;
        }
#endif
        static string filePath;
        static void SetFilePath() {
            if(filePath == null)
                filePath = DataDirectoryHelper.GetFile("cars.db", DataDirectoryHelper.DataFolderName);
            try {
                var attributes = File.GetAttributes(filePath);
                if(attributes.HasFlag(FileAttributes.ReadOnly)) {
                    File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                }
            } catch { }
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarScheduling> CarSchedule { get; set; }
        public DbSet<UsageType> UsageTypes { get; set; }
    }
}



namespace DevExpress.DemoData.Models {
    public class Car {
        ImageSource imageSource;
        public long Id { get; set; }
        public string Trademark { get; set; }
        public string Model { get; set; }
        public int HP { get; set; }
        public double Liter { get; set; }
        public int Cyl { get; set; }
        public int TransmissSpeedCount { get; set; }
        public string TransmissAutomatic { get; set; }
        public int MPGCity { get; set; }
        public int MPGHighway { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Hyperlink { get; set; }
        public byte[] Picture { get; set; }
        public byte[] Icon { get; set; }
        public ImageSource ImageSource {
            get {
                if(imageSource == null)
                    imageSource = ImageSourceHelper.GetImageSource(new MemoryStream(Picture));
                return imageSource;
            }
        }
        public decimal Price { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool IsInStock { get; set; }
    }

    public class CarScheduling {
        public long Id { get; set; }
        public virtual Car Car { get; set; }
        public int Status { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int Label { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public bool AllDay { get; set; }
        public int EventType { get; set; }
        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }
        public decimal Price { get; set; }
        public string ContactInfo { get; set; }
    }

    public class UsageType {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Color { get; set; }
    }
}



namespace DevExpress.DemoData.Models {
    public class CarsData {
        CarsContext context = new CarsContext();
        public IEnumerable<Car> Cars {
            get { return context.Cars.ToList(); }
        }
        public IEnumerable<CarScheduling> CarsSchedule {
            get { return context.CarSchedule.ToList(); }
        }
    }
}
