using DevExpress.Xpf.DemoBase;
using System.Collections.Generic;
using System.Linq;
#if !DXCORE3
using System.Data.SQLite;
#else
using Microsoft.Data.Sqlite;
#endif

namespace GridDemo {
    public class EntityFrameworkInstantFeedbackModeViewModel : InstantFeedbackModeViewModelBase<OutlookDataRecord> {
#region DatabaseInfo
#if !DXCORE3
        public static readonly DatabaseInfo DatabaseInfo = new DatabaseInfo(
            OutlookDataContext.FileName, "OutlookDataRecords", typeof(OutlookDataRecord), CreateValues, (sql, connection) => new SQLiteCommand(sql, (SQLiteConnection)connection));
#else
        public static readonly DatabaseInfo DatabaseInfo = new DatabaseInfo(
            OutlookDataContext.FileName, "OutlookDataRecords", typeof(OutlookDataRecord), CreateValues, (sql, connection) => new SqliteCommand(sql, (SqliteConnection)connection));
#endif
        static Dictionary<string, object> CreateValues() {
            var subject = OutlookDataGenerator.GetSubject();
            var hasAttachment = OutlookDataGenerator.GetHasAttachment();
            var size = OutlookDataGenerator.GetSize(hasAttachment);
            var from = OutlookDataGenerator.GetFrom();
            var sent = OutlookDataGenerator.GetSentDate();
            var dict = new Dictionary<string, object>();
            dict.Add("Subject", subject);
            dict.Add("HasAttachment", hasAttachment);
            dict.Add("Size", size);
            dict.Add("From", from);
            dict.Add("Sent", sent);
            return dict;
        }
#endregion
        protected EntityFrameworkInstantFeedbackModeViewModel()
            : base(DatabaseInfo, () => new OutlookDataContext().OutlookDataRecords) {
        }
    }
}
