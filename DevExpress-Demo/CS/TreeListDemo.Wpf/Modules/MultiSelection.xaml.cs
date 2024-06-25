using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Grid;
using DevExpress.Data;

namespace TreeListDemo {
    
    
    
    public partial class MultiSelection : TreeListDemoModule {
        public MultiSelection() {
            InitializeComponent();
            treeList.SelectRange(0, 10);
        }     
        protected override void Show() {
            base.Show();
            treeList.UpdateTotalSummary();
        }       
    }
    #region Converters
    public class MultiSelectModeToBoolConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return !object.Equals(value, MultiSelectMode.None);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (bool)value ? MultiSelectMode.Row : MultiSelectMode.None;
        }
        #endregion
    }
    #endregion
}
