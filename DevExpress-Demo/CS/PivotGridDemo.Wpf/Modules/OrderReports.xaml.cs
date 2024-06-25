using System.Linq;
using System.Windows;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    
    public partial class OrderReports : PivotGridDemoModule {

        public OrderReports() {
            InitializeComponent();
        }
        private void ListBoxEdit_SelectionChanged(object sender, RoutedEventArgs e) {
            if(!IsLoaded) return;
            pivotGrid.BeginUpdate();
            groupOptions.Visibility = System.Windows.Visibility.Collapsed;
            fieldOrder.FilterValues.Clear();
            fieldOrder.FilterValues.FilterType = FieldFilterType.Excluded;
            fieldOrder.Area = FieldArea.RowArea;
            fieldUnitPrice.Area = FieldArea.DataArea;
            fieldDiscount.Area = FieldArea.DataArea;
            fieldExtendedPrice.Area = FieldArea.DataArea;
            fieldQuantity.Area = FieldArea.DataArea;

            switch(reportsList.SelectedIndex) {
                case 0:
                    break;
                case 1:
                    fieldOrder.AreaIndex = 0;
                    groupOptions.Visibility = System.Windows.Visibility.Visible;
                    if(orderIDFilter.Items.Count == 0) {
                        orderIDFilter.Items.AddRange(fieldOrder.GetUniqueValues());
                        orderIDFilter.SelectedIndex = 0;
                    }
                    SetFilter();
                    break;
                case 2:
                    fieldOrder.Area = FieldArea.FilterArea;
                    fieldUnitPrice.Area = FieldArea.FilterArea;
                    fieldDiscount.Area = FieldArea.FilterArea;
                    fieldExtendedPrice.Area = FieldArea.FilterArea;
                    break;
                case 3:
                    fieldOrder.Area = FieldArea.FilterArea;
                    fieldQuantity.Area = FieldArea.FilterArea;
                    fieldDiscount.Area = FieldArea.FilterArea;
                    fieldExtendedPrice.Area = FieldArea.FilterArea;
                    break;
            }
            pivotGrid.EndUpdate();
        }

        private void orderIDFilter_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            SetFilter();
        }

        void SetFilter() {
            fieldOrder.FilterValues.FilterType = FieldFilterType.Included;
            fieldOrder.FilterValues.Clear();
            fieldOrder.FilterValues.Add(orderIDFilter.SelectedItem);
        }
    }
}
