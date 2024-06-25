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
using EditorsDemo.Utils;
using DevExpress.Xpf.Editors;
using System.Windows.Markup;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;

namespace EditorsDemo {
    
    
    
    [CodeFile("Controls/PaintControl.xaml")]
    [CodeFile("Controls/PaintControl.xaml.(cs)")]
    public partial class ColorEditModule : EditorsDemoModule {
        public ColorEditModule() {
            InitializeComponent();
        }
    }
}
