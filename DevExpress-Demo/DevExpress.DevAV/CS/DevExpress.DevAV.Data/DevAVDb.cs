using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using DevExpress.DemoData;

namespace DevExpress.DevAV {
    [DbConfigurationType(typeof(DemoDbContextConfiguration))]
    public class DevAVDb : DevAVDbBase {
        public DevAVDb() : base() { }
        public DevAVDb(string connectionStringOrName) : base(connectionStringOrName) { }
        static DevAVDb() {
            Database.SetInitializer<DevAVDb>(null);
        }
    }
    public class DevAVDbBase : DemoDbContext {
        public DevAVDbBase() : base("devav.sqlite3") { }
        public DevAVDbBase(string connectionStringOrName) : base(connectionStringOrName, true) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<EmployeeTask> Tasks { get; set; }
        public DbSet<Crest> Crests { get; set; }
        public DbSet<CustomerCommunication> Communications { get; set; }
        public DbSet<CustomerStore> CustomerStores { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Probation> Probations { get; set; }
        public DbSet<ProductCatalog> ProductCatalogs { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuoteItem> QuoteItems { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<CustomerEmployee> CustomerEmployees { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<TaskAttachedFile> AttachedFiles { get; set; }
        public DbSet<DatabaseVersion> Version { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TaskAttachedFile>()
                .HasOptional(t => t.EmployeeTask)
                .WithMany(p => p.AttachedFiles)
                .HasForeignKey(t => t.EmployeeTaskId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CustomerEmployee>()
                .HasMany(x => x.EmployeeTasks)
                .WithOptional(x => x.CustomerEmployee);

            modelBuilder.Entity<Employee>()
                .HasOptional(x => x.Picture)
                .WithMany(x => x.Employees);

            modelBuilder.Entity<Employee>()
                .HasOptional(x => x.ProbationReason)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.ProbationReason_Id);

            modelBuilder.Entity<Evaluation>()
                .HasOptional(x => x.CreatedBy)
                .WithMany(x => x.EvaluationsCreatedBy);

            modelBuilder.Entity<CustomerEmployee>()
                .HasOptional(x => x.CustomerStore)
                .WithMany(x => x.CustomerEmployees);

            modelBuilder.Entity<CustomerEmployee>()
                .HasOptional(x => x.Picture)
                .WithMany(x => x.CustomerEmployees);

            modelBuilder.Entity<CustomerStore>()
                .HasOptional(x => x.Crest)
                .WithMany(x => x.CustomerStores);

            modelBuilder.Entity<Order>()
                .HasOptional(x => x.Employee)
                .WithMany(x => x.Orders);

            modelBuilder.Entity<Order>()
                .HasOptional(x => x.Store)
                .WithMany(x => x.Orders);

            modelBuilder.Entity<Product>()
                .HasOptional(x => x.Engineer)
                .WithMany(x => x.Products);

            modelBuilder.Entity<Product>()
                .HasOptional(x => x.PrimaryImage)
                .WithMany(x => x.Products);

            modelBuilder.Entity<Product>()
                .HasOptional(x => x.Support)
                .WithMany(x => x.SupportedProducts);

            modelBuilder.Entity<ProductImage>()
                .HasOptional(x => x.Picture)
                .WithMany(x => x.ProductImages);

            modelBuilder.Entity<Quote>()
                .HasOptional(x => x.CustomerStore)
                .WithMany(x => x.Quotes);

            modelBuilder.Entity<Quote>()
                .HasOptional(x => x.Employee)
                .WithMany(x => x.Quotes);

            modelBuilder.Entity<QuoteItem>()
                .HasOptional(x => x.Product)
                .WithMany(x => x.QuoteItems);

            modelBuilder.Entity<CustomerCommunication>()
                .HasOptional(x => x.CustomerEmployee)
                .WithMany(x => x.CustomerCommunications);

            modelBuilder.Entity<CustomerCommunication>()
                .HasOptional(x => x.Employee)
                .WithMany(x => x.Employees);
        }
    }
    public class DatabaseVersion : DatabaseObject {
        public DateTime Date { get; set; }
    }
}
