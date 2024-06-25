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

namespace EditorsDemo {
    public partial class SeparatorControl : UserControl {
        public static readonly DependencyProperty HeaderProperty;

        static SeparatorControl() {
            HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(SeparatorControl));
        }

        public SeparatorControl() {
            InitializeComponent();
            DataContext = this;
        }

        public string Header {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
    }
}
