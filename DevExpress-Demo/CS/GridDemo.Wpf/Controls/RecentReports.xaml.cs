using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid.Printing;
using DevExpress.Xpf.Reports.UserDesigner.Extensions;
using DevExpress.XtraExport.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace GridDemo {
    public partial class RecentReports : UserControl {
        public static readonly DependencyProperty ReportServiceProperty =
            DependencyProperty.Register("ReportService", typeof(GridReportManagerService), typeof(RecentReports), new PropertyMetadata(null));
        public GridReportManagerService ReportService {
            get { return (GridReportManagerService)GetValue(ReportServiceProperty); }
            set { SetValue(ReportServiceProperty, value); }
        }
        public RecentReports() {
            InitializeComponent();
        }
    }
}
