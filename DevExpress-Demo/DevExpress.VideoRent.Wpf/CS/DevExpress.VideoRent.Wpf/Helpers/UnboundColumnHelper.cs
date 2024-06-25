using System;
using System.Windows;
using System.Windows.Data;
using System.Collections.Generic;
using DevExpress.Xpf.Grid;
using DevExpress.Data;
using DevExpress.VideoRent.ViewModel.Helpers;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public abstract class UnboundColumnBase : FrameworkElement {
        #region Dependency Properties
        public static readonly DependencyProperty ColumnNameProperty;
        static UnboundColumnBase() {
            Type ownerType = typeof(UnboundColumnBase);
            ColumnNameProperty = DependencyProperty.Register("ColumnName", typeof(string), ownerType, new PropertyMetadata(null));
        }
        #endregion

        public string ColumnName { get { return (string)GetValue(ColumnNameProperty); } set { SetValue(ColumnNameProperty, value); } }
        public abstract void ReadValue(GridControl gridControl, GridColumnDataEventArgs e);
        public abstract void WriteValue(GridControl gridControl, GridColumnDataEventArgs e);
    }
    public class CommandUnboundColumn : UnboundColumnBase {
        public event CommandEventHandler ValueReading;
        public event CommandEventHandler ValueWriting;

        public override void ReadValue(GridControl gridControl, GridColumnDataEventArgs e) {
            CommandEventArgs args = new CommandEventArgs(gridControl.GetRowByListIndex(e.ListSourceRowIndex), null);
            if(ValueReading != null)
                ValueReading(this, args);
            e.Value = args.Value;
        }
        public override void WriteValue(GridControl gridControl, GridColumnDataEventArgs e) {
            CommandEventArgs args = new CommandEventArgs(gridControl.GetRowByListIndex(e.ListSourceRowIndex), e.Value);
            if(ValueWriting != null)
                ValueWriting(this, args);
        }
    }
    public class UnboundColumn : UnboundColumnBase {
        #region Dependency Properties
        public static readonly DependencyProperty FieldNameProperty;
        public static readonly DependencyProperty ConverterProperty;
        public static readonly DependencyProperty ConverterParameterProperty;
        static UnboundColumn() {
            Type ownerType = typeof(UnboundColumn);
            FieldNameProperty = DependencyProperty.Register("FieldName", typeof(string), ownerType, new PropertyMetadata(null));
            ConverterProperty = DependencyProperty.Register("Converter", typeof(IValueConverter), ownerType, new PropertyMetadata(null));
            ConverterParameterProperty = DependencyProperty.Register("ConverterParameter", typeof(object), ownerType, new PropertyMetadata(null));
        }
        #endregion

        public string FieldName { get { return (string)GetValue(FieldNameProperty); } set { SetValue(FieldNameProperty, value); } }
        public IValueConverter Converter { get { return (IValueConverter)GetValue(ConverterProperty); } set { SetValue(ConverterProperty, value); } }
        public object ConverterParameter { get { return GetValue(ConverterParameterProperty); } set { SetValue(ConverterParameterProperty, value); } }
        public override void ReadValue(GridControl gridControl, GridColumnDataEventArgs e) {
            e.Value = Convert(e.GetListSourceFieldValue(FieldName));
        }
        public override void WriteValue(GridControl gridControl, GridColumnDataEventArgs e) {
            SetListSourceRowValue(gridControl, e.ListSourceRowIndex, FieldName, ConvertBack(e.Value));
        }
        object Convert(object value) {
            return Converter == null ? value : Converter.Convert(value, null, ConverterParameter, null);
        }
        object ConvertBack(object value) {
            return Converter == null ? value : Converter.ConvertBack(value, null, ConverterParameter, null);
        }
        static void SetListSourceRowValue(GridControl gridControl, int listSourceRowIndex, string columnName, object value) {
            DataColumnInfo column = gridControl.DataController.Columns[columnName];
            if(column == null || listSourceRowIndex < 0) return;
            gridControl.DataController.Helper.SetRowValue(listSourceRowIndex, column.Index, value);
        }
    }
    public class UnboundColumnCollection : List<UnboundColumnBase> { }
    public static class UnboundColumnHelper {
        #region Dependency Properties
        public static readonly DependencyProperty UnboundColumnsProperty;
        static UnboundColumnHelper() {
            Type ownerType = typeof(UnboundColumnHelper);
            UnboundColumnsProperty = DependencyProperty.RegisterAttached("UnboundColumns", typeof(UnboundColumnCollection), ownerType, new PropertyMetadata(null, RaiseUnboundColumnsChanged));
        }
        #endregion

        public static UnboundColumnCollection GetUnboundColumns(GridControl gridControl) {
            return (UnboundColumnCollection)gridControl.GetValue(UnboundColumnsProperty);
        }
        public static void SetUnboundColumns(GridControl gridControl, UnboundColumnCollection unboundColumns) {
            gridControl.SetValue(UnboundColumnsProperty, unboundColumns);
        }
        static void RaiseUnboundColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            GridControl gridControl = (GridControl)d;
            gridControl.CustomUnboundColumnData -= OnGridControlCustomUnboundColumnData;
            gridControl.CustomUnboundColumnData += OnGridControlCustomUnboundColumnData;
        }
        static void OnGridControlCustomUnboundColumnData(object sender, GridColumnDataEventArgs e) {
            GridControl gridControl = (GridControl)sender;
            if(e.IsGetData) {
                UnboundColumnBase unboundColumn = FindUnboundColumn(gridControl, e.Column.FieldName);
                if(unboundColumn != null)
                    unboundColumn.ReadValue(gridControl, e);
                return;
            }
            if(e.IsSetData) {
                UnboundColumnBase unboundColumn = FindUnboundColumn(gridControl, e.Column.FieldName);
                if(unboundColumn != null)
                    unboundColumn.WriteValue(gridControl, e);
                return;
            }
        }
        static UnboundColumnBase FindUnboundColumn(GridControl gridControl, string name) {
            List<UnboundColumnBase> unboundColumns = GetUnboundColumns(gridControl);
            foreach(UnboundColumnBase unboundColumn in unboundColumns) {
                if(unboundColumn.ColumnName == name) return unboundColumn;
            }
            return null;
        }
    }
}
