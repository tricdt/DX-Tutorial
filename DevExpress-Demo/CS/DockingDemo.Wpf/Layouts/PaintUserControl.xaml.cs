using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Xpf.Core;

namespace DockingDemo {
    public partial class PaintUserControl : UserControl {
        public PaintUserControl() {
            InitializeComponent();
        }
    }
    public class EditWidthConverter : MarkupExtension, IValueConverter {
        public double EditWidth { get; set; }
        public double TouchScaleFactor { get; set; }

        public EditWidthConverter() { TouchScaleFactor = 2d; }
        public override object ProvideValue(IServiceProvider serviceProvider) { return this; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var walker = value as ThemeTreeWalker;
            if(walker != null && (walker.IsTouch || walker.ThemeName == Theme.TouchlineDarkName))
                return EditWidth * TouchScaleFactor;
            return EditWidth;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
