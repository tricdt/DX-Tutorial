using DevExpress.Xpf.DemoBase;
using System;
using System.Data.Entity;
using System.IO;

namespace GridDemo {
    public class OutlookDataContext : DbContext {
        public static readonly string FileName = Path.GetFullPath("OutlookData.db");
        public OutlookDataContext()
            : base(SQLiteDataBaseGenerator.CreateConnection(EntityFrameworkInstantFeedbackModeViewModel.DatabaseInfo), true) {
        }
        public DbSet<OutlookDataRecord> OutlookDataRecords { get; set; }
    }
    public class OutlookDataRecord {
        public long Id { get; set; }
        public DateTime Sent { get; set; }
        public bool HasAttachment { get; set; }
        public long Size { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
    }
}
