using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.IO;
using DbModelBuilder = Microsoft.EntityFrameworkCore.ModelBuilder;

namespace PivotGridDemo {
    public class OrderDataContext : DbContext {
        public static readonly string FileName = Path.GetFullPath("OrderData.db");
        public DbSet<OrderDataRecord> OrderDataRecords { get; set; }

        public OrderDataContext() : base() {
            SetFilePath();
            connectionString = string.Format("Data Source={0}", filePath);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseLazyLoadingProxies().UseSqlite(connectionString);
        }
        string connectionString;

        static OrderDataContext defaultContext;
        public static OrderDataContext Default {
            get { return defaultContext ?? (defaultContext = new OrderDataContext()); }
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new OrderDataRecordMap());
        }

        static string filePath;
        static void SetFilePath() {
            if(filePath == null)
                filePath = FileName;
            try {
                var attributes = File.GetAttributes(filePath);
                if(attributes.HasFlag(FileAttributes.ReadOnly)) {
                    File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                }
            } catch { }
        }
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

    public class OrderDataRecordMap : IEntityTypeConfiguration<OrderDataRecord> {
        public void Configure(EntityTypeBuilder<OrderDataRecord> builder) {
            
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Quantity)
                .HasColumnType("int");
            builder.Property(t => t.UnitPrice)
                .HasConversion<float>();
            builder.Property(t => t.Id)
                .ValueGeneratedNever();
            builder.Property(t => t.OrderDate)
                .ValueGeneratedNever();
            builder.Property(t => t.Quantity)
                .ValueGeneratedNever();
            builder.Property(t => t.UnitPrice)
                .ValueGeneratedNever();
            builder.Property(t => t.CustomerName)
                .HasMaxLength(100);
            builder.Property(t => t.ProductName)
                .HasMaxLength(100);
            builder.Property(t => t.CategoryName)
                .HasMaxLength(100);
            builder.Property(t => t.SalesPersonName)
                .HasMaxLength(100);
            builder.Property(e => e.UnitPrice)
                .HasConversion<double>();
            
            builder.ToTable("OrderDataRecords");
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.OrderDate).HasColumnName("OrderDate");
            builder.Property(t => t.Quantity).HasColumnName("Quantity");
            builder.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            builder.Property(t => t.CustomerName).HasColumnName("CustomerName");
            builder.Property(t => t.ProductName).HasColumnName("ProductName");
            builder.Property(t => t.CategoryName).HasColumnName("CategoryName");
            builder.Property(t => t.SalesPersonName).HasColumnName("SalesPersonName");
        }
    }
}
