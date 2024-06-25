using System;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Demos;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.PropertyGrid;

namespace SpreadsheetDemo {
    public partial class OperationRestrictions : SpreadsheetDemoModule {
        int optionsGroupCounter = 11;

        public OperationRestrictions() {
            InitializeComponent();
            propertyGridControl1.SelectedObject = new BehaviorOptionsProvider(spreadsheetControl1.Options.Behavior);
            propertyGridControl1.CustomExpand += OnCustomExpand;
        }

        void OnCustomExpand(object sender, CustomExpandEventArgs args) {
            args.IsExpanded = true;
            args.Handled = true;
            optionsGroupCounter--;
            if (optionsGroupCounter == 0)
                propertyGridControl1.CustomExpand -= OnCustomExpand;
        }
    }
}
