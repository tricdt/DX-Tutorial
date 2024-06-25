using System.Windows;

namespace PivotGridDemo {
    public partial class PrintOptions : PivotGridDemoModule {

        public PrintOptions() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            pivotGrid.BestFit(true, false);
            pivotGrid.ShowPrintPreview(Window.GetWindow(this));
        }
    }
}
