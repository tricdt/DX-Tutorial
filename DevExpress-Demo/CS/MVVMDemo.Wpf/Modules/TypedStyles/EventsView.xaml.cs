using System.Windows;
using System.Windows.Controls;

namespace MVVMDemo.TypedStylesDemo {
    public partial class EventsView : UserControl {
        public EventsView() {
            InitializeComponent();
        }

        private void OnButtonLocalClick(object sender, System.Windows.RoutedEventArgs e) {
            MessageBox.Show("Local click");
        }

        private void OnContainerAttachedClick(object sender, System.Windows.RoutedEventArgs e) {
            MessageBox.Show("Container attached click");
        }
    }
}
