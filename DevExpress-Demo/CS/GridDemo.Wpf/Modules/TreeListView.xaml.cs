using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.Grid;
using System;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GridDemo {
    public partial class TreeListView : GridDemoModule {
        int maxId;
        public TreeListView() {
            InitializeComponent();
            maxId = EmployeesWithPhotoData.DataSource.Max(x => x.Id);
        }
        void OnInitNewNode(object sender, DevExpress.Xpf.Grid.TreeList.TreeListNodeEventArgs e) {
            view.SetNodeValue(e.Node, "Id", ++maxId);
        }
    }
    public class EmployeeCategoryImageSelector : TreeListNodeImageSelector {
        public override ImageSource Select(DevExpress.Xpf.Grid.TreeList.TreeListRowData rowData) {
            Employee empl = (rowData.Row as Employee);
            if(empl == null || string.IsNullOrEmpty(empl.GroupName))
                return null;
            var extension = new SvgImageSourceExtension() { Uri = new Uri(GroupNameToImageConverterExtension.GetImagePathByGroupName(empl.GroupName)), Size = new System.Windows.Size(16, 16) };
            return (ImageSource)extension.ProvideValue(null);
        }
    }
}
