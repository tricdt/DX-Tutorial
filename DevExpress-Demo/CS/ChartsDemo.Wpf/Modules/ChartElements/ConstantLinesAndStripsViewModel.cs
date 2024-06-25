using System.ComponentModel;

namespace ChartsDemo {
    public class ConstantLinesAndStripsViewModel : INotifyPropertyChanged {
        OperatingSurfaceTemperatureData data = new OperatingSurfaceTemperatureData();
        double optimalTemperature;

        public OperatingSurfaceTemperatureData Data { get { return data; } }
        public double OptimalTemperature {
            get { return optimalTemperature; }
            set {
                if (value < data.MinOptimalTemperature)
                    optimalTemperature = data.MinOptimalTemperature;
                else if (value > data.MaxOptimalTemperature)
                    optimalTemperature = data.MaxOptimalTemperature;
                else
                    optimalTemperature = value;
                OnPropertyChanged("OptimalTemperature");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ConstantLinesAndStripsViewModel() {
            optimalTemperature = data.OptimalTemperature;
        }

        void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }
    }
}
