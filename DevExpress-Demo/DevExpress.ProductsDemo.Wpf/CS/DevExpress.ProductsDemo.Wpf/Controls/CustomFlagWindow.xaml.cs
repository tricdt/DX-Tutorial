using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProductsDemo.Controls {
    
    
    
    public partial class CustomFlagWindow : ThemedWindow {
        public CustomFlagWindow() {
            InitializeComponent();
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
            Close();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
            Close();
        }
    }
}
