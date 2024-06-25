using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using DevExpress.Xpf.DemoBase.DataClasses;

namespace EditorsDemo {
    public partial class RatingEditModule : EditorsDemoModule {
        public RatingEditModule() {
            InitializeComponent();
        }
    }
    public class RatingEditViewModel {
        public virtual Color Background { get; set; }
        public virtual Color HoverBackground { get; set; }
        public virtual Color SelectedBackground { get; set; }
        public virtual List<CarRating> Cars { get; set; }
        public virtual CarRating SelectedCar { get; set; }
        public RatingEditViewModel() {
            Background = Colors.Transparent;
            HoverBackground = Colors.Transparent;
            SelectedBackground = Colors.Transparent;
            CreateCarsSource();
            SelectedCar = Cars[0];
        }

        void CreateCarsSource() {
            var carsSource = CarsData.DataSource;
            List<CarRating> cars = new List<CarRating>();
            Random rand = new Random();
            foreach (var car in carsSource) {
                cars.Add(new CarRating() {
                    Car = car,
                    SpeedRating = rand.Next(5, 10),
                    ComfortRating = rand.Next(5, 10),
                    QualityRating = rand.Next(5, 10),
                    PriceRating = rand.Next(5, 10),
                });
            }
            Cars = cars;
        }
    }
    public class ColorDisplayTextConverter : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if((Color)value == Colors.Transparent) return "Automatic";
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return new ColorDisplayTextConverter();
        }
    }
    public class IsNullColorConverter : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return ((Color)value) == Colors.Transparent;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return new IsNullColorConverter();
        }
    }
    public class ColorToBrushConverter : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value == null ? null : new SolidColorBrush((Color)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return new ColorToBrushConverter();
        }
    }
    public class CarRating {
        public Cars Car { get; set; }
        public double SpeedRating { get; set; }
        public double ComfortRating { get; set; }
        public double QualityRating { get; set; }
        public double PriceRating { get; set; }
    }
}
