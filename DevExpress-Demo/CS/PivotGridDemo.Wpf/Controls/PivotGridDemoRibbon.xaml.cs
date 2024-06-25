using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    public partial class PivotGridDemoRibbon : UserControl {

       public static readonly DependencyProperty ExportPivotGridProperty =
            DependencyProperty.Register("ExportPivotGrid", typeof(PivotGridControl), typeof(PivotGridDemoRibbon), new PropertyMetadata(null));

        public PivotGridControl ExportPivotGrid {
            get { return (PivotGridControl)GetValue(ExportPivotGridProperty); }
            set { SetValue(ExportPivotGridProperty, value); }
        }

        public PivotGridDemoRibbon() {
            InitializeComponent();
        }
    }
}
