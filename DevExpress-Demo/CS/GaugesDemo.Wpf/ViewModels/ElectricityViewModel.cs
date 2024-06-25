using DevExpress.Mvvm;
using System;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace GaugesDemo {
    public class ElectricityViewModel {
        readonly Random rand = new Random();
        readonly DispatcherTimer timer = new DispatcherTimer();

        public virtual double Voltage { get; protected set; }
        public virtual double Amperage { get; protected set; }
        public virtual double Power { get; protected set; }
        public int MaxVoltage { get { return 10; } }
        public int MaxAmperage { get { return 3; } }

        protected ElectricityViewModel() {
            timer.Interval = TimeSpan.FromSeconds(3);
        }
        void Timer_Tick(object source, EventArgs e) {
            Voltage = rand.Next(1, MaxVoltage);
            Amperage = (double)rand.Next(3, MaxAmperage * 10) / 10.0;
            Power = Voltage * Amperage;
        }
        public void Start() {
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        public void Stop() {
            timer.Stop();
            timer.Tick -= Timer_Tick;
        }
    }

    [ContentProperty("EasingFunction")]
    public class EasingFunctionItem : BindableBase {
        public IEasingFunction EasingFunction { get; set; }

        public override string ToString() {
            return EasingFunction != null ? EasingFunction.GetType().Name : "Default";
        }
    }

    public class DoubleToTimeSpanConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        #region IValueConvector implementation
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return TimeSpan.FromSeconds(Math.Ceiling((double)value));
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
