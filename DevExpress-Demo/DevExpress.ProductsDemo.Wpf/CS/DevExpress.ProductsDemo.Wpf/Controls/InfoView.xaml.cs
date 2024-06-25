using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf;
using DevExpress.Xpf.Core.Native;
using DevExpress.Mvvm.Native;

namespace ProductsDemo {
    public partial class InfoView : UserControl {
        public InfoView() {
            InitializeComponent();
        }
    }
    public class AboutInfoToControlAboutConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            ControlAbout control = new ControlAbout((AboutInfo)value);
            control.SizeChanged += control_SizeChanged;
            return control;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
        void control_SizeChanged(object sender, SizeChangedEventArgs e) {
            ControlAbout control = (ControlAbout)sender;
            LayoutHelper.FindElementByName(control, "CloseButton").Do(x => x.Visibility = Visibility.Collapsed);
        }
    }
}
