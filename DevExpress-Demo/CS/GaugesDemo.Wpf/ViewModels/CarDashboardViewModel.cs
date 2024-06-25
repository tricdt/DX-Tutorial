using System;
using System.Windows.Threading;

namespace GaugesDemo {
    public class CarDashboardViewModel {
        const double NormalEngineTemperature = 85;
        const int GearCount = 6;

        readonly DispatcherTimer timer = new DispatcherTimer();
        readonly DispatcherTimer timerInitialAnimation = new DispatcherTimer();
        readonly DispatcherTimer timerUpdateDateTime = new DispatcherTimer();

        double GearInteval { get { return 0.8 * (MaxSpeed / GearCount); } }

        public virtual bool IsAcceleratePressed { get; set; }
        public virtual bool IsBrakePressed { get; set; }

        public virtual double Speed { get; protected set; }
        public virtual double CurrentEngineTemperature { get; protected set; }
        public virtual double TachometerValue { get; protected set; }
        public virtual double FuelLevel { get; protected set; }
        public virtual DateTime CurrentDateTime { get; protected set; }

        public double TachometerMaxValue { get { return 8000; } }
        public double MaxEngineTemperature { get { return 130; } }
        public double MaxSpeed { get { return 120.0; } }

        public CarDashboardViewModel() {
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timerInitialAnimation.Interval = TimeSpan.FromMilliseconds(800);
            timerUpdateDateTime.Interval = TimeSpan.FromSeconds(1);

            CurrentEngineTemperature = 20;
            TachometerValue = 900;
            FuelLevel = 0.75;
            UpdateDateTime();
        }
        void OnTimedEventInitialAnimation(object source, EventArgs e) {
            timerInitialAnimation.Stop();
            Speed = 0;
            TachometerValue = 0;
            timer.Tick += OnTimedEvent;
            timer.Start();
        }
        void OnTimedEvent(object source, EventArgs e) {
            Update(timer.Interval.TotalSeconds);
        }
        void Update(double deltaTime) {
            UpdateSpeed(10 * deltaTime);
            int gear = (int)Math.Min(GearCount, Math.Ceiling(Speed / GearInteval));
            TachometerValue = gear > 0 ? TachometerMaxValue * (Speed - GearInteval * (gear - 1)) / GearInteval : 0;
            TachometerValue = Math.Max(0, Math.Min(TachometerMaxValue, TachometerValue));
            FuelLevel -= TachometerValue / TachometerMaxValue / 1000;
            if(((TachometerMaxValue / 2) < TachometerValue) || (CurrentEngineTemperature < NormalEngineTemperature))
                CurrentEngineTemperature += TachometerValue / TachometerMaxValue / 2.5;
            else
                CurrentEngineTemperature -= (TachometerMaxValue - TachometerValue) / TachometerMaxValue / 2.5;
            CurrentEngineTemperature = Math.Min(MaxEngineTemperature, CurrentEngineTemperature);
        }
        void UpdateSpeed(double delta) {
            if(IsAcceleratePressed)
                Speed += delta;
            else
                if(IsBrakePressed)
                    Speed -= delta;
            Speed = Math.Max(0, Math.Min(MaxSpeed, Speed));
        }
        void UpdateDateTime() {
            CurrentDateTime = DateTime.Now;
        }
        void OnTimedEventDateTime(object source, EventArgs e) {
            UpdateDateTime();
        }

        public void Start() {
            timerUpdateDateTime.Tick += OnTimedEventDateTime;
            timerUpdateDateTime.Start();

            Speed = MaxSpeed;
            TachometerValue = TachometerMaxValue;
            timerInitialAnimation.Tick += OnTimedEventInitialAnimation;
            timerInitialAnimation.Start();
        }
        public void Stop() {
            timerUpdateDateTime.Stop();
            timerUpdateDateTime.Tick -= OnTimedEventDateTime;
            timer.Stop();
            timer.Tick -= OnTimedEvent;
            timerInitialAnimation.Stop();
            timerInitialAnimation.Tick -= OnTimedEventInitialAnimation;
        }
    }
}
