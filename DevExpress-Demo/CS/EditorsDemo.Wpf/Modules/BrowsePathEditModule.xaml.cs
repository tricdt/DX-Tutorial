using DevExpress.Xpf.DemoBase;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace EditorsDemo {
    [CodeFile("ViewModels/BrowsePathEditModuleViewModel.(cs)")]
    public partial class BrowsePathEditModule : EditorsDemoModule {
        public BrowsePathEditModule() {
            InitializeComponent();
        }
    }

    public class PngSizeToTextConverter : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            var intValue = (int)value;
            if(intValue == 0)
                return string.Empty;

            return string.Format("PNG ({0} kb.)", intValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
