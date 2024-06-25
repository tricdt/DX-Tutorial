using DevExpress.Xpf.DemoBase.DataClasses;
using System.Xml;

namespace GridDemo {
    public partial class BindingToXML : GridDemoModule {
        public BindingToXML() {
            InitializeComponent();
            XmlDocument document = new XmlDocument();
            document.Load(EmployeesWithPhotoData.GetDataStream());
            grid.ItemsSource = document.SelectNodes("/Employees/Employee");
        }
    }
}
