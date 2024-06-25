using System.Windows.Controls;
using DevExpress.WindowsMailClient.Wpf.ViewModel;

namespace DevExpress.WindowsMailClient.Wpf.View {
    public partial class MainView : UserControl {
        public MainView() {
            DataContext = MainViewModel.Create();
            InitializeComponent();
        }
    }
}
