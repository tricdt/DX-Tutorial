using System;
using System.Globalization;
using System.Windows.Data;

namespace ProductsDemo {
    class BillionStringToShortStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(!(value is string))
                return null;
            string billionFormatString = "0,,.0M";
            string thousandsFormatString = "0,.0K";

            decimal dec;
            bool parsed = decimal.TryParse((string)value, out dec);
            if(parsed) {
                if(dec == 0)
                    return "0";
                else if(dec >= 0.1M * 1000000M)
                    return dec.ToString(billionFormatString);
                else
                    return dec.ToString(thousandsFormatString);
            } else
                return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
