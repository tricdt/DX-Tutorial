using DevExpress.Xpf.DocumentViewer;
using DevExpress.Xpf.Gantt;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GanttDemo {
    public partial class GanttDemoRibbon : UserControl {
       public static readonly DependencyProperty ExportGanttProperty =
            DependencyProperty.Register("ExportGantt", typeof(GanttControl), typeof(GanttDemoRibbon), new PropertyMetadata(null));
        public GanttControl ExportGantt {
            get { return (GanttControl)GetValue(ExportGanttProperty); }
            set { SetValue(ExportGanttProperty, value); }
        }
        public GanttDemoRibbon() {
            InitializeComponent();
        }
    }
}
