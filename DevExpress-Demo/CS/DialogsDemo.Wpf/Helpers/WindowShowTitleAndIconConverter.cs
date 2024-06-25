using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace CommonDemo.Helpers {
    public class WindowShowTitleAndIconConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (bool)values[0];
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            return new object[] { (bool)value, (bool)value };
        }
    }


    
}
