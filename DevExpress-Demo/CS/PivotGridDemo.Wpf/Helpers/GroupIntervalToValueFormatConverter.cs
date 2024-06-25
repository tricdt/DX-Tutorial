using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {

    public class GroupIntervalToValueFormatConverter : MarkupExtension, IValueConverter {
        
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            FieldGroupInterval? groupInterval = value as FieldGroupInterval?;
            if(groupInterval == FieldGroupInterval.DateQuarter)
                return "Quarter {0}";
            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

}
