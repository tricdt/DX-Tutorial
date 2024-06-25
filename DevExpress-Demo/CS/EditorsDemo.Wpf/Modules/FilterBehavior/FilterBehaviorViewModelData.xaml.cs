using DevExpress.Xpf.Core.FilteringUI;
using DevExpress.Xpf.Data;
using System;
using System.Linq;
using System.Windows.Controls;

namespace EditorsDemo.FilterBehavior {

    public partial class FilterBehaviorViewModelData : UserControl {
        FilterBehaviorViewModelDataViewModel ViewModel { get { return (FilterBehaviorViewModelDataViewModel)DataContext; } }
        public FilterBehaviorViewModelData() {
            InitializeComponent();
        }

        void filterBehavior_ActualFilterCriteriaChanged(object sender, EventArgs e) {
            if(ReferenceEquals(filterBehavior.ActualFilterCriteria, null)) {
                ViewModel.SetFilter(null);
            } else {
                var converter = new GridFilterCriteriaToExpressionConverter<ProductInfo>();
                var expression = converter.Convert(filterBehavior.ActualFilterCriteria);
                ViewModel.SetFilter(expression);
            }
        }

        void filterBehavior_CustomUniqueValues(object sender, CustomUniqueValuesEventArgs e) {
            if(e.FieldName != "CategoryName")
                throw new InvalidOperationException();
            e.CountedValues = ViewModel.GetCategoryUniqueValues();
        }
    }
}
