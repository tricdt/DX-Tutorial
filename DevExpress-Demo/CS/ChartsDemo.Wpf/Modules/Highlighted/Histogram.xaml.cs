using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Highlighted/Histogram.xaml"),
     CodeFile("Modules/Highlighted/Histogram.xaml.(cs)"),
     CodeFile("Modules/Highlighted/HistogramViewModel.(cs)"),
     NoAutogeneratedCodeFiles]
    public partial class HistogramDemo : ChartsDemoModule {
        Random random = new Random(1);

        public HistogramDemo() {
            InitializeComponent();
            GeneratePoints();
        }
        void GeneratePoints() {
            List<IEnumerable<NumericPoint>> data = new List<IEnumerable<NumericPoint>>();
            data.Add(PointGenerator.GenerateCluster(random, random.Next(20, 70), random.Next(120, 180), random.Next(0, 10), random.Next(70, 120), 2000));
            data.Add(PointGenerator.GenerateCluster(random, random.Next(0, 10), random.Next(70, 120), random.Next(40, 80), random.Next(160, 200), 2000));
            data.Add(PointGenerator.GenerateCluster(random, random.Next(60, 100), random.Next(160, 200), random.Next(40, 80), random.Next(160, 200), 2000));
            for (int i = 0; i < chart.Diagram.Series.Count; i++) {
                int dataIndex = (int)Math.Floor(i / 3d);
                chart.Diagram.Series[i].DataSource = data[dataIndex];
            }
        }
        void BtnGeneratePoints_Click(object sender, RoutedEventArgs e) {
            GeneratePoints();
        }
    }
}
