using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Utils;
using DevExpress.Xpf.Gauges;

namespace GaugesDemo {
    public class OscilloscopeViewModel {
        const int toolTipOffset = 15;

        double dataOffset = -1;
        readonly DispatcherTimer timer = new DispatcherTimer();

        public virtual double HorizontalPosition { get; set; }
        public virtual double HorizontalSensitivity { get; set; }
        public virtual double VerticalPosition { get; set; }
        public virtual double VerticalSensitivity { get; set; }
        public virtual double ReferenceVoltage { get; set; }
        public virtual double HorizontalMin { get; set; }
        public virtual double HorizontalMax { get; set; }
        public virtual double VerticalMin { get; set; }
        public virtual double VerticalMax { get; set; }
        public virtual bool EnableSignalUp { get; set; }
        public virtual ArcScaleNeedle ActiveNeedle { get; protected set; }
        public virtual CircularGaugeControl ActiveGauge { get; protected set; }
        public virtual double ActiveGaugePositionX { get; protected set; }
        public virtual double ActiveGaugePositionY { get; protected set; }
        public virtual List<Point> Oscillations { get; protected set; }
        public List<GridLineItem> AxisGridLines { get; private set; }

        protected OscilloscopeViewModel() {
            timer.Interval = TimeSpan.FromMilliseconds(50);
            ReferenceVoltage = 2;
            VerticalSensitivity = 5.5;
            HorizontalSensitivity = 10.5;
            AxisGridLines = CreateOscilloscopeGrid().ToList();
            UpdateData();
        }

        void UpdateVerticalRange() {
            VerticalMin = VerticalPosition - 0.5 * VerticalSensitivity;
            VerticalMax = VerticalPosition + 0.5 * VerticalSensitivity;
        }
        void UpdateHorizontalRange() {
            HorizontalMin = HorizontalPosition - 0.5 * HorizontalSensitivity;
            HorizontalMax = HorizontalPosition + 0.5 * HorizontalSensitivity;
        }
        protected void OnVerticalSensitivityChanged() {
            UpdateVerticalRange();
        }
        protected void OnHorizontalSensitivityChanged() {
            UpdateHorizontalRange();
        }
        protected void OnVerticalPositionChanged() {
            UpdateVerticalRange();
        }
        protected void OnHorizontalPositionChanged() {
            UpdateHorizontalRange();
        }
        void OnTimedEvent(object source, EventArgs e) {
            UpdateData();
        }
        void UpdateData() {
            if(Math.Abs(ReferenceVoltage) <= 1)
                dataOffset = EnableSignalUp ? 0 : 1;
            else {
                dataOffset += 0.5;
                if(dataOffset > 1)
                    dataOffset = -1;
                if(dataOffset < -1)
                    dataOffset = 1;
            }
            List<Point> oscillations = new List<Point>();
            for(int i = -25; i < 25; i++) {
                oscillations.Add(new Point(i + dataOffset, Math.Abs(i % 2) * 2 - 1));
            }
            Oscillations = oscillations;
        }
        IEnumerable<GridLineItem> CreateOscilloscopeGrid() {
            for(double i = 0.25; i < 4; i += 0.25) {
                yield return new GridLineItem(i, (i / 0.25) % 2 == 0);
            }
        }
        public void ShowToolTip(CircularGaugeControl gauge, Point position) {
            ActiveNeedle = gauge.CalcHitInfo(position).Needle;
            if(ActiveNeedle != null) {
                ActiveGauge = gauge;
                ActiveGaugePositionX = position.X + toolTipOffset;
                ActiveGaugePositionY = position.Y - gauge.ActualHeight + toolTipOffset;
            }
        }
        public void HideToolTip() {
            ActiveNeedle = null;
        }
        public void Start() {
            timer.Tick += OnTimedEvent;
            timer.Start();
        }
        public void Stop() {
            timer.Stop();
            timer.Tick -= OnTimedEvent;
        }
    }

    public class GridLineItem : ImmutableObject {
        public double Value { get; private set; }
        public bool Major { get; private set; }

        public GridLineItem(double value, bool major) {
            this.Value = value;
            this.Major = major;
        }
    }
}
