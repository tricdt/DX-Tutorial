using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace EditorsDemo {
    public class GroupNameToImageConverter : IValueConverter {
        static readonly List<string> images = new List<string> { "administration", "inventory", "manufacturing", "quality", "research", "sales" };
        static GroupNameToImageConverter() {
            Instance = new GroupNameToImageConverter();
        }
        public static GroupNameToImageConverter Instance { get; private set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var group = value != null ? value.ToString().ToLower() : string.Empty;
            var imageName = images.FirstOrDefault(x => group.Contains(x));
            return !string.IsNullOrEmpty(imageName) ? new Uri(string.Format("pack://application:,,,/EditorsDemo;component/Images/Groups/{0}.svg", imageName)) : null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class ProductItemToImageConverter : IValueConverter {

        static ProductItemToImageConverter() {
            Instance = new ProductItemToImageConverter();
        }
        public static ProductItemToImageConverter Instance { get; private set; }

        readonly Regex regex = new Regex(@"[^A-Z]+", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var category = value != null ? value.ToString() : string.Empty;
            return !string.IsNullOrEmpty(category) ? new Uri(string.Format("pack://application:,,,/EditorsDemo;component/Images/Products/{0}.svg", Clear(category))) : null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        string Clear(string category) {
            return regex.Replace(category, "");
        }
    }
}
