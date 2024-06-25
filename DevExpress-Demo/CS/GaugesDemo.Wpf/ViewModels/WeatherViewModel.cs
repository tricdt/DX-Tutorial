namespace GaugesDemo {
    public class WeatherViewModel {
        const int cloudyIndex = 0;
        const int snowyIndex = 1;
        const int rainyIndex = 2;
        const int sunnyIndex = 3;

        enum PressureState {
            Low,
            Normal,
            High,
            Undefined
        }

        enum TemperatureState {
            Low,
            High
        }

        PressureState pressure = PressureState.Normal;
        TemperatureState temperature = TemperatureState.High;

        public void LowRangeIndicatorEnter() {
            pressure = PressureState.Low;
            UpdateWeatherState();
        }
        public void LowRangeIndicatorLeave(double minValue, double value) {
            if(value < minValue) {
                pressure = PressureState.Undefined;
                UpdateWeatherState();
            }
        }
        public void NormalRangeIndicatorEnter() {
            pressure = PressureState.Normal;
            UpdateWeatherState();
        }
        public void NormalRangeIndicatorLeave(double minValue, double value) {
            pressure = value < minValue ? PressureState.Low : PressureState.High;
            UpdateWeatherState();
        }
        public void HighRangeIndicatorEnter() {
            pressure = PressureState.High;
            UpdateWeatherState();
        }
        public void HighRangeIndicatorLeave(double minValue, double value) {
            pressure = value < minValue ? PressureState.Normal : PressureState.Undefined;
            UpdateWeatherState();
        }
        public void HighTemperatureIndicatorEnter() {
            temperature = TemperatureState.High;
            UpdateWeatherState();
        }
        public void HighTemperatureIndicatorLeave() {
            temperature = TemperatureState.Low;
            UpdateWeatherState();
        }
        int GetStateIndex() {
            switch(pressure) {
                case PressureState.Low:
                    return temperature == TemperatureState.Low ? snowyIndex : rainyIndex;
                case PressureState.Normal:
                    return cloudyIndex;
                case PressureState.High:
                    return sunnyIndex;
                default:
                    return -1;
            }
        }
        void UpdateWeatherState() {
            StateIndex = GetStateIndex();
        }

        public virtual int StateIndex { get; protected set; }
    }
}
