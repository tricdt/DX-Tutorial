using DevExpress.DemoData.Models;
using DevExpress.Xpf.Grid;
using System;
using System.Windows.Data;

namespace GridDemo {
    public class CustomerDetailsConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            RowData rowData = value as RowData;
            if(rowData == null)
                return null;
            Customer row = (Customer)rowData.Row;
            return String.Format("{0}{1}, {2}, {3}\r\n{4}, {5}", row.Region, row.Country, row.City, row.PostalCode, row.Address, row.Phone);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
