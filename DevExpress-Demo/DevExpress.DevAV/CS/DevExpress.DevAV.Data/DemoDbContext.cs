using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Internal;
using System.Runtime.CompilerServices;

namespace DevExpress.DemoData {
    class DemoDbContextConfiguration : DbConfiguration {
        public DemoDbContextConfiguration() {
            if(!DemoDbContext.IsWebModel) {
                LoadSQLite();
            }
        }

        [MethodImplAttribute(MethodImplOptions.NoInlining)]
        void LoadSQLite() {
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", System.Data.SQLite.EF6.SQLiteProviderFactory.Instance);
            SetProviderServices("System.Data.SQLite", GetProviderServices());
            SetProviderFactoryResolver(new SQLiteProviderFactoryResolver());
        }

        class SQLiteProviderFactoryResolver : IDbProviderFactoryResolver {
            DbProviderFactory IDbProviderFactoryResolver.ResolveProviderFactory(DbConnection connection) {
                if(connection is SQLiteConnection)
                    return SQLiteFactory.Instance;
                if(connection is System.Data.Entity.Core.EntityClient.EntityConnection) {
                    return System.Data.Entity.Core.EntityClient.EntityProviderFactory.Instance;
                }
                return null;
            }
        }

        static System.Data.Entity.Core.Common.DbProviderServices GetProviderServices() {
            return (System.Data.Entity.Core.Common.DbProviderServices)
                    System.Data.SQLite.EF6.SQLiteProviderFactory.Instance.GetService(
                        typeof(System.Data.Entity.Core.Common.DbProviderServices));
        }
    }
    public class DemoDbContext : DbContext {
        public DemoDbContext(string dbName) : base(CreateConnection(dbName), true) { }
        public DemoDbContext(string nameOrConnectionString, bool token) : base(nameOrConnectionString) { }

        static DbConnection CreateConnection(string dbName) {
            var filePath = DevAVDataDirectoryHelper.GetFile(dbName, DevAVDataDirectoryHelper.DataFolderName);
            try {
                var attributes = File.GetAttributes(filePath);
                if(attributes.HasFlag(FileAttributes.ReadOnly)) {
                    File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                }
            } catch { }

            return new SQLiteConnection {
                ConnectionString = new SQLiteConnectionStringBuilder { DataSource = filePath }.ConnectionString
            };
        }

        public static bool IsWebModel { get; set; }
    }
}
