using DevExpress.Diagram.Demos;
using DevExpress.Mvvm;
using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public class DatabaseDiagramViewModel : ViewModelBase {
        public DatabaseDefinition Database { get; private set; }

        public DatabaseDiagramViewModel() {
            Database = DatabaseData.GetDatabaseDefinition();
        }
    }
}
