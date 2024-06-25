using System;
using System.Globalization;
using System.Windows.Data;
using DevExpress.Mvvm;
using DevExpress.Xpf.Charts;

namespace CommonChartsDemo {
    public class AggregateTypeToAggregateFunctionConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            EnumMemberInfo aType = value as EnumMemberInfo;
            if (aType == null)
                return AggregateFunction.None;
            return (AggregateFunction)Convert.ToInt32(aType.Id);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        #endregion
    }
}
