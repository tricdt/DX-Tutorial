using System;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/SecondaryAxes.xaml"),
     CodeFile("Modules/ChartElements/SecondaryAxes.xaml.(cs)"),
     NoAutogeneratedCodeFiles]
    public partial class SecondaryAxesDemo : ChartsDemoModule {
        const string SecondaryPostfixX = " - Secondary Axis X";
        const string SecondaryPostfixY = " - Secondary Axis Y";
        const string PrimaryPostfixX = " - Primary Axis X";
        const string PrimaryPostfixY = " - Primary Axis Y";

        bool loading = true;

        XYSeries2D FirstSeries { get { return this.chart.Diagram.Series.Count > 0 ? (XYSeries2D)this.chart.Diagram.Series[0] : null; } }
        XYSeries2D SecondSeries { get { return this.chart.Diagram.Series.Count > 1 ? (XYSeries2D)this.chart.Diagram.Series[1] : null; } }
        AxisX2D AxisX { get { return ((XYDiagram2D)this.chart.Diagram).AxisX; } }
        AxisY2D AxisY { get { return ((XYDiagram2D)this.chart.Diagram).AxisY; } }
        SecondaryAxisX2D SecondaryAxisX { get { return ((XYDiagram2D)this.chart.Diagram).SecondaryAxesX[0]; } }
        SecondaryAxisY2D SecondaryAxisY { get { return ((XYDiagram2D)this.chart.Diagram).SecondaryAxesY[0]; } }

        public SecondaryAxesDemo() {
            InitializeComponent();
            CreateSeries(new LineSeries2D(), new LineSeries2D());
            SecondaryAxisX.Title.Content = SecondSeries.DisplayName + SecondaryPostfixX;
            SecondaryAxisY.Title.Content = SecondSeries.DisplayName + SecondaryPostfixY;
            this.lbChartType.SelectedIndex = 0;
            this.lbSeries2AxisX.SelectedIndex = 0;
            this.lbSeries2AxisY.SelectedIndex = 1;
            this.loading = false;
        }

        void PrepareSeries1(Series series) {
            series.DisplayName = "Series 1";
            series.Label = new SeriesLabel();
            series.Label.ResolveOverlappingMode = ResolveOverlappingMode.JustifyAroundPoint;
            if (this.chbVisible.IsChecked.HasValue)
                series.LabelsVisibility = chbVisible.IsChecked.Value;
            series.Points.Add(new SeriesPoint("A", 40));
            series.Points.Add(new SeriesPoint("B", 30));
            series.Points.Add(new SeriesPoint("C", 25));
            series.Points.Add(new SeriesPoint("D", 22.5));
            series.Points.Add(new SeriesPoint("E", 21.25));
        }
        void PrepareSeries2(Series series) {
            series.DisplayName = "Series 2";
            series.Label = new SeriesLabel();
            series.Label.ResolveOverlappingMode = ResolveOverlappingMode.JustifyAroundPoint;
            if (this.chbVisible.IsChecked.HasValue)
                series.LabelsVisibility = chbVisible.IsChecked.Value;
            series.Points.Add(new SeriesPoint("A", 1700));
            series.Points.Add(new SeriesPoint("B", 900));
            series.Points.Add(new SeriesPoint("C", 500));
            series.Points.Add(new SeriesPoint("D", 300));
            series.Points.Add(new SeriesPoint("E", 200));
            series.Points.Add(new SeriesPoint("F", 150));
            series.Points.Add(new SeriesPoint("G", 125));
        }
        void CreateSeries(XYSeries2D series1, XYSeries2D series2) {
            series1.AnimationAutoStartMode = AnimationAutoStartMode.SetStartState;
            series2.AnimationAutoStartMode = AnimationAutoStartMode.SetStartState;
            this.chart.Diagram.Series.Clear();
            this.chart.Diagram.Series.Add(series1);
            PrepareSeries1(series1);
            this.chart.Diagram.Series.Add(series2);
            PrepareSeries2(series2);
            if (this.lbSeries2AxisX.SelectedIndex == 1)
                SecondSeries.AxisX = SecondaryAxisX;
            if (this.lbSeries2AxisY.SelectedIndex == 1)
                SecondSeries.AxisY = SecondaryAxisY;
        }
        void lbChartType_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if (this.chart != null) {
                if (this.lbChartType.SelectedIndex == 0) {
                    LineSeries2D lineSeries1 = new LineSeries2D();
                    LineSeries2D lineSeries2 = new LineSeries2D();
                    lineSeries1.SeriesAnimation = new Line2DUnwrapVerticallyAnimation();
                    lineSeries1.PointAnimation = new Marker2DSlideFromTopAnimation();
                    lineSeries2.SeriesAnimation = new Line2DUnwrapVerticallyAnimation() { BeginTime = TimeSpan.FromMilliseconds(400) };
                    lineSeries2.PointAnimation = new Marker2DSlideFromTopAnimation() { BeginTime = TimeSpan.FromMilliseconds(400) };
                    CreateSeries(lineSeries1, lineSeries2);
                }
                else {
                    BarSideBySideSeries2D barSeries1 = new BarSideBySideSeries2D();
                    BarSideBySideSeries2D barSeries2 = new BarSideBySideSeries2D();
                    barSeries1.PointAnimation = new Bar2DSlideFromBottomAnimation() {
                        PointDelay = TimeSpan.FromMilliseconds(200)
                    };
                    barSeries2.PointAnimation = new Bar2DSlideFromBottomAnimation() {
                        PointDelay = TimeSpan.FromMilliseconds(200),
                        BeginTime = TimeSpan.FromMilliseconds(100)
                    };
                    CreateSeries(barSeries1, barSeries2);
                }
            }
            if (!this.loading)
                this.chart.Animate();
        }
        void lbSeries2AxisX_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if (this.chart != null && SecondSeries != null) {
                if (this.lbSeries2AxisX.SelectedIndex == 0) {
                    SecondSeries.AxisX = null;
                    SecondaryAxisX.Visible = false;
                    AxisX.Title.Content = FirstSeries.DisplayName + ", " + SecondSeries.DisplayName + PrimaryPostfixX;
                }
                else {
                    SecondSeries.AxisX = SecondaryAxisX;
                    SecondaryAxisX.Visible = true;
                    AxisX.Title.Content = FirstSeries.DisplayName + PrimaryPostfixX;
                }
            }
        }
        void lbSeries2AxisY_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if (this.chart != null && SecondSeries != null) {
                if (this.lbSeries2AxisY.SelectedIndex == 0) {
                    SecondSeries.AxisY = null;
                    SecondaryAxisY.Visible = false;
                    AxisY.Title.Content = FirstSeries.DisplayName + ", " + SecondSeries.DisplayName + PrimaryPostfixY;
                }
                else {
                    SecondSeries.AxisY = SecondaryAxisY;
                    SecondaryAxisY.Visible = true;
                    AxisY.Title.Content = FirstSeries.DisplayName + PrimaryPostfixY;
                }
            }
        }
        void chbVisible_Checked(object sender, RoutedEventArgs e) {
            foreach (Series series in this.chart.Diagram.Series)
                series.LabelsVisibility = true;
        }
        void chbVisible_Unchecked(object sender, RoutedEventArgs e) {
            foreach (Series series in this.chart.Diagram.Series)
                series.LabelsVisibility = false;
        }
    }
}
