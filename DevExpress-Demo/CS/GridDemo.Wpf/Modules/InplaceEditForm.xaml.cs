using DevExpress.DemoData.Models.Vehicles;
using DevExpress.Xpf.Grid;
using System.Linq;
using System.Windows;

namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/InplaceEditFormResources.xaml")]
    public partial class InplaceEditForm : GridDemoModule {
        public InplaceEditForm() {
            InitializeComponent();
            grid.ItemsSource = new VehiclesContext().Models.ToList();
        }

        void OnTemplateValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(tableView == null)
                return;
            int index = templatesListBox.SelectedIndex;
            if(index == 0)
                tableView.ClearValue(TableView.EditFormTemplateProperty);
            else if(index == 1)
                tableView.EditFormTemplate = (DataTemplate)Resources["CustomEditFormTemplate"];
        }
    }
}
