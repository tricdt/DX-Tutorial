using System;
using System.Windows.Threading;

namespace GaugesDemo {
    public enum SegmentState {
        Enabled,
        Disabled,
        Blinking
    }

    public class TrafficLightsState {
        public static TrafficLightsState Red(int? resetTimerTicks = null) { return new TrafficLightsState(ArrowBlinking, redSegment: SegmentState.Enabled, arrowSegment: SegmentState.Enabled, gangerRedSegment: SegmentState.Enabled, resetTimerTicks: resetTimerTicks); }
        public static TrafficLightsState ArrowBlinking() { return new TrafficLightsState(YellowRed, redSegment: SegmentState.Enabled, arrowSegment: SegmentState.Blinking, gangerRedSegment: SegmentState.Enabled); }
        public static TrafficLightsState YellowRed() { return new TrafficLightsState(Green, redSegment: SegmentState.Enabled, yellowSegment: SegmentState.Enabled, gangerRedSegment: SegmentState.Enabled); }
        public static TrafficLightsState Green() { return new TrafficLightsState(GreenBlinking, greenSegment: SegmentState.Enabled, resetTimerTicks: 2); }
        public static TrafficLightsState GreenBlinking() { return new TrafficLightsState(Yellow, greenSegment: SegmentState.Blinking); }
        public static TrafficLightsState Yellow() { return new TrafficLightsState(() => Red(), yellowSegment: SegmentState.Enabled, gangerRedSegment: SegmentState.Enabled, resetTimerTicks: 4); }

        readonly Func<TrafficLightsState> next;
        public SegmentState GreenSegment { get; private set; }
        public SegmentState YellowSegment { get; private set; }
        public SegmentState RedSegment { get; private set; }
        public SegmentState ArrowSegment { get; private set; }
        public SegmentState GangerRedSegment { get; private set; }
        public int? ResetTimerTicks { get; private set; }

        TrafficLightsState(Func<TrafficLightsState> next, SegmentState greenSegment = SegmentState.Disabled, SegmentState yellowSegment = SegmentState.Disabled, SegmentState redSegment = SegmentState.Disabled, SegmentState arrowSegment = SegmentState.Disabled, SegmentState gangerRedSegment = SegmentState.Disabled, int? resetTimerTicks = null) {
            this.next = next;
            this.GreenSegment = greenSegment;
            this.YellowSegment = yellowSegment;
            this.RedSegment = redSegment;
            this.ArrowSegment = arrowSegment;
            this.GangerRedSegment = gangerRedSegment;
            this.ResetTimerTicks = resetTimerTicks;
        }
        public TrafficLightsState NextState() {
            return next();
        }
    }

    public class TrafficLightsViewModel {
        public virtual int WaitTime { get; protected set; }
        public virtual bool IsTimerGreen { get; protected set; }
        public virtual bool IsRedSegmentEnabled { get; protected set; }
        public virtual bool IsYellowSegmentEnabled { get; protected set; }
        public virtual bool IsGreenLeftSegmentEnabled { get; protected set; }
        public virtual bool IsGreenRightSegmentEnabled { get; protected set; }
        public virtual bool IsGangerRedSegmentEnabled { get; protected set; }
        public virtual bool IsGangerGreenSegmentEnabled { get; protected set; }

        const int initialTicksCount = 3;

        readonly DispatcherTimer blinkingTimer = new DispatcherTimer();
        readonly DispatcherTimer timer = new DispatcherTimer();
        TrafficLightsState currentState;
        int changeStateTicksCount;

        protected TrafficLightsViewModel() {
            TryChangeState(() => TrafficLightsState.Red(3));
            timer.Interval = TimeSpan.FromSeconds(1);
            blinkingTimer.Interval = TimeSpan.FromMilliseconds(300);
        }

        public void Start() {
            blinkingTimer.Tick += OnBlinkingTimedEvent;
            timer.Tick += OnTimedEvent;
            timer.Start();
        }
        public void Stop() {
            timer.Stop();
            timer.Tick -= OnTimedEvent;
            blinkingTimer.Stop();
            blinkingTimer.Tick -= OnBlinkingTimedEvent;
        }
        void UpdateSegemntsState() {
            if(currentState.ResetTimerTicks.HasValue)
                WaitTime = currentState.ResetTimerTicks.Value * initialTicksCount;

            IsGreenLeftSegmentEnabled = currentState.GreenSegment != SegmentState.Disabled;
            IsGreenRightSegmentEnabled = currentState.ArrowSegment != SegmentState.Disabled;
            IsYellowSegmentEnabled = currentState.YellowSegment != SegmentState.Disabled;
            IsRedSegmentEnabled = currentState.RedSegment != SegmentState.Disabled;
            IsGangerGreenSegmentEnabled = currentState.GreenSegment != SegmentState.Disabled;
            IsGangerRedSegmentEnabled = currentState.GangerRedSegment != SegmentState.Disabled;
            IsTimerGreen = !IsGangerRedSegmentEnabled;

            blinkingTimer.IsEnabled = currentState.GreenSegment == SegmentState.Blinking || currentState.ArrowSegment == SegmentState.Blinking;
            changeStateTicksCount = initialTicksCount;
        }
        void TryChangeState(Func<TrafficLightsState> nextState) {
            if(changeStateTicksCount == 0) {
                currentState = nextState();
                UpdateSegemntsState();
            }
            changeStateTicksCount--;
            WaitTime--;
        }
        void ChangeBlinkingState() {
            if(currentState.GreenSegment == SegmentState.Blinking) {
                IsGangerGreenSegmentEnabled = !IsGangerGreenSegmentEnabled;
                IsGreenLeftSegmentEnabled = !IsGreenLeftSegmentEnabled;
            }
            if(currentState.ArrowSegment == SegmentState.Blinking)
                IsGreenRightSegmentEnabled = !IsGreenRightSegmentEnabled;
        }
        void OnTimedEvent(object source, EventArgs e) {
            TryChangeState(() => currentState.NextState());
        }
        void OnBlinkingTimedEvent(object source, EventArgs e) {
            ChangeBlinkingState();
        }
    }
}
