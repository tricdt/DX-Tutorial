using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {

    public partial class PieTab : TabItemModule {
        public PieTab() {
            InitializeComponent();
        }

        void chart_QueryChartCursor(object sender, QueryChartCursorEventArgs e) {
            ChartHitInfo hitInfo = this.chart.CalcHitInfo(e.Position);
            if (hitInfo != null && hitInfo.SeriesPoint != null)
                e.Cursor = Cursors.Hand;
        }
        bool isAnimationCompleted = false;
        public override bool IsAnimationCompleted {
            get {
                return isAnimationCompleted;
            }
        }
        private void Storyboard_Completed(object sender, System.EventArgs e) {
            isAnimationCompleted = true;
        }
    }


    class ShowTotalInToTitleVisibleConverter : ForwardOnlyValueConverter {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || !(value is string))
                return value;
            string str = (string)value;
            return str == "Title";
        }
    }


    class ShowTotalInToPieTotalLabelVisibilityConverter : ForwardOnlyValueConverter {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || !(value is string))
                return value;
            string str = (string)value;
            if (str == "Label")
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }
    }


    public class PieChartRotationBehavior : Behavior<ChartControl> {
        ChartControl Chart { get { return AssociatedObject; } }
        PieSeries2D Series { get { return (PieSeries2D)Chart.Diagram.Series[0]; } }
        bool rotate;
        Point startPosition;

        protected override void OnAttached() {
            base.OnAttached();
            Chart.MouseDown += Chart_MouseDown;
            Chart.MouseMove += Chart_MouseMove;
            Chart.MouseUp += Chart_MouseUp;
        }

        void Chart_MouseDown(object sender, MouseButtonEventArgs e) {
            Point position = e.GetPosition(Chart);
            ChartHitInfo hitInfo = Chart.CalcHitInfo(position);
            if (hitInfo != null && hitInfo.SeriesPoint != null) {
                this.rotate = true;
                this.startPosition = position;
            }
        }
        void Chart_MouseMove(object sender, MouseEventArgs e) {
            var position = e.GetPosition(Chart);
            var hitInfo = Chart.CalcHitInfo(position);
            if (hitInfo != null && this.rotate) {
                var angleDelta = CalcAngle(this.startPosition, position);
                angleDelta *= Series.SweepDirection == PieSweepDirection.Clockwise ? -1.0 : 1.0;
                var newAngle = Series.Rotation + angleDelta;
                if (Math.Abs(newAngle) > 360)
                    newAngle += -720 * Math.Sign(newAngle);
                Series.Rotation = newAngle;
                this.startPosition = position;
            }
        }
        void Chart_MouseUp(object sender, MouseButtonEventArgs e) {
            this.rotate = false;
        }
        double CalcAngle(Point p1, Point p2) {
            var center = new Point(Chart.Diagram.ActualWidth / 2, Chart.ActualHeight / 2);
            Vector startVector = p1 - center;
            Vector endVector = p2 - center;
            return Vector.AngleBetween(endVector, startVector);
        }
    }

}
