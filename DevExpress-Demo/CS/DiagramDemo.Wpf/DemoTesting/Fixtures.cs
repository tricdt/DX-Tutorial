using DevExpress.Xpf.DemoBase.DemoTesting;
using System.Threading;
using System.Windows.Threading;
using System;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using System.Windows;
using System.Globalization;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using System.Linq;

namespace DiagramDemo.Tests {
    public class DiagramCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool AllowSwitchToTheTheme(Type moduleType, Theme theme) {
            return theme != Theme.HybridApp;
        }
    }
    public class DiagramDemoModulesAccessor : DemoModulesAccessor<DiagramDemoModule> {
        public DiagramDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
    }
    public abstract class BaseDiagramDemoTestingFixture : BaseDemoTestingFixture {
        protected DiagramDemoModulesAccessor ModuleAccessor { get; private set; }
        public BaseDiagramDemoTestingFixture() {
            ModuleAccessor = GetModulesAccessor();
        }
        protected abstract DiagramDemoModulesAccessor GetModulesAccessor();
    }
}
