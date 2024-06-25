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
using System.Windows.Shapes;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;
using DevExpress.Utils;
using System.ComponentModel;
using DevExpress.Xpf.Core;
using DevExpress.Mvvm;

namespace RibbonDemo {    
    public partial class PainterWindow {
        public PainterWindow() {
            InitializeComponent();
        }

        void OnEditValueChanged(object sender, RoutedEventArgs e) {
            NavigationTree.ExitMenuMode(true, false);
        }
    }
}
