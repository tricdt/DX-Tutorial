using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors.Settings;

namespace DockingDemo {
    [CodeFile("ViewModels/DashboardViewModel.(cs)")]
    public partial class Dashboard : DockingDemoModule {
        public Dashboard() {
            InitializeComponent();
            
        }
    }
    public class ChartPaletteToMapColorsConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Palette chartColors = (Palette)value;
            DoubleCollection rangeStops = (DoubleCollection)parameter;
            int colorsCount = (int)(rangeStops[rangeStops.Count - 1] - rangeStops[0]) + 1;
            DevExpress.Xpf.Map.ColorCollection mapColors = new DevExpress.Xpf.Map.ColorCollection();
            for(int i = 0; i < colorsCount; i++)
                mapColors.Add(chartColors[i]);
            return mapColors;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
    public class RevenueFormatTextEditSettings : TextEditSettings {
        public override string GetDisplayTextFromEditor(object editValue) {
            return "$" + Format((decimal)editValue);
        }
        string Format(decimal source) {
            var integer = Math.Truncate(source);
            if(integer > 999999999)
                return integer.ToString("0,,,.###B", CultureInfo.InvariantCulture);
            else if(integer > 999999)
                return integer.ToString("0,,.##M", CultureInfo.InvariantCulture);
            else if(integer > 999)
                return integer.ToString("0,.#K", CultureInfo.InvariantCulture);
            else 
                return integer.ToString("", CultureInfo.InvariantCulture);
        }
    }
    public class UniformGridReorderBehavior : Behavior<UniformGrid> {
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.SizeChanged += AssociatedObject_SizeChanged;
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.SizeChanged -= AssociatedObject_SizeChanged;
        }
        void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs e) {
            if(AssociatedObject.ActualWidth < 200) {
                AssociatedObject.Rows = 6;
                AssociatedObject.Columns = 1;
            } else if(AssociatedObject.ActualWidth < 340) {
                AssociatedObject.Rows = 3;
                AssociatedObject.Columns = 2;
            } else {
                AssociatedObject.Rows = 2;
                AssociatedObject.Columns = 3;
            }
        }
    }
}
