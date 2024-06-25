using DevExpress.Data;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GridDemo {
    public class DemoDataProvider {
        public User[] Users { get { return OutlookData.Users; } }
        public SummaryItemType[] SummaryItemTypes {
            get {
                return new SummaryItemType[] {
                    SummaryItemType.Sum,
                    SummaryItemType.Min,
                    SummaryItemType.Max,
                    SummaryItemType.Count,
                    SummaryItemType.Average
                };
            }
        }
        public string[] SummaryFieldNames {
            get {
                return new string[] {
                        "UnitPrice",
                        "Quantity",
                        "Discount",
                        "ExtendedPrice",
                        "Freight",
                        "Total"
                };
            }
        }
        public static readonly string[] PalleteSizesStatic = new string[] { "4x4", "10x10", "16x16", "20x20", "25x25" };
        public string[] PalleteSizes { get { return PalleteSizesStatic; } }
    }
    public enum Priority { Low, BelowNormal, Normal, AboveNormal, High }
    [Serializable]
    [POCOViewModel]
    public class OutlookData {
        public static readonly User[] Users = new User[] {
            new User() { Id = 0, Name = "Peter Dolan"},
            new User() { Id = 1, Name = "Ryan Fischer"},
            new User() { Id = 2, Name = "Richard Fisher"},
            new User() { Id = 3, Name = "Tom Hamlett" },
            new User() { Id = 4, Name = "Mark Hamilton" },
            new User() { Id = 5, Name = "Steve Lee" },
            new User() { Id = 6, Name = "Jimmy Lewis" },
            new User() { Id = 7, Name = "Jeffrey W McClain" },
            new User() { Id = 8, Name = "Andrew Miller" },
            new User() { Id = 9, Name = "Dave Murrel" },
            new User() { Id = 10, Name = "Bert Parkins" },
            new User() { Id = 11, Name = "Mike Roller" },
            new User() { Id = 12, Name = "Ray Shipman" },
            new User() { Id = 13, Name = "Paul Bailey" },
            new User() { Id = 14, Name = "Brad Barnes" },
            new User() { Id = 15, Name = "Carl Lucas" },
            new User() { Id = 16, Name = "Jerry Campbell" },
        };
        public virtual int? OID { get; set; }
        public virtual string Subject { get; set; }
        public virtual string From { get; set; }
        public virtual DateTime? Sent { get; set; }
        public virtual bool? HasAttachment { get; set; }
        public virtual long? Size { get; set; }
        public virtual double? HoursActive { get; set; }
        public virtual Priority? Priority { get; set; }
        public virtual int? UserId { get; set; }

        public override string ToString() {
            return Subject;
        }
    }
    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString() {
            return Name;
        }
    }
    public static class OutlookDataGenerator {
        static Random rnd = new Random();
        public static string[] Subjects = new string[] { "Developer Express MasterView. Integrating the control into an Accounting System.",
                                                "Web Edition: Data Entry Page. There is an issue with date validation.",
                                                "Payables Due Calculator is ready for testing.",
                                                "Web Edition: Search Page is ready for testing.",
                                                "Main Menu: Duplicate Items. Somebody has to review all menu items in the system.",
                                                "Receivables Calculator. Where can I find the complete specs?",
                                                "Ledger: Inconsistency. Please fix it.",
                                                "Receivables Printing module is ready for testing.",
                                                "Screen Redraw. Somebody has to look at it.",
                                                "Email System. What library are we going to use?",
                                                "Cannot add new vendor. This module doesn't work!",
                                                "History. Will we track sales history in our system?",
                                                "Main Menu: Add a File menu. File menu item is missing.",
                                                "Currency Mask. The current currency mask in completely unusable.",
                                                "Drag & Drop operations are not available in the scheduler module.",
                                                "Data Import. What database types will we support?",
                                                "Reports. The list of incomplete reports.",
                                                "Data Archiving. We still don't have this features in our application.",
                                                "Email Attachments. Is it possible to add multiple attachments? I haven't found a way to do this.",
                                                "Check Register. We are using different paths for different modules.",
                                                "Data Export. Our customers asked us for export to Microsoft Excel"};

        public static string GetSubject() {
            return Subjects[rnd.Next(Subjects.Length - 1)];
        }

        public static string GetFrom() {
            return OutlookData.Users[GetFromId()].Name;
        }

        public static DateTime GetSentDate() {
            DateTime ret = DateTime.Today;
            int r = rnd.Next(12);
            if(r > 1)
                ret = ret.AddDays(-rnd.Next(50));
            return ret;
        }
        public static long? GetSize(bool? largeData) {
            return 1000 + (largeData.Value ? 20 * rnd.Next(10000) : 30 * rnd.Next(100));
        }
        public static bool GetHasAttachment() {
            return rnd.Next(10) > 5;
        }
        public static Priority GetPriority() {
            return (Priority)rnd.Next(5);
        }
        public static int GetHoursActive() {
            return (int)Math.Round(rnd.NextDouble() * 1000, 1);
        }
        public static int GetFromId() {
            return rnd.Next(OutlookData.Users.Length);
        }
        public static OutlookData CreateOutlookData(int id) {
            OutlookData data = ViewModelSource.Create(() => new OutlookData());
            data.OID = id;
            data.From = GetFrom();
            data.UserId = GetFromId();
            data.Sent = GetSentDate();
            data.HasAttachment = GetHasAttachment();
            data.Priority = GetPriority();
            data.HoursActive = GetHoursActive();
            data.Subject = GetSubject();
            return data;
        }
        public static DataTable CreateOutlookDataTable(int rowCount) {
            DataTable table = new DataTable();
            table.Columns.Add("OID", typeof(int));
            table.Columns.Add("From", typeof(string));
            table.Columns.Add("ToId", typeof(int));
            table.Columns.Add("Sent", typeof(DateTime));
            table.Columns.Add("HasAttachment", typeof(bool));
            table.Columns.Add("Priority", typeof(int));
            table.Columns.Add("HoursActive", typeof(int));
            for(int i = 0; i < rowCount; i++) {
                DataRow row = table.NewRow();
                row["OID"] = i;
                row["From"] = GetFrom();
                row["ToId"] = GetFromId();
                row["Sent"] = GetSentDate();
                row["HasAttachment"] = GetHasAttachment();
                row["Priority"] = (int)GetPriority();
                row["HoursActive"] = GetHoursActive();
                table.Rows.Add(row);
            }
            return table;
        }
        public static List<OutlookData> CreateOutlookDataList(int rowCount) {
            return Enumerable.Range(0, rowCount)
                .Select(i => CreateOutlookData(i))
                .ToList();
        }
    }
}
