using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Grid;

namespace TreeListDemo {
    public partial class TreeListDemoRibbon : UserControl {
       public static readonly DependencyProperty ExportTreeListProperty =
            DependencyProperty.Register("ExportTreeList", typeof(TreeListControl), typeof(TreeListDemoRibbon), new PropertyMetadata(null));
        public TreeListControl ExportTreeList {
            get { return (TreeListControl)GetValue(ExportTreeListProperty); }
            set { SetValue(ExportTreeListProperty, value); }
        }
        public TreeListDemoRibbon() {
            InitializeComponent();
        }
    }
}
