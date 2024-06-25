using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PropertyGridDemo {
    public class CategoryNameToImageSourceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var category = (string)value;
            if(string.IsNullOrEmpty(category))
                return null;
            category = category.Replace(" ", "").Replace("/", "");
            return ImageHelper.GetSvgImage(@"Products/" + category);
        }
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
