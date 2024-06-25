using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace RibbonDemo {
    [CodeFiles(@"Modules/RibbonWindow/Views/PainterWindow.xaml;
                 Modules/RibbonWindow/Views/PainterWindow.xaml.(cs);
                 Modules/RibbonWindow/ViewModels/PainterWindowViewModel.(cs);
                 Modules/RibbonWindow/Services/CloseWindowService.(cs);
                 Modules/RibbonWindow/Views/RibbonWindow.xaml;
                 Modules/RibbonWindow/Views/RibbonWindow.xaml.(cs)")]
    
    public partial class RibbonWindow : RibbonDemoModule {

        public RibbonWindow() {
            DevExpress.Xpf.Bars.BarManager.CheckBarItemNames = false;
            InitializeComponent();
        }

        private void ShowPainterWindow() {
            PainterWindow Window = new PainterWindow();
            Window.Width = this.ActualWidth;
            Window.Height = this.ActualHeight;
            Point pos = this.PointToScreen(new Point());
            Window.Left = pos.X;
            Window.Top = pos.Y;
            Window.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            ShowPainterWindow();
        }
    }
}
