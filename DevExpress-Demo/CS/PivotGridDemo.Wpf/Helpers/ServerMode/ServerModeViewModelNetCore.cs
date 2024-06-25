using DevExpress.Xpf.DemoBase;
using Microsoft.Data.Sqlite;
using PivotGridDemo.Helpers;
using System.Collections.Generic;

namespace PivotGridDemo {
    public class ServerModeViewModel : ServerModeViewModelBase<OrderDataRecord> {
        #region DatabaseInfo
        public static readonly DatabaseInfo DatabaseInfo = new DatabaseInfo(
            OrderDataContext.FileName, "OrderDataRecords", typeof(OrderDataRecord), DatabaseHelper.CreateValues, (sql, connection) => new SqliteCommand(sql, (SqliteConnection)connection));
        #endregion

        protected ServerModeViewModel()
            : base(DatabaseInfo, () => new OrderDataContext().OrderDataRecords) {
        }
    }
}
