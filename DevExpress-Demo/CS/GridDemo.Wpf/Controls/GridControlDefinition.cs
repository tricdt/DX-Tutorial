using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using System.Collections.Generic;
using System.Windows;

namespace GridDemo {
    public class GridControlDefinitionCollection : List<GridControlDefinition> { }
    public class GridControlDefinition : DependencyObject {
        public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register("DataSource", typeof(object), typeof(GridControlDefinition), new PropertyMetadata(null));
        public object DataSource {
            get { return GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }
        public string Name { get; set; }
        List<GridColumnDefinition> columns;
        public List<GridColumnDefinition> Columns { get { return columns; } }
        public GridControlDefinition() {
            columns = new List<GridColumnDefinition>();
        }
    }
    public class GridColumnDefinition {
        public string FieldName { get; set; }
        public DataTemplate CellTemplate { get; set; }
        public GridColumnWidth Width { get; set; }
        public bool FixedWidth { get; set; }
        public BaseEditSettings EditSettings { get; set; }
        public object Header { get; set; }
        public bool ReadOnly { get; set; }
        public GridColumnDefinition() {
            Width = double.NaN;
        }
    }
}
