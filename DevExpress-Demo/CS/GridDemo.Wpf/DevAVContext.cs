using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using DevExpress.DemoData;
using DevExpress.Internal;

namespace DevExpress.DevAV {    
    public class DevAVDbContext : DevAVDbBase {
        static DevAVDbContext() {
            Database.SetInitializer<DevAVDbContext>(null);
        }
    }
}
