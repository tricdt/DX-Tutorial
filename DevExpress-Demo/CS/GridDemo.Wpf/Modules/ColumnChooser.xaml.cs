using DevExpress.Xpf.Grid;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace GridDemo {
    public partial class ColumnChooser : GridDemoModule {
        public ColumnChooser() {
            InitializeComponent();
            UpdateToggleButtonContent();
            OptionsDataContext = this;
        }
        protected override void ShowPopupContent() {
            base.ShowPopupContent();
            Dispatcher.BeginInvoke(new Action(gridView.ShowColumnChooser), DispatcherPriority.Render, null);
        }
        
        void showHideButton_Toggle(object sender, RoutedEventArgs e) {
            UpdateToggleButtonContent();
        }

        void UpdateToggleButtonContent() {
            showHideButton.Content = showHideButton.IsChecked.Value ? "Hide Column Chooser" : "Show Column Chooser";
        }
    }
}
