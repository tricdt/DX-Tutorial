using DevExpress.StockMarketTrader.Wpf.ViewModel;
using DevExpress.StockMarketTrader.Wpf.Views;
using DevExpress.Xpf.Core;

namespace DevExpress.StockMarketTrader {
    public partial class MainWindow : ThemedWindow {
        public MainWindow() {
            InitializeComponent();
        }

        void OnInformationPanelLoaded(object sender, System.Windows.RoutedEventArgs e) {
            ((InformationPanel)sender).DataContext = ((MainViewModel)DataContext).InformationPanelModel;
        }
    }
}
