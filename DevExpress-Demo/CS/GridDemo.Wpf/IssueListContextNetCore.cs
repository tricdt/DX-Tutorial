using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.IO;
using DevExpress.DemoData.Models.Mapping;
using DevExpress.Internal;
using DevExpress.Xpf.DemoCenterBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DbModelBuilder = Microsoft.EntityFrameworkCore.ModelBuilder;

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
                return context.Items.Local.ToObservableCollection();
            }
        }
        public ObservableCollection<Project> Projects {
            get {
                LoadIfNeed(ref projectsLoaded, context.Projects);
                return context.Projects.Local.ToObservableCollection();
            }
        }
        public ObservableCollection<User> Users {
            get {
                LoadIfNeed(ref usersLoaded, context.Users);
                return context.Users.Local.ToObservableCollection();
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
        public IssueListContext() : base() {
            SetFilePath();
            if(filePath != DemoRunner.DBFileFailedString)
                connectionString = string.Format("Data Source={0}", filePath);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseLazyLoadingProxies().UseSqlite(connectionString);
        }
        string connectionString;

        static IssueListContext defaultContext;
        public static IssueListContext Default {
            get { return defaultContext ?? (defaultContext = new IssueListContext()); }
        }
        
        public DbSet<Department> Departments { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTeam> ProjectTeams { get; set; }
        public DbSet<Scheduler> Schedulers { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new DepartmentMap());
            modelBuilder.ApplyConfiguration(new ItemMap());
            modelBuilder.ApplyConfiguration(new ProjectMap());
            modelBuilder.ApplyConfiguration(new ProjectTeamMap());
            modelBuilder.ApplyConfiguration(new SchedulerMap());
            modelBuilder.ApplyConfiguration(new TaskMap());
            modelBuilder.ApplyConfiguration(new UserMap());
        }

        static string filePath;
        static void SetFilePath() {
            if(filePath == null)
                filePath = DemoRunner.GetDBFileSafe("issue-list.db");
            try {
                var attributes = File.GetAttributes(filePath);
                if(attributes.HasFlag(FileAttributes.ReadOnly)) {
                    File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                }
            } catch { }
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
    public class DepartmentMap : IEntityTypeConfiguration<Department> {
        public void Configure(EntityTypeBuilder<Department> builder) {
            
            builder.HasKey(t => t.ID);
            
            builder.Property(t => t.ID)
                .ValueGeneratedNever();
            builder.Property(t => t.Name)
                .HasMaxLength(100);
            
            builder.ToTable("Departments");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.Name).HasColumnName("Name");
        }
    }
}


namespace DevExpress.DemoData.Models.Mapping {
    public class ItemMap : IEntityTypeConfiguration<Item> {
        public void Configure(EntityTypeBuilder<Item> builder) {
            
            builder.HasKey(t => new { t.ID });
            
            builder.Property(t => t.ID)
                .ValueGeneratedNever();
            builder.Property(t => t.Name)
                .HasMaxLength(50);
            builder.Property(t => t.Description)
                .HasMaxLength(2147483647);
            builder.Property(t => t.Resolution)
                .HasMaxLength(2147483647);
            
            builder.ToTable("Items");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.Type).HasColumnName("Type");
            builder.Property(t => t.ProjectID).HasColumnName("ProjectID");
            builder.Property(t => t.Priority).HasColumnName("Priority");
            builder.Property(t => t.Status).HasColumnName("Status");
            builder.Property(t => t.CreatorID).HasColumnName("CreatorID");
            builder.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            builder.Property(t => t.OwnerID).HasColumnName("OwnerID");
            builder.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            builder.Property(t => t.FixedDate).HasColumnName("FixedDate");
            builder.Property(t => t.Description).HasColumnName("Description");
            builder.Property(t => t.Resolution).HasColumnName("Resolution");
        }
    }
}


namespace DevExpress.DemoData.Models.Mapping {
    public class ProjectMap : IEntityTypeConfiguration<Project> {
        public void Configure(EntityTypeBuilder<Project> builder) {
            
            builder.HasKey(t => t.ID);
            
            builder.Property(t => t.ID)
                .ValueGeneratedNever();
            builder.Property(t => t.Name)
                .HasMaxLength(100);
            
            builder.ToTable("Projects");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.ManagerID).HasColumnName("ManagerID");
        }
    }
}


namespace DevExpress.DemoData.Models.Mapping {
    public class ProjectTeamMap : IEntityTypeConfiguration<ProjectTeam> {
        public void Configure(EntityTypeBuilder<ProjectTeam> builder) {
            
            builder.HasKey(t => t.ID);
            
            builder.Property(t => t.ID)
                .ValueGeneratedNever();
            builder.Property(t => t.Function)
                .HasMaxLength(50);
            
            builder.ToTable("ProjectTeam");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.ProjectID).HasColumnName("ProjectID");
            builder.Property(t => t.UserID).HasColumnName("UserID");
            builder.Property(t => t.Function).HasColumnName("Function");
        }
    }
}


namespace DevExpress.DemoData.Models.Mapping {
    public class SchedulerMap : IEntityTypeConfiguration<Scheduler> {
        public void Configure(EntityTypeBuilder<Scheduler> builder) {
            
            builder.HasKey(t => t.ID);
            
            builder.Property(t => t.ID)
                .ValueGeneratedNever();
            
            builder.ToTable("Scheduler");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.ProjectID).HasColumnName("ProjectID");
            builder.Property(t => t.UserID).HasColumnName("UserID");
            builder.Property(t => t.Sunday).HasColumnName("Sunday");
            builder.Property(t => t.Monday).HasColumnName("Monday");
            builder.Property(t => t.Tuesday).HasColumnName("Tuesday");
            builder.Property(t => t.Wednesday).HasColumnName("Wednesday");
            builder.Property(t => t.Thursday).HasColumnName("Thursday");
            builder.Property(t => t.Friday).HasColumnName("Friday");
            builder.Property(t => t.Saturday).HasColumnName("Saturday");
        }
    }
}


namespace DevExpress.DemoData.Models.Mapping {
    public class TaskMap : IEntityTypeConfiguration<Task> {
        public void Configure(EntityTypeBuilder<Task> builder) {
            
            builder.HasKey(t => new { t.ID, t.Name, t.ParentID, t.UserID, t.Done });
            
            builder.Property(t => t.ID)
                .ValueGeneratedNever();
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(t => t.ParentID)
                .ValueGeneratedNever();
            builder.Property(t => t.UserID)
                .ValueGeneratedNever();
            
            builder.ToTable("Tasks");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.ParentID).HasColumnName("ParentID");
            builder.Property(t => t.UserID).HasColumnName("UserID");
            builder.Property(t => t.StartDate).HasColumnName("StartDate");
            builder.Property(t => t.EndDate).HasColumnName("EndDate");
            builder.Property(t => t.Done).HasColumnName("Done");
            builder.Property(t => t.Priority).HasColumnName("Priority");
            builder.Property(t => t.Complete).HasColumnName("Complete");
        }
    }
}


namespace DevExpress.DemoData.Models.Mapping {
    public class UserMap : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {
            
            builder.HasKey(t => t.ID);
            
            builder.Property(t => t.ID)
                .ValueGeneratedNever();
            builder.Property(t => t.FirstName)
                .HasMaxLength(25);
            builder.Property(t => t.MiddleName)
                .HasMaxLength(20);
            builder.Property(t => t.LastName)
                .HasMaxLength(25);
            builder.Property(t => t.Country)
                .HasMaxLength(15);
            builder.Property(t => t.PostalCode)
                .HasMaxLength(10);
            builder.Property(t => t.City)
                .HasMaxLength(15);
            builder.Property(t => t.Address)
                .HasMaxLength(60);
            builder.Property(t => t.Phone)
                .HasMaxLength(24);
            builder.Property(t => t.Fax)
                .HasMaxLength(24);
            builder.Property(t => t.Email)
                .HasMaxLength(50);
            builder.Property(t => t.HomePage)
                .HasMaxLength(50);
            
            builder.ToTable("Users");
            builder.Property(t => t.ID).HasColumnName("ID");
            builder.Property(t => t.FirstName).HasColumnName("FirstName");
            builder.Property(t => t.MiddleName).HasColumnName("MiddleName");
            builder.Property(t => t.LastName).HasColumnName("LastName");
            builder.Property(t => t.Country).HasColumnName("Country");
            builder.Property(t => t.PostalCode).HasColumnName("PostalCode");
            builder.Property(t => t.City).HasColumnName("City");
            builder.Property(t => t.Address).HasColumnName("Address");
            builder.Property(t => t.Phone).HasColumnName("Phone");
            builder.Property(t => t.Fax).HasColumnName("Fax");
            builder.Property(t => t.Email).HasColumnName("Email");
            builder.Property(t => t.HomePage).HasColumnName("HomePage");
            builder.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
        }
    }
}
