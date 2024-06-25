using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TreeListDemo {
    
    
    
    public partial class UnboundColumns : TreeListDemoModule {
        public UnboundColumns() {
            InitializeComponent();
            this.treeList.View.ExpandAllNodes();            
        }

        private void showExpressionEditorButton_Click(object sender, RoutedEventArgs e) {
            treeList.View.ShowUnboundExpressionEditor(treeList.Columns[(string)((ListBoxItem)columnsList.SelectedItem).Tag]);
        }

        private void view_UnboundExpressionEditorCreated(object sender, DevExpress.Xpf.Grid.UnboundExpressionEditorEventArgs e) {
            e.AutoCompleteExpressionEditorControl.Context.Columns.Add(new DevExpress.Data.Controls.ExpressionEditor.ColumnInfo() { Name = "Title", Category = "Columns", Type = typeof(string), Description = "string Title" });
            e.AutoCompleteExpressionEditorControl.Context.Columns.Add(new DevExpress.Data.Controls.ExpressionEditor.ColumnInfo() { Name = "FirstName", Category = "Columns", Type = typeof(string), Description = "string FirstName" });
            e.AutoCompleteExpressionEditorControl.Context.Columns.Add(new DevExpress.Data.Controls.ExpressionEditor.ColumnInfo() { Name = "LastName", Category = "Columns", Type = typeof(string), Description = "string LastName" });
            e.AutoCompleteExpressionEditorControl.Context.Columns.Add(new DevExpress.Data.Controls.ExpressionEditor.ColumnInfo() { Name = "BirthDate", Category = "Columns", Type = typeof(DateTime), Description = "string BirthDate" });
        }
    }
    public class BooleanToVisibilityConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value is Boolean && (bool)value)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
    
