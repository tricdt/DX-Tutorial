using System.Collections.ObjectModel;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.DataClasses;

namespace TreeListDemo {
    public partial class DragDrop : TreeListDemoModule {
        public DragDrop() {
            InitializeComponent();
        }

        void OnDragRecordOver(object sender, DragRecordOverEventArgs e) {
            if(e.IsFromOutside && typeof(Employee).IsAssignableFrom(e.GetRecordType()))
                e.Effects = System.Windows.DragDropEffects.Move;
            if(e.DropPosition == DropPosition.Inside && allowDropInside.IsChecked == false)
                e.DropPosition = e.DropPositionRelativeCoefficient > 0.5 ? DropPosition.After : DropPosition.Before;
            e.Handled = true;
        }
    }

    public class DragDropViewModel {
        public DragDropViewModel() {
            ActiveEmployees = new ObservableCollection<Employee>(DragDropSourceGenerator.GetActiveEmployees());
            NewEmployees = new ObservableCollection<Employee>(DragDropSourceGenerator.GetNewEmployees());
        }

        public ObservableCollection<Employee> ActiveEmployees { get; set; }
        public ObservableCollection<Employee> NewEmployees { get; set; }
    }
}
