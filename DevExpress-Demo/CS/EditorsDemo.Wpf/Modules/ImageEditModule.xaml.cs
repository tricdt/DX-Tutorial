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
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Utils;
using System.Reflection;
using System.Collections.ObjectModel;

namespace EditorsDemo {
    
    
    
    public partial class ImageEditModule : EditorsDemoModule {
        public ImageEditModule() {
            DataContext = new EmployeeViewModel();
            InitializeComponent();
        }
    }
}
