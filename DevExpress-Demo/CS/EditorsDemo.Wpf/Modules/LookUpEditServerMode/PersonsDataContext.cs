using DevExpress.Xpf.DemoBase;
using System.Data.Entity;
using System.IO;

namespace GridDemo {
    public class PersonsDataContext : DbContext {
        public static readonly string FileName = Path.GetFullPath("PersonData.db");
        public PersonsDataContext()
            : base(SQLiteDataBaseGenerator.CreateConnection(LookUpInstantFeedbackModeViewModel.DatabaseInfo), true) {
        }
        public DbSet<Person> People { get; set; }
    }
}
