using System;
using System.Collections.Generic;
#if DXCORE3
using Microsoft.EntityFrameworkCore;
using System.IO;
using DevExpress.Internal;
#else
using System.Data.Entity;
#endif
using System.Linq;
using System.Text;

namespace GridDemo {
    public static class DevAVHelper {
        static List<DevExpress.DevAV.Employee> employees = null;
        public static List<DevExpress.DevAV.Employee> Employees {
            get {
                if(employees == null) {
#if DXCORE3
                    SetFilePath();
                    var devAvDb = new DevExpress.DevAV.DevAVDb(string.Format("Data Source={0}", filePath));
                    devAvDb.Pictures.Load();
#else
                    var devAvDb = new DevExpress.DevAV.DevAVDbContext(); 
#endif
                    devAvDb.Employees.Load();
                    employees = devAvDb.Employees.Local.ToList();
                }
                return employees;
            }
        }
#if DXCORE3
        static string filePath;
        static void SetFilePath() {
            if(filePath == null)
                filePath = DataDirectoryHelper.GetFile("devav.sqlite3", DataDirectoryHelper.DataFolderName);
            try {
                var attributes = File.GetAttributes(filePath);
                if(attributes.HasFlag(FileAttributes.ReadOnly)) {
                    File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                }
            } catch { }
        }
#endif
        internal static DevExpress.DevAV.Employee GetEmployee(string email) {
            return Employees.SingleOrDefault(x => x.Email.Equals(email));
        }
    }
}
