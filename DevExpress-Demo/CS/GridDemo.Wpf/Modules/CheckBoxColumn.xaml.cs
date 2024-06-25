using System;
using System.Globalization;
using System.Windows.Data;
using DevExpress.Xpf.Core;

namespace GridDemo {
    public partial class CheckBoxColumn : GridDemoModule {
        public CheckBoxColumn() {
            InitializeComponent();
        }
    }

    public class ProductIdToImageConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int categoryId = 0;
            if(value == null || !int.TryParse(value.ToString(), out categoryId))
                return null;   
            string name = null;
            switch(categoryId) {
                case 1:
                    name = "Beverages";
                    break;
                case 2:
                    name = "condiments";
                    break;
                case 3:
                    name = "confections";
                    break;
                case 4:
                    name = "DairyProduct";
                    break;
                case 5:
                    name = "grains_cereals";
                    break;
                case 6:
                    name = "MeatPoultry";
                    break;
                case 7:
                    name = "produce";
                    break;
                case 8:
                    name = "Seafood";
                    break;
                    default:
                    return null;
            }         
            SvgImageSourceExtension extension = new SvgImageSourceExtension() {
                Uri = new Uri(string.Format(@"pack://application:,,,/GridDemo;component/Images/Products/{0}.svg", name))
            };
            return extension.ProvideValue(null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
