using DevExpress.DemoData.Models.Mapping;
using DevExpress.Internal;
using DevExpress.Xpf.DemoCenterBase;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.SQLite;
using System.IO;

namespace DevExpress.DemoData.Models {
    public partial class Department {
        public long ID { get; set; }
        public string Name { get; set; }
    }
}

namespace DevExpress.DemoData {
    using DevExpress.DemoData.Models;
    using DevExpress.Mvvm.DataAnnotations;

    public class IssueDataLoader : DataLoaderBase {
        bool itemsLoaded = false;
        bool projectsLoaded = false;
        bool usersLoaded = false;
        IssueListContext context = new IssueListContext();
        public ObservableCollection<Item> Items {
            get {
                LoadIfNeed(ref itemsLoaded, context.Items);
                return context.Items.Local;
            }
        }
        public ObservableCollection<Project> Projects {
            get {
                LoadIfNeed(ref projectsLoaded, context.Projects);
                return context.Projects.Local;
            }
        }
        public ObservableCollection<User> Users {
            get {
                LoadIfNeed(ref usersLoaded, context.Users);
                return context.Users.Local;
            }
        }
    }
    public enum IssueType { Request, Bug }

    static class IssuePriorityHelper {
        public const string CellMergingImagesPath = "pack://application:,,,/GridDemo;component/Images/CellMerging/";
    }
    public enum IssuePriority {
        [Image(IssuePriorityHelper.CellMergingImagesPath + "Low.svg")]
        Low,
        [Image(IssuePriorityHelper.CellMergingImagesPath + "Medium.svg")]
        Medium,
        [Image(IssuePriorityHelper.CellMergingImagesPath + "High.svg")]
        High 
    }
    public enum Status { New, Postponed, Fixed, Rejected }
}


namespace DevExpress.DemoData.Models {
    public partial class Item {
        public long ID { get; set; }
        public string Name { get; set; }
        public IssueType Type { get; set; }
        public Nullable<long> ProjectID { get; set; }
        public Nullable<IssuePriority> Priority { get; set; }
        public Nullable<Status> Status { get; set; }
        public Nullable<long> CreatorID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> OwnerID { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.DateTime> FixedDate { get; set; }
        public string Description { get; set; }
        public string Resolution { get; set; }
    }
}


namespace DevExpress.DemoData.Models {
    public partial class IssueListContext : DbContext {
        public IssueListContext() : base(CreateConnection(), true) { }
        public IssueListContext(string connectionString) : base(connectionString) { }
        public IssueListContext(DbConnection connection) : base(connection, true) { }

        static IssueListContext defaultContext;
        public static IssueListContext Default {
            get { return defaultContext ?? (defaultContext = new IssueListContext()); }
        }
        
        static IssueListContext() {
            Database.SetInitializer<IssueListContext>(null);
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTeam> ProjectTeams { get; set; }
        public DbSet<Scheduler> Schedulers { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new ItemMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new ProjectTeamMap());
            modelBuilder.Configurations.Add(new SchedulerMap());
            modelBuilder.Configurations.Add(new TaskMap());
            modelBuilder.Configurations.Add(new UserMap());
        }

        static string filePath;
        static DbConnection CreateConnection() {
            if(filePath == null)
                filePath = DemoRunner.GetDBFileSafe("issue-list.db");
            try {
                var attributes = File.GetAttributes(filePath);
                if(attributes.HasFlag(FileAttributes.ReadOnly)) {
                    File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                }
            } catch { }
            var connection = DbProviderFactories.GetFactory("System.Data.SQLite.EF6").CreateConnection();
            if(filePath != DemoRunner.DBFileFailedString)
                connection.ConnectionString = new SQLiteConnectionStringBuilder { DataSource = filePath }.ConnectionString;
            return connection;
        }
    }
}


namespace DevExpress.DemoData.Models {
    public partial class Project {
        public long ID { get; set; }
        public string Name { get; set; }
        public Nullable<long> ManagerID { get; set; }
    }
}


namespace DevExpress.DemoData.Models {
    public partial class ProjectTeam {
        public long ID { get; set; }
        public Nullable<long> ProjectID { get; set; }
        public Nullable<long> UserID { get; set; }
        public string Function { get; set; }
    }
}


namespace DevExpress.DemoData.Models {
    public partial class Scheduler {
        public long ID { get; set; }
        public Nullable<long> ProjectID { get; set; }
        public Nullable<long> UserID { get; set; }
        public Nullable<short> Sunday { get; set; }
        public Nullable<short> Monday { get; set; }
        public Nullable<short> Tuesday { get; set; }
        public Nullable<short> Wednesday { get; set; }
        public Nullable<short> Thursday { get; set; }
        public Nullable<short> Friday { get; set; }
        public Nullable<short> Saturday { get; set; }
    }
}


namespace DevExpress.DemoData.Models {
    public partial class Task {
        public long ID { get; set; }
        public string Name { get; set; }
        public long ParentID { get; set; }
        public long UserID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public bool Done { get; set; }
        public Nullable<long> Priority { get; set; }
        public Nullable<double> Complete { get; set; }
    }
}


namespace DevExpress.DemoData.Models {
    public partial class User {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string HomePage { get; set; }
        public Nullable<long> DepartmentID { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}


namespace DevExpress.DemoData.Models.Mapping {
    public class DepartmentMap : EntityTypeConfiguration<Department> {
        public DepartmentMap() {
            
            this.HasKey(t => t.ID);
            
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.Name)
                .HasMaxLength(100);
            
            this.ToTable("Departments");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}


namespace DevExpress.DemoData.Models.Mapping {
    public class ItemMap : EntityTypeConfiguration<Item> {
        public ItemMap() {
            
            this.HasKey(t => new { t.ID });
            
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.Name)
                .HasMaxLength(50);
            this.Property(t => t.Description)
                .HasMaxLength(2147483647);
            this.Property(t => t.Resolution)
                .HasMaxLength(2147483647);
            
            this.ToTable("Items");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.ProjectID).HasColumnName("ProjectID");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatorID).HasColumnName("CreatorID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.OwnerID).HasColumnName("OwnerID");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.FixedDate).HasColumnName("FixedDate");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Resolution).HasColumnName("Resolution");
        }
    }
}


namespace DevExpress.DemoData.Models.Mapping {
    public class ProjectMap : EntityTypeConfiguration<Project> {
        public ProjectMap() {
            
            this.HasKey(t => t.ID);
            
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.Name)
                .HasMaxLength(100);
            
            this.ToTable("Projects");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ManagerID).HasColumnName("ManagerID");
        }
    }
}


namespace DevExpress.DemoData.Models.Mapping {
    public class ProjectTeamMap : EntityTypeConfiguration<ProjectTeam> {
        public ProjectTeamMap() {
            
            this.HasKey(t => t.ID);
            
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.Function)
                .HasMaxLength(50);
            
            this.ToTable("ProjectTeam");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ProjectID).HasColumnName("ProjectID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.Function).HasColumnName("Function");
        }
    }
}


namespace DevExpress.DemoData.Models.Mapping {
    public class SchedulerMap : EntityTypeConfiguration<Scheduler> {
        public SchedulerMap() {
            
            this.HasKey(t => t.ID);
            
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            
            this.ToTable("Scheduler");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ProjectID).HasColumnName("ProjectID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.Sunday).HasColumnName("Sunday");
            this.Property(t => t.Monday).HasColumnName("Monday");
            this.Property(t => t.Tuesday).HasColumnName("Tuesday");
            this.Property(t => t.Wednesday).HasColumnName("Wednesday");
            this.Property(t => t.Thursday).HasColumnName("Thursday");
            this.Property(t => t.Friday).HasColumnName("Friday");
            this.Property(t => t.Saturday).HasColumnName("Saturday");
        }
    }
}


namespace DevExpress.DemoData.Models.Mapping {
    public class TaskMap : EntityTypeConfiguration<Task> {
        public TaskMap() {
            
            this.HasKey(t => new { t.ID, t.Name, t.ParentID, t.UserID, t.Done });
            
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);
            this.Property(t => t.ParentID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.UserID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            
            this.ToTable("Tasks");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ParentID).HasColumnName("ParentID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.Done).HasColumnName("Done");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.Complete).HasColumnName("Complete");
        }
    }
}


namespace DevExpress.DemoData.Models.Mapping {
    public class UserMap : EntityTypeConfiguration<User> {
        public UserMap() {
            
            this.HasKey(t => t.ID);
            
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.FirstName)
                .HasMaxLength(25);
            this.Property(t => t.MiddleName)
                .HasMaxLength(20);
            this.Property(t => t.LastName)
                .HasMaxLength(25);
            this.Property(t => t.Country)
                .HasMaxLength(15);
            this.Property(t => t.PostalCode)
                .HasMaxLength(10);
            this.Property(t => t.City)
                .HasMaxLength(15);
            this.Property(t => t.Address)
                .HasMaxLength(60);
            this.Property(t => t.Phone)
                .HasMaxLength(24);
            this.Property(t => t.Fax)
                .HasMaxLength(24);
            this.Property(t => t.Email)
                .HasMaxLength(50);
            this.Property(t => t.HomePage)
                .HasMaxLength(50);
            
            this.ToTable("Users");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Fax).HasColumnName("Fax");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.HomePage).HasColumnName("HomePage");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
        }
    }
}
