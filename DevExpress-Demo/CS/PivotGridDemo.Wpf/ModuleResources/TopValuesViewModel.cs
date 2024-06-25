using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    [POCOViewModel]
    public class TopValuesViewModel {
        [BindableProperty(OnPropertyChangedMethodName = "UpdateTopValueCount")]
        public virtual Decimal TopValueCount { get; set; }
        [BindableProperty(OnPropertyChangedMethodName = "UpdateTopValueCount")]
        public virtual bool TopValueShowOthers { get; set; }

        public IEnumerable<FieldViewModel> Fields { get; private set; }
        public IEnumerable<FieldViewModel> RowsAndColumns { get; private set; }
        public TopValuesViewModel() {
            Fields = new List<FieldViewModel> {
                FieldViewModel.Create("Order ID", "OrderID", FieldArea.RowArea, 0),
                FieldViewModel.Create("Order Amount", "ExtendedPrice", FieldArea.DataArea, 0),
                FieldViewModel.Create("Category Name", "CategoryName", FieldArea.RowArea, 1),
                FieldViewModel.Create("Sales Person", "FullName", FieldArea.RowArea, 2),
                FieldViewModel.Create("Product Name", "ProductName", FieldArea.RowArea, 3),
            };
            RowsAndColumns = Fields.Where(x => x.Area != FieldArea.DataArea);
            SelectedField = Fields.Last();
            TopValueCount = 5;
        }
        [BindableProperty(OnPropertyChangedMethodName = "OnSelectedFieldChanged")]
        public virtual FieldViewModel SelectedField { get; set; }
        protected void OnSelectedFieldChanged() {
            var fields = RowsAndColumns.Except(SelectedField.Yield());
            fields.ForEach(x => x.Visible = false);
            SelectedField.Visible = true;
            SelectedField.SortOrder = FieldSortOrder.Descending;
            SelectedField.SortByFieldName = "ExtendedPrice";
            UpdateTopValueCount();
        }
        protected void UpdateTopValueCount() {
            SelectedField.TopValueCount = TopValueCount;
            SelectedField.TopValueShowOthers = TopValueShowOthers;
        }
    }
}
