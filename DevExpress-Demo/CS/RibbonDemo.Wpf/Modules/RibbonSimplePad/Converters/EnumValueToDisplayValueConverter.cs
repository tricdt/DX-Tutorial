using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace RibbonDemo {
    public class EnumValueToDisplayValueConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null)
                return DependencyProperty.UnsetValue;
            IEnumerable<Attribute> customAttributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(false).Cast<Attribute>();
            DisplayAttribute displayAttribute = customAttributes.OfType<DisplayAttribute>().FirstOrDefault();
            return displayAttribute == null ? Enum.GetName(value.GetType(), value) : displayAttribute.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
