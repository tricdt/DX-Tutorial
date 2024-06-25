using DevExpress.DemoData;
using DevExpress.Mvvm;
using DevExpress.Xpf.PivotGrid;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace PivotGridDemo {
    public class MVVMPivotViewModel : ViewModelBase {
        public MVVMPivotViewModel() {
            FormatConditionsSource = new List<FormatConditionItem> {
                new FormatConditionItem { Type = FormatConditionType.DataBar, MeasureName = "fieldModelPrice", ColumnName = "fieldYear", RowName = "fieldName", PredefinedFormatName = "BlueGradientDataBar" },
                new FormatConditionItem { Type = FormatConditionType.DataBar, MeasureName = "fieldModelPrice", ColumnName = "fieldMonth", RowName = "fieldName", PredefinedFormatName = "BlueGradientDataBar" },
                new FormatConditionItem { Type = FormatConditionType.DataBar, MeasureName = "fieldModelPrice", ColumnName = "fieldDay", RowName = "fieldName", PredefinedFormatName = "BlueGradientDataBar" },
                new FormatConditionItem { Type = FormatConditionType.DataBar, MeasureName = "fieldModelPrice", ColumnName = "fieldYear", RowName = "fieldTrademark", PredefinedFormatName = "OrangeGradientDataBar" },
                new FormatConditionItem { Type = FormatConditionType.DataBar, MeasureName = "fieldModelPrice", ColumnName = "fieldMonth", RowName = "fieldTrademark", PredefinedFormatName = "OrangeGradientDataBar" },
                new FormatConditionItem { Type = FormatConditionType.DataBar, MeasureName = "fieldModelPrice", ColumnName = "fieldDay", RowName = "fieldTrademark", PredefinedFormatName = "OrangeGradientDataBar" },
            };
            GroupsSource = new List<string> { "PivotGridGroup1" };
            FieldsSource = new List<FieldItem> {
                new FieldItem { Name = "fieldTrademark", DataBinding = new DataSourceColumnBinding("Trademark"), Area = FieldArea.RowArea, Caption = "Trademark", Width = 140d },
                new FieldItem { Name = "fieldName", DataBinding = new DataSourceColumnBinding("Name"), Area = FieldArea.RowArea, Caption = "Name", Width = 140d },
                new FieldItem { Name = "fieldYear", DataBinding = new DataSourceColumnBinding("SalesDate", FieldGroupInterval.DateYear), Area = FieldArea.ColumnArea, Caption = "Year", Width = 140d, GroupName = "PivotGridGroup1" },
                new FieldItem { Name = "fieldMonth", DataBinding = new DataSourceColumnBinding("SalesDate", FieldGroupInterval.DateMonth), Area = FieldArea.ColumnArea, Caption = "Month", Width = 140d, GroupName = "PivotGridGroup1" },
                new FieldItem { Name = "fieldDay", DataBinding = new DataSourceColumnBinding("SalesDate", FieldGroupInterval.DateDay), Area = FieldArea.ColumnArea, Caption = "Day", Width = 140d, GroupName = "PivotGridGroup1" },
                new FieldItem { Name = "fieldModelPrice", DataBinding = new DataSourceColumnBinding("ModelPrice"), Area = FieldArea.DataArea, Caption = "Extended Price", Width = 140d },
                new FieldItem { Name = "fieldModification", DataBinding = new DataSourceColumnBinding("Modification"), Area = FieldArea.FilterArea, Caption = "Modification", Width = 140d },
                new FieldItem { Name = "fieldBodyStyle", DataBinding = new DataSourceColumnBinding("BodyStyle"), Area = FieldArea.FilterArea, Caption = "Body Style", Width = 140d },
                new FieldItem { Name = "fieldSalesDate", DataBinding = new DataSourceColumnBinding("SalesDate", FieldGroupInterval.Date), Area = FieldArea.FilterArea, Caption = "Sales Date", Width = 140d },
                new FieldItem { Name = "fieldModelPrice1", DataBinding = new DataSourceColumnBinding("ModelPrice"), Area = FieldArea.FilterArea, Caption = "Model Price", Width = 140d },
                new FieldItem { Name = "fieldMPGCity", DataBinding = new DataSourceColumnBinding("MPGCity"), Area = FieldArea.FilterArea, Caption = "MPG City", Width = 140d },
                new FieldItem { Name = "fieldMPGHighway", DataBinding = new DataSourceColumnBinding("MPGHighway"), Area = FieldArea.FilterArea, Caption = "MPG Highway", Width = 140d },
            };
            DataSource = VehiclesData.GetMDBData();
        }
        public object DataSource { get; private set; }
        public IEnumerable<FormatConditionItem> FormatConditionsSource { get; private set; }
        public IEnumerable<string> GroupsSource { get; private set; }
        public IEnumerable<FieldItem> FieldsSource { get; private set; }
    }
}
