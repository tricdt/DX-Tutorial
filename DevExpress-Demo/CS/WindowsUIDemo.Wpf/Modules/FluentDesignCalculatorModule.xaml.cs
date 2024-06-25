using DevExpress.Xpf.DemoBase;
using System.Windows;


namespace WindowsUIDemo {
    [CodeFile("Modules/FluentDesignCalculatorModule/FluentDesignCalculatorWindow.xaml")]
    [CodeFile("Modules/FluentDesignCalculatorModule/FluentDesignCalculatorWindow.xaml.(cs)")]
    public partial class FluentDesignCalculatorModule : DemoModule {
        public FluentDesignCalculatorModule() {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e) {
            Window newWindow = Window.GetWindow((DependencyObject)sender);
            FluentDesignCalculatorWindow fluentDesignCalculator = new FluentDesignCalculatorWindow();
            fluentDesignCalculator.Owner = newWindow;
            fluentDesignCalculator.ShowDialog();
        }
    }
}
