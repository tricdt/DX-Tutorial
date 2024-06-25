using System.Windows;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    public partial class Groups : PivotGridDemoModule {
        public Groups() {
            InitializeComponent();
        }
        void UpdateGroupsExpanded(bool expanded) {
            foreach(PivotGridGroup group in pivotGrid.Groups)
                foreach(PivotGridField field in group)
                    field.ExpandedInFieldsGroup = expanded;
        }
        void Collapse_Click(object sender, RoutedEventArgs e) {
            UpdateGroupsExpanded(false);
        }
        void Expand_Click(object sender, RoutedEventArgs e) {
            UpdateGroupsExpanded(true);
        }
    }
}
