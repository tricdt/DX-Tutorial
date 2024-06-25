using DevExpress.Xpf.DemoBase;
using PivotGridDemo.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace PivotGridDemo {
    public class ServerModeViewModel : ServerModeViewModelBase<OrderDataRecord> {
        #region DatabaseInfo
        public static readonly DatabaseInfo DatabaseInfo = new DatabaseInfo(
            OrderDataContext.FileName, "OrderDataRecords", typeof(OrderDataRecord), DatabaseHelper.CreateValues, (sql, connection) => new SQLiteCommand(sql, (SQLiteConnection)connection));
        #endregion

        protected ServerModeViewModel()
            : base(DatabaseInfo, () => new OrderDataContext().OrderDataRecords) {
        }
    }
}
