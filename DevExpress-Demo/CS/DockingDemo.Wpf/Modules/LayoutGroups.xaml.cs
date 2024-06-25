using System;

namespace DockingDemo {
    public partial class LayoutGroups : DockingDemoModule {
        public LayoutGroups() {
            InitializeComponent();
            SizeChanged += new System.Windows.SizeChangedEventHandler(LayoutGroups_SizeChanged);
        }
        void LayoutGroups_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e) {
            if(e.NewSize.Height < 500) {
                layoutGroup2.Visibility = System.Windows.Visibility.Collapsed;
                layoutGroup3.Visibility = System.Windows.Visibility.Collapsed;
            }
            else {
                layoutGroup2.Visibility = System.Windows.Visibility.Visible;
                layoutGroup3.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
