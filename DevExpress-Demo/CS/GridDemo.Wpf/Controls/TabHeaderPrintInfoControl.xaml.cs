using DevExpress.Xpf.Printing;
using System.Windows;
using System.Windows.Controls;

namespace GridDemo {
    public partial class TabHeaderPrintInfoControl : UserControl {
        public LinkBase Link { get; set; }

        public string TabName {
            get { return (string)GetValue(TabNameProperty); }
            set { SetValue(TabNameProperty, value); }
        }
        public static readonly DependencyProperty TabNameProperty =
            DependencyProperty.Register("TabName", typeof(string), typeof(TabHeaderPrintInfoControl), new PropertyMetadata(null, new PropertyChangedCallback(OnTabNameChanged)));
        static void OnTabNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((TabHeaderPrintInfoControl)d).OnTabNameChanged();
        }
        public TabHeaderPrintInfoControl() {
            InitializeComponent();
        }
        void OnTabNameChanged() {
            tabNameTextBlock.Text = TabName;
        }
    }
}
