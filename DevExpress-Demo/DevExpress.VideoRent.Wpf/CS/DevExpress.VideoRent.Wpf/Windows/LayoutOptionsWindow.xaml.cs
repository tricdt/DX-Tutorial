using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Core;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.Wpf {
    public partial class LayoutOptionsWindow : ThemedWindow {
        public LayoutOptionsWindow() {
            InitializeComponent();
        }
        void OnClearLayoutsButtonClick(object sender, RoutedEventArgs e) {
            LayoutManager.Current.ClearLayoutData<ElementLayoutData>();
        }
    }
}
