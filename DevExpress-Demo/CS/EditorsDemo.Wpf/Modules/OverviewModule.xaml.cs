using System;
using System.Collections.Generic;
using DevExpress.Xpf.DemoBase;

namespace EditorsDemo {
    [CodeFile("ViewModels/OverviewDemoViewModel.(cs)")]
    public partial class OverviewModule : EditorsDemoModule {
        public OverviewModule() {
            InitializeComponent();
            DataContext = new EmployeeViewModel();
        }
    }
}
