using DevExpress.Xpf.Grid;
using System.ComponentModel;
using System.Windows;

namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/GroupIntervalData.(cs)")]
    public partial class SortBySummary : GridDemoModule {
        #region static
        public static readonly DependencyProperty IsSelectedProperty;
        static SortBySummary() {
            IsSelectedProperty = DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(SortBySummary), new PropertyMetadata(false));
        }
        public static void SetIsSelected(DependencyObject element, bool value) {
            element.SetValue(IsSelectedProperty, value);
        }
        public static int GetIsSelected(DependencyObject element) {
            return (int)element.GetValue(IsSelectedProperty);
        }
        #endregion 
        public SortBySummary() {
            InitializeComponent();
            grid.GroupBy("OrderDate");
            sortModeList.SelectedIndex = 0;
        }
        ListSortDirection CurrentSortOrder { get { return sortModeList.SelectedIndex % 2 == 0 ? ListSortDirection.Ascending : ListSortDirection.Descending; } }
        int CurrentSummaryItemIndex { get { return (int)(sortModeList.SelectedIndex / 2); } }
        private void sortModeList_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            grid.GroupSummarySortInfo.Clear();
            for(int i = 0; i < grid.GroupSummary.Count; i++) {
                DevExpress.Xpf.Grid.GridSummaryItem item = grid.GroupSummary[i];
                if(i == CurrentSummaryItemIndex) {
                    SortBySummary.SetIsSelected(item, true);
                    grid.GroupSummarySortInfo.Add(new GridGroupSummarySortInfo(item, "OrderDate", CurrentSortOrder));
                }
                else {
                    SortBySummary.SetIsSelected(item, false);
                }
            }
        }
    }
}
